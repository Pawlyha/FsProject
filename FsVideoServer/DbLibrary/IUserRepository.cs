using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLibrary
{
    public interface IUserRepository
    {
        bool Create(UserSession user);

        bool Update(UserSession user);

        bool Delete(UserSession user);

        UserSession Get(string name, string password);

        UserSession Get(Guid id);

        UserSession GetByConnection(string connectionId);
    }
}
