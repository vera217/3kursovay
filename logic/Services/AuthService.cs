using logic.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;

namespace logic.Services
{
    public class AuthService
    {
        private readonly AppDbContext _db;

        public AuthService(AppDbContext db)
        {
            _db = db;
        }

        public User Login(string username, string password)
        {
            return _db.users
                .FirstOrDefault(u => u.username == username && u.password == password);
        }

        public bool RegisterClient(string clientId, string phone, string email, string password)
        {
            if (_db.users.Any(u => u.username == clientId))
                return false;

            var user = new User
            {
                username = clientId,
                password = password,
                role = "client"
            };

            var client = new Client
            {
                ClientId = clientId,
                PhoneNumber = phone,
                email = email
            };

            _db.users.Add(user);
            _db.clients.Add(client);

            return _db.SaveChanges() > 0;
        }

        public List<User> GetAllUsers()
        {
            return _db.users.ToList();
        }

        public User GetUserByUsername(string username)
        {
            return _db.users.FirstOrDefault(u => u.username == username);
        }
    }
}
