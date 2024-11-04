
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identidade.Infra.Data
{
    public class ApplicationDataContext : IdentityDbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options) { }
    }
}
