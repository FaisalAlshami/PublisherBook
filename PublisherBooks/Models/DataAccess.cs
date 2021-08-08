using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization.Attributes;


namespace PublisherBooks.Models
{
    public class DataAccess
    {
        //this class to connect to db 

        public const string BookTableName = "Book";
        public const string UserTableName = "User";
        public const string UserDemandTableName = "UserDemand";
        MongoClient _client;
        MongoServer _server;
        public MongoDatabase _db;
        /******************/
        public string ConnectState = "";
        // if ConnectState=OK is connect otherwise not connect and error


        public DataAccess()
        {
            try
            {
                _client = new MongoClient("mongodb://localhost:27017");
                _server = _client.GetServer();
                _db = _server.GetDatabase("Crossover");
                ConnectState = "OK";
                if(_server.State==MongoServerState.Disconnected ||
                    _server.State == MongoServerState.Disconnecting )
                {
                    ConnectState = "Error";
                    // if server db connect state is disconnect redirect to page show web application disconnect to db

                }
            }
            catch(InvalidOperationException e)
            {
                ConnectState = "Error";
            }
            catch(Exception e)
            {
                ConnectState = "Error";
            }
        }

      

        public IEnumerable<Book> GetBooks()
        {
            return _db.GetCollection<Book>(BookTableName).FindAll();
        }
        public Book GetBookById(ObjectId oid)
        {
            var res = Query<Book>.Where(a => a.Id.Equals(oid));
            return _db.GetCollection<Book>(BookTableName).FindOne(res);
         
        }
        public User GetUserByUsername(string usname)
        {
            var res = Query<User>.Where(a => a.Username.Equals(usname));
            return _db.GetCollection<User>(UserTableName).FindOne(res);

        }
        public IEnumerable<User> GetUsers()
        {
            return _db.GetCollection<User>(UserTableName).FindAll();
        }
        public IEnumerable<UserDemand> GetUserDemands(string username)
        {
            var res = Query<UserDemand>.Where(a => a.OwnUser.Username.Equals(username));
            return _db.GetCollection<UserDemand>(UserDemandTableName).Find(res);// FindAll();
            
        }
        public User CheckLogin(string username, string pwd)
        {
            var res = Query<User>.Where(a=>a.Username.Equals(username) && a.Password.Equals(pwd));
            return _db.GetCollection<User>(UserTableName).FindOne(res);
        }
        public User CreateUser(User usr)
        {

            _db.GetCollection<User>(UserTableName).Save(usr);
            return usr;
            
        }
        public User CheckUsernameExist(string username)
        {
            var res = Query<User>.Where(a => a.Username.Equals(username));
            return _db.GetCollection<User>(UserTableName).FindOne(res);
        }
        public void RemoveUserDemand(ObjectId bkid, ObjectId usid)
        {
            
            var res = Query<UserDemand>.Where(a => a.Id.Equals(bkid)
                    && a.UserId.Equals(usid));
           var operation= _db.GetCollection<UserDemand>(UserDemandTableName).Remove(res);
            

        }
        public void RemoveUserDemand(string bkid, string usid)
        {
            ObjectId obkid = new ObjectId(bkid);
            ObjectId ousid = new ObjectId(usid);
            var res = Query<UserDemand>.Where(a => a.Id.Equals(obkid)
                    && a.UserId.Equals(ousid));
            var operation = _db.GetCollection<UserDemand>(UserDemandTableName).Remove(res);


        }

        public void CreateUserDemandBook(string bookid,string username)
        {
            User user = GetUserByUsername(username);
            ObjectId Id = new ObjectId(bookid);
            Book book = GetBookById(Id);
            try
            {
                UserDemand usdemandbook = new UserDemand();
                usdemandbook.Id = Id;
                usdemandbook.UserId = user.Id;
                usdemandbook.Username = username;
                usdemandbook.OwnUser = user;
                usdemandbook.UserDemandBook = book;
                var demand = _db.GetCollection<UserDemand>(UserDemandTableName);
                demand.Save(usdemandbook);

            }
            catch (Exception ex)
            {
            }
        }
        public void ChangepasswordUser(string username,string pwd)
        {
            User user = GetUserByUsername(username);
            try
            {
                user.Password = pwd;
                var demand = _db.GetCollection<User>(UserTableName);
                demand.Save(user);

            }
            catch (Exception ex)
            {
            }
        }
    }
}