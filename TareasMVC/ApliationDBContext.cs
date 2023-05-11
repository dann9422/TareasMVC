using Microsoft.EntityFrameworkCore;

namespace TareasMVC
{
    public class ApliationDBContext : DbContext
    {
        public ApliationDBContext(DbContextOptions options): base(options)
        {

        }

    }
}
