using SozlukProject.Service.Dtos.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Contracts
{
    public interface IEmailService
    {
        Task<int> ActivationEmail(UserReadDto user);
        Task<int> WelcomeEmail(UserReadDto user);
        Task<int> ForgotPassword(string email);
    }
}
