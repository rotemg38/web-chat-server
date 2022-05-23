using System;
using System.Collections.Generic;
using Models;

namespace Services
{
    //todo: in the future we ill add Repository project that will access to DB.
    public class MsgInChatService
    {
        //
        private static List<MsgInChat> _msgInChatsList = new List<MsgInChat>
        {
            new MsgInChat(
                new Chat
                {
                    ChatId = 1,
                    Participants = new Tuple<User, User>(
                                            new User("shir", "Shir", "Shir1998", "default_picture.jpg", "localhost:5000"),
                                            new User("dwayne johnson", "The Rock", "Strong9", "https://www.biography.com/.image/t_share/MTgwOTI0NDYwNjQ2Mjc4MjMy/gettyimages-1061959920.jpg", "localhost:5000")
                                                        )
                },
                new List<MsgUsers> { new MsgUsers(
                                                new Message(  1,   "content",  "10.10.10", true ),
                                                new User("shir", "Shir", "Shir1998", "default_picture.jpg", "localhost:5000") ,
                                                new User("dwayne johnson", "The Rock", "Strong9", "https://www.biography.com/.image/t_share/MTgwOTI0NDYwNjQ2Mjc4MjMy/gettyimages-1061959920.jpg", "localhost:5000")

                                                  )
                                    }
                )
        };

        public MsgInChatService() {
            
        }

        public List<MsgInChat> GetAll()
        {
            return _msgInChatsList;
        }
        public List<MsgUsers> GetMessagesInChat(Chat chat)
        {
            if (chat != null)
            {
                MsgInChat msgInChat = _msgInChatsList.Find(
                       (msgInChat) => { return msgInChat.Chat.ChatId == chat.ChatId; });
                if (msgInChat == null)
                    return null;
                return msgInChat.Messages;
            }
            return null;
        }

        public List<Message> ExtractMessages(List<MsgUsers> msgsUsers)
        {
            List<Message> lstMsgs = new List<Message>();
            foreach (MsgUsers  msgUser in msgsUsers)
            {
                lstMsgs.Add(msgUser.Message);
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
            var messages = GetMessagesInChat(chat);
            //if there is no messages- need to add new
            if(messages == null)
            {
                _msgInChatsList.Add(new MsgInChat(chat, msg));
            }
            else
            {
                //otherwise need to add the message to the existing list
                messages.Add(msg);
            }
            
        }

        //todo: check if needed?
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
                    msgsUsers.Remove(GetMsg(msgsUsers, idMsg));
                    return true;
                }
            }

            return false;
        }

        //check if the given username sent the message
        public bool IsSender(string userName, int idMsg)
        {
            foreach(MsgInChat msgInChat in _msgInChatsList)
            {
                //msgInChat.Chat
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
    }
}
