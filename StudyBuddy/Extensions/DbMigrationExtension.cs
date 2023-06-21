using Microsoft.EntityFrameworkCore;
using StudyBuddy.Repositories;

namespace StudyBuddy.WebApi.Extensions
{
    public static class DbMigrationExtension
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<StudyBuddyContext>();
                db.Database.Migrate();
            }    
        }
    }
}
