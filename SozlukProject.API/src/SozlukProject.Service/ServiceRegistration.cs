using Microsoft.Extensions.DependencyInjection;
using SozlukProject.Service.Services;
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
            // Comment
            services.AddScoped<CommentService>();

            // Discussion
            services.AddScoped<DiscussionService>();

            // User
            services.AddScoped<UserService>();

            // Vote
            services.AddScoped<VoteService>();
        }
    }
}
