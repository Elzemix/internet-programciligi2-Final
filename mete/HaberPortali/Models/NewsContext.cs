using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HaberPortali.Models
{
    public class NewsContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public NewsContext(DbContextOptions<NewsContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<News>().HasData(new News() { NewsId = 1, NewsTitle = "Global Leaders Gather for Climate Summit in Paris", Date = "08.03.2024", IsActive = true });
         modelBuilder.Entity<News>().HasData(new News() { NewsId = 2, NewsTitle = "UNESCO Adds Historic Site to World Heritage List", Date = "13.01.2023", IsActive = false });
         modelBuilder.Entity<News>().HasData(new News() { NewsId = 3, NewsTitle = "Olympic Committee Faces Controversy Over Host City Selection", Date = "07.09.2024", IsActive = true });
         modelBuilder.Entity<News>().HasData(new News() { NewsId = 4, NewsTitle = "Renowned Author's Unpublished Manuscript Found After Decades", Date = "18.11.2023", IsActive = true });
        }

        public DbSet<News> News { get; set; }


    }
}
