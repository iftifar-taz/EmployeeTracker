using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Identity.Web.Configurations
{
    public static class DBConfiguration
    {
        public static async Task ApplyPendingMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                db.Database.Migrate();
            }

            await DbSeeder.SeedUsersAndRolesAsync(app.ApplicationServices);
        }
    }
}
