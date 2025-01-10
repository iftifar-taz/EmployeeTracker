using EmployeeManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Web.Configurations
{
    public static class DBConfiguration
    {
        public static void ApplyPendingMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataContext>();
            db.Database.Migrate();
        }
    }
}
