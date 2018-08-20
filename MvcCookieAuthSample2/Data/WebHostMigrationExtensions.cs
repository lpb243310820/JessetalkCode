using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MvcCookieAuthSample2.Data
{
    public static class WebHostMigrationExtensions
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost host, Action<TContext, IServiceProvider> sedder)
            where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    context.Database.Migrate();
                    sedder(context, services);

                    logger.LogInformation($"执行DBcontext {typeof(TContext).Name} seed 方法执行成功");
                }
                catch (Exception e)
                {
                    logger.LogError(e, $"执行DBcontext {typeof(TContext).Name} seed 方法失败");
                }
            }

            return host;
        }
    }
}
