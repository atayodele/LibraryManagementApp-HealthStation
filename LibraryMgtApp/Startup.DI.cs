using LibraryMgtApp.Context;
using LibraryMgtApp.Extensions;
using LibraryMgtApp.Extensions.Helpers;
using LibraryMgtApp.Infrastructure.Interfaces;
using LibraryMgtApp.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp
{
    public partial class Startup
    {
        public static IServiceCollection AddIposbiService(IServiceCollection services)
        {
            //Register DI for Domain Service
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserMgmtService, UserMgmtService>();
            services.AddScoped<ICheckoutService, CheckoutService>();
            services.AddScoped<ApiExceptionFilter>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.Configure<AppSettings>(Startup.Configuration);
            services.AddScoped<IDbContext, DataContext>();
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));
            return services;
        }
    }
}
