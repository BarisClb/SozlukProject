using SozlukProject.Service.Contracts;
using SozlukProject.Service.Dtos.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        public async Task ActivationEmail(UserReadDto user)
        {
            int activationCode = new Random().Next(1000, 9999);

            string receiverEmail = user.Email;
            string subject = $"Activation code for {user.Username}.";
            string text = $"Your Account Activation code is {activationCode}.";

            await SendEmail(receiverEmail, subject, text);
        }

        public async Task WelcomeEmail(UserReadDto user)
        {
            string receiverEmail = user.Email;
            string subject = $"Welcome, {user.Username}!";
            string text = $"Don't forget to Activate your account!";

            await SendEmail(receiverEmail, subject, text);
        }


        public static async Task SendEmail(string receiverEmail, string subject, string text)
        {
            MailMessage mail = new()
            {
                // Change "example@example.com" to Email address

                From = new MailAddress("example@example.com"),
                Subject = subject,
                Body = text,
                IsBodyHtml = true
            };

            mail.To.Add(receiverEmail);

            SmtpClient smtp = new()
            {
                // Change "example@example.com" to Email address, "password" to Email password

                Credentials = new NetworkCredential("example@example.com", "password"),
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true
            };

            smtp.Send(mail);
        }
    }
}
