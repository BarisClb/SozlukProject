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
        Task ActivationEmail(UserReadDto user);
        Task WelcomeEmail(UserReadDto user);
    }
}
