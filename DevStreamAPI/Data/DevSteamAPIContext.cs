using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevStreamAPI.Data
{
    public class DevSteamAPIContext : IdentityDbContext
    {
        //metodo construtor
        public DevSteamAPIContext(DbContextOptions<DevSteamAPIContext> options) :
            base(options)
        { }
        //sobscrever o metodo OnModeCreating
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
        }
    }
}
