using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Middlewares
{
    public static class SeedRolesAndAdminMiddlewareExtensions
    {
        public static IApplicationBuilder UseSeedRolesMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedRolesAndAdminMiddleware>();
        }
    }
}
