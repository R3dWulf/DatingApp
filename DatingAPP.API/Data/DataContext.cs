using DatingAPP.API.Models;
using DatingAPP.API.Modules;
using Microsoft.EntityFrameworkCore;

namespace DatingAPP.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Like>()
                .HasKey( k => new { k.LikerId, k.LikeeId });

            builder.Entity<Like>()
                .HasOne( u => u.Likee )
                .WithMany( u => u.Liker )
                .HasForeignKey( u => u.LikerId )
                .OnDelete( DeleteBehavior.Restrict );

            builder.Entity<Like>()
                .HasOne( u => u.Liker )
                .WithMany( u => u.Likee )
                .HasForeignKey( u => u.LikeeId )
                .OnDelete( DeleteBehavior.Restrict );
        }
        
    }
}