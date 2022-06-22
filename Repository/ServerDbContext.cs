using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
	public class ServerDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<MsgUsers> MsgUsers { get; set; }


        private string connectionString =
            "FileName=serverSqlDB.db";
        //"FileName=/Users/rotemgh/Documents/programingCourses/yearB/AdvancedProgramming2/AdvancedProgrammingWebServerSide/AdvancedProgrammingWebServer/newIcqDB.db";

        public ServerDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                //.UseLazyLoadingProxies()
                .UseSqlServer("Server=localhost;Database=master;User=sa;Password=Strong.Pwd-123;");
            //+"Trusted_Connection=True;MultipleActiveResultSets=true");

            /*optionsBuilder.UseSqlite(connectionString, options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });*/

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(k => k.Id);

            });

            modelBuilder.Entity<Chat>().ToTable("Chat");
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasKey(k => k.ChatId);

            });

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(k => k.Id);

            });

            modelBuilder.Entity<Rate>().ToTable("Rate");
            modelBuilder.Entity<Rate>(entity =>
            {
                entity.HasKey(k => k.Id);

            });
          
            modelBuilder.Entity<MsgUsers>().ToTable("MsgUsers");
            modelBuilder.Entity<MsgUsers>(entity =>
            {
                entity.HasKey(k => k.Id);
            });

            base.OnModelCreating(modelBuilder);


        }

        public void DetachAllEntities()
        {
            ChangeTracker.Clear();
            /*var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;*/
        }


        #region chats

        public Chat? getChat(User user1, User user2)
        {
            if(user1 == null || user2 == null)
            {
                return null;
            }

            return Chats.Include(c => c.user1)
               .Include(c => c.user2).AsNoTracking().ToList().Find((chat) => {
                   return
                (chat.user1.Id == user1.Id &&
                chat.user2.Id == user2.Id) ||
                (chat.user1.Id == user2.Id &&
                chat.user2.Id == user1.Id);
               });
        }
        public List<Chat> getAllChats()
        {
            //Users.AsNoTracking().ToList();

            //return Chats.AsNoTracking().ToList();

            //List<Chat> chats = Chats.AsNoTracking().ToList();
            return Chats.Include(c=>c.user1)
                .Include(c=>c.user2).AsNoTracking().ToList();

        }

        public void insertChat(Chat chat)
        {
            //User us1 = new User(chat.user1.Id, chat.user1.Name, chat.user1.Server);
            //User us2 = new User(chat.user2.Id, chat.user2.Name, chat.user2.Server);
            //chat.user1.State = EntityState.Detached;
            DetachAllEntities();
            User us1 = Users.Attach(chat.user1).Entity;
            User us2 = Users.Attach(chat.user2).Entity;
            Chats.Add(new Chat
            {
                user1 = us1,
                user2 = us2
            });

            //Chats.Add(chat);
            SaveChanges();

            Entry(chat.user1).State = EntityState.Detached;
            Entry(chat.user2).State = EntityState.Detached;
        }

        public void removeChat(Chat chat)
        {
            Chats.Remove(chat);
            SaveChanges();
        }

        #endregion

        #region messages

        public List<Message> getAllMsgs()
        {
            return Messages.AsNoTracking().ToList<Message>();
        }

        public Message? getMsg(int id)
        {
            return Messages.AsNoTracking().Where(msg => msg.Id == id)
                .Select(msg => msg).SingleOrDefault();

        }

        public Message? insertMsg(Message msg)
        {
            Messages.Add(msg);
            SaveChanges();

            //get the last message that just added
            return Messages.OrderByDescending(msg => msg.Id).FirstOrDefault();

        }

        public void updateContent(Message msg)
        {
            Message? currMsg = Messages.Where(m => m.Id == msg.Id)
                .Select(msg => msg).SingleOrDefault();
            if (currMsg != null)
            {
                DetachAllEntities();
                currMsg.Content = msg.Content;
                Messages.Attach(currMsg).Property(x => x.Content).IsModified = true;
                SaveChanges();

                Entry(currMsg).State = EntityState.Detached;
               
            }
        }

        #endregion

        #region users

        public List<User> getAllUsers()
        {
            return Users.AsNoTracking().ToList<User>();
        }

        public User? getUser(String username)
        {
            return Users.AsNoTracking().Where(user => user.Id == username)
                .Select(user => user).SingleOrDefault();
        }

        public void insertUser(User user)
        {
            Users.Add(user);
            SaveChanges();
        }

        public void removeUser(User user)
        {
            Users.Remove(user);
            SaveChanges();
        }

        public void updateImageUser(String username, String img)
        {
            //var user = new User { Id = username, Image = img };
            
            User? user = Users.Where(user => user.Id == username)
                .Select(user => user).SingleOrDefault();

            if (user != null)
            {
                DetachAllEntities();
                user.Image = img;
                Users.Attach(user).Property(x => x.Image).IsModified = true;
                SaveChanges();
                Entry(user).State = EntityState.Detached;
            }
        }

        public void updateUserNameAndServer(User user)
        {
            if (user != null)
            {
                DetachAllEntities();
                Users.Attach(user).Property(x => x.Server).IsModified = true;
                SaveChanges();
                Users.Attach(user).Property(x => x.Name).IsModified = true;
                SaveChanges();

                Entry(user).State = EntityState.Detached;
            }
        }

        public void updateUserLastMsg(User user)
        {
            if (user != null)
            {
                DetachAllEntities();
                Users.Attach(user).Property(x => x.last).IsModified = true;
                SaveChanges();
                Users.Attach(user).Property(x => x.lastdate).IsModified = true;
                SaveChanges();

                Entry(user).State = EntityState.Detached;
            }
        }

        #endregion

        #region msgInChat

        public List<MsgInChat> getAllMsgInChat()
        {
            List<MsgInChat> msgInChat = new List<MsgInChat>();

            List<MsgUsers> msgUsers = MsgUsers
                .Include(msg => msg.From).Include(msg => msg.To)
                .Include(msg => msg.Message).AsNoTracking().ToList();

            foreach (MsgUsers msgUser in msgUsers)
            {
                MsgInChat? msgChat = msgInChat.Find(m =>
                (m.Chat.user1.Id == msgUser.From.Id &&
                m.Chat.user2.Id == msgUser.To.Id) ||
                (m.Chat.user1.Id == msgUser.To.Id &&
                m.Chat.user2.Id == msgUser.From.Id));

                //check if the chat exists because we might found it allready
                if (msgChat != null)
                {
                    msgChat.Messages.Add(msgUser);
                }
                else
                {
                    Chat c = getChat(msgUser.To, msgUser.From);
                    msgInChat.Add(new MsgInChat(c, msgUser));
                }
            }

            return msgInChat;

        }

        public void insertMsgInChat(MsgInChat msg)
        {
            foreach (MsgUsers msgUser in msg.Messages)
            {
                //msgUser.Chat = msg.Chat;
                //MsgUsers.Add(msgUser);

                DetachAllEntities();
                //Chat c = Chats.Attach(msgUser.Chat).Entity;
                Message message = Messages.Attach(msgUser.Message).Entity;
                User to = Users.Attach(msgUser.To).Entity;
                User from = Users.Attach(msgUser.From).Entity;
                //User to = msgUser.To;
                //User from = msgUser.From;

                MsgUsers newMsg = new MsgUsers(message,from, to);
                //newMsg.Chat = c;
                MsgUsers.Add(newMsg);

                SaveChanges();

                //Entry(c).State = EntityState.Detached;
                Entry(message).State = EntityState.Detached;
                //Entry(to).State = EntityState.Detached;
                //Entry(from).State = EntityState.Detached;

            }

            //MsgInChat.Add(msg);
            //SaveChanges();

        }

        public void removeMsgInChat(MsgUsers msg)
        {
            MsgUsers.Remove(msg);
            SaveChanges();
        }
        #endregion

        #region rate
        public List<Rate> getAllRate()
        {
            return Rates.ToList<Rate>();
        }

        public Rate? getRate(int? id)
        {
            return Rates.Where(rate => rate.Id == id)
                .Select(rate => rate).SingleOrDefault();
        }

        public void insertRate(Rate rate)
        {
            Rates.Add(rate);
            SaveChanges();
        }

        public void removeRate(Rate rate)
        {
            Rates.Remove(rate);
            SaveChanges();
        }

        public void updateRate(Rate rate)
        {
            /*Rate r = getRate(rate.Id);
            r.Feedback = rate.Feedback;
            r.Name = rate.Name;
            r.RateNumber = rate.RateNumber;*/
            DetachAllEntities();
            Rates.Attach(rate).Property(x => x.Feedback).IsModified = true;
            SaveChanges();

            Rates.Attach(rate).Property(x => x.Name).IsModified = true;
            SaveChanges();

            Rates.Attach(rate).Property(x => x.RateNumber).IsModified = true;
            SaveChanges();


            Entry(rate).State = EntityState.Detached;
           
        }
        #endregion
    }
}

