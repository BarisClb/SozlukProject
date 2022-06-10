using Microsoft.Extensions.DependencyInjection;
using SozlukProject.Service.Contracts;
using SozlukProject.Service.Services;
using SozlukProject.Service.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service
{
    public static class ServiceRegistration
    {
        public static void ImplementServiceServices(this IServiceCollection services)
        {
            //// Services

            // AccountService
            services.AddScoped<AccountService>();

            // Comment
            services.AddScoped<CommentService>();

            // Discussion
            services.AddScoped<DiscussionService>();

            // User
            services.AddScoped<UserService>();

            // UserActivation
            services.AddScoped<UserActivationService>();

            // Vote
            services.AddScoped<VoteService>();

            // Discussion Page
            services.AddScoped<DiscussionPageService>();

        }
    }
}
