using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Contracts
{
    public interface IBCryptNet
    {
        string HashPassword(string password);
        bool CheckPassword(string password, string hashedPassword);
    }
}
