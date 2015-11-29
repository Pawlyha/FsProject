using DbLibrary;
using FsVideoServer.Models;
using System.Linq;
using CryptoLibrary;
using System;

namespace FsVideoServer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        private readonly CryptoService _crypto;

        public UserRepository()
        {
            _context = new UserContext();
            _crypto = new CryptoService();
        }

        public bool Create(UserSession session) 
        {
            var result = _context.UserConnections.Add(session);
            return _context.SaveChanges() > 0;
        }

        public bool Update(UserSession user)
        {
            var connection = _context.UserConnections.FirstOrDefault(m => m.Id == user.Id);

            if (connection != null)
            {
                connection = user;
                return _context.SaveChanges() > 0;
            }

            return false;
        }

        public bool Delete(UserSession user) 
        {
            var connection = _context.UserConnections.Find(user.Id);
            if(connection != null)
                _context.UserConnections.Remove(connection);

            return _context.SaveChanges() > 0;
        }

        public UserSession Get(string name, string password)
        {
            return _context.UserConnections.FirstOrDefault(m => m.UserName == name &&
                m.Password == _crypto.Encrypt(password));
        }

        public UserSession Get(Guid id)
        {
            return _context.UserConnections.Find(id);
        }

        public UserSession GetByConnection(string connectionId)
        {
            return _context.UserConnections.FirstOrDefault(m => m.ConnectionId == connectionId);
        }
    }
}