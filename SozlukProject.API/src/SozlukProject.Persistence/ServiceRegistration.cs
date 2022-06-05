using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SozlukProject.Domain.Repositories;
using SozlukProject.Persistence.Contexts;
using SozlukProject.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Persistence
{
    public static class ServiceRegistration
    {
        public static void ImplementPersistenceServices(this IServiceCollection services, string connectionString)
        {
            //// Service Connection

            // Sql Server Connection
            services.AddDbContext<SozlukProjectDbContext>(options => options.UseSqlServer(connectionString));

            //// Dependency Injection

            // Comment
            services.AddScoped<ICommentRepository, CommentRepository>();
            
            // Discussion
            services.AddScoped<IDiscussionRepository, DiscussionRepository>();
            
            // User
            services.AddScoped<IUserRepository, UserRepository>();
            
            // Vote
            services.AddScoped<IVoteRepository, VoteRepository>();
        }
    }
}
