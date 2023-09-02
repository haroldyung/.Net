using Microsoft.EntityFrameworkCore;

namespace TestLogAuditRequest.Models.TestAuditControllerRequest
{
    public class TestAuditControllerRequestContext: DbContext
    {
        protected readonly IConfiguration Configuration;

        public TestAuditControllerRequestContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("auditLog"));
        }

        public DbSet<REQUEST_LOG> REQUEST_LOGs { get; set; }
        public DbSet<REQUEST_RESULT_LOG> REQUEST_RESULT_LOGs { get; set; }
        public DbSet<REQUEST_QUERTY_CATEGORY> REQUEST_QUERTY_CATEGORYs { get; set; }
    }
}
