﻿using System;
using System.Collections.Generic;
using Models;
using Repository;

namespace Services
{
    public class MsgInChatService :IMsgInChatService
    {
        private ServerDbContext _context;

        public MsgInChatService() {
            _context = new ServerDbContext();
        }

        public List<MsgUsers> GetMessagesInChat(int chatId)
        {
            var msgInChatsList = _context.getAllMsgInChat();
            MsgInChat msgInChat = msgInChatsList.Find(
                   (msgInChat) => { return msgInChat.Chat.ChatId == chatId; });
            if (msgInChat == null)
                return null;
            return msgInChat.Messages;
        }

        public List<MsgUsers> GetMessagesInChat(Chat chat)
        {
            if (chat != null)
            {
                return GetMessagesInChat(chat.ChatId);
            }
            return null;
        }

        public List<Message> ExtractMessages(List<MsgUsers> msgsUsers)
        {
            List<Message> lstMsgs = new List<Message>();
            if (msgsUsers != null)
            {
                foreach (MsgUsers msgUser in msgsUsers)
                {
                    lstMsgs.Add(msgUser.Message);
                }
            }
            return lstMsgs;
        }

        public List<MsgUsers> GetAllMessages(List<Chat> chats)
        {
            List<MsgUsers> messages = new List<MsgUsers>();
            if (chats != null)
            {
                //go throug all chats
                foreach (Chat chat in chats)
                {
                    //and for each chat get the messages
                    List<MsgUsers> msgsInChat = GetMessagesInChat(chat);

                    //if there are messages in this chat add to the big list of messages
                    if (msgsInChat != null)
                    {
                        messages.AddRange(msgsInChat);
                    }
                }
            }
            return messages;
        }

        public void AddMsgInChat(Chat chat, MsgUsers msg)
        {
            _context.insertMsgInChat(new MsgInChat(chat, msg));
        }

        public MsgUsers CreatMsgUsers(User from, User to, Message msg)
        {
            return new MsgUsers(msg, from, to);
        }


        //get the message from the given list by Id
        public MsgUsers GetMsg(List<MsgUsers> msgsUsers, int idMsg)
        {
            if (msgsUsers != null)
            {
                return msgsUsers.Find((msgUser) =>
                {
                    return msgUser.Message.Id == idMsg;
                });
            }
            return null;
        }

        //get list of messages of specific chat that contain the message with the given Id
        public List<MsgUsers> FindAllMsgs(Chat chat, int idMsg)
        {
            List<MsgUsers> msgsUsers = GetMessagesInChat(chat);
            //find the message in the chat by the given Id
            MsgUsers msg = GetMsg(msgsUsers, idMsg);

            if (msg != null)
            {
                return msgsUsers;
            }
            else
            {
                return null;
            }

        }

        //from the list of chats- delete the message
        public bool DeleteMsg(List<Chat> chats, int idMsg)
        {
            //go through all given chats
            foreach(Chat chat in chats)
            {
                List<MsgUsers> msgsUsers = FindAllMsgs(chat, idMsg);
                //if we found the message in one of them
                if (msgsUsers != null)
                {
                    _context.removeMsgInChat(GetMsg(msgsUsers, idMsg));
                    return true;
                }
            }

            return false;
        }

        //check if the given username sent the message
        public bool IsSender(string userName, int idMsg)
        {
            var msgInChatsList = _context.getAllMsgInChat();
            foreach (MsgInChat msgInChat in msgInChatsList)
            {
                List<MsgUsers> msgsUsers = FindAllMsgs(msgInChat.Chat, idMsg);
                MsgUsers msgUsers = GetMsg(msgsUsers, idMsg);
                if(msgUsers != null)
                {
                    return msgUsers.From.Id == userName;
                }
            }

            return false;
        }

        public List<Message> GetCopyWithFixedSent(string userName, List<Message> msgs)
        {
            List<Message> fixedMsgs = new List<Message>();
            foreach (var msg in msgs)
            {
                Message fixedMsg = new Message(msg);
                fixedMsg.Sent = IsSender(userName, msg.Id);
                fixedMsgs.Add(fixedMsg);
            }

            return fixedMsgs;
        }

        public Message GetCopyWithFixedSent(string userName, Message msg)
        {
            Message fixedMsg = new Message(msg);
            fixedMsg.Sent = IsSender(userName, msg.Id);
            
            return fixedMsg;
        }
    }
}
