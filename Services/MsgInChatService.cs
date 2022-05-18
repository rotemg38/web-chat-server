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
                                            new User { id = "shir", name = "Shir", Password = "Shir1998", Image = "default_picture.jpg" },
                                            new User { id = "dwayne johnson", name = "The Rock", Password = "Strong9", Image = "https://www.biography.com/.image/t_share/MTgwOTI0NDYwNjQ2Mjc4MjMy/gettyimages-1061959920.jpg" }
                                                        )
                },
                new List<MsgUsers> { new MsgUsers(
                                                new Message() { Id = 1, Type = "text", Text = "content", Date = "10.10.10", MediaSrc = "uri" },
                                                new User { id = "shir", name = "Shir", Password = "Shir1998", Image = "default_picture.jpg" } ,
                                                new User { id = "dwayne johnson", name = "The Rock", Password = "Strong9", Image = "https://www.biography.com/.image/t_share/MTgwOTI0NDYwNjQ2Mjc4MjMy/gettyimages-1061959920.jpg" }
                                                  )
                                    }
                )
        };

        public MsgInChatService() {
            
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


        //get the message from the given list by id
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

        //get list of messages of specific chat that contain the message with the given id
        public List<MsgUsers> FindAllMsgs(Chat chat, int idMsg)
        {
            List<MsgUsers> msgsUsers = GetMessagesInChat(chat);
            //find the message in the chat by the given id
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
        public void DeleteMsg(List<Chat> chats, int idMsg)
        {
            //go through all given chats
            foreach(Chat chat in chats)
            {
                List<MsgUsers> msgsUsers = FindAllMsgs(chat, idMsg);
                //if we found the message in one of them
                if (msgsUsers != null)
                {
                    msgsUsers.Remove(GetMsg(msgsUsers, idMsg));
                }
            }
            
        }

    }
}
