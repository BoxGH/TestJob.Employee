using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TestJob.Employee.Models;
using Microsoft.Data.Entity;

namespace TestJob.Employee
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string connectStr = "Server=(localdb)\\mssqllocaldb;Database=db-Employee;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddEntityFramework()
                    .AddSqlServer()
                    .AddDbContext<AppDbContext>(options => options.UseSqlServer(connectStr));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            { 
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Employees}/{action=Start}/{id?}");
            });
        }
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
