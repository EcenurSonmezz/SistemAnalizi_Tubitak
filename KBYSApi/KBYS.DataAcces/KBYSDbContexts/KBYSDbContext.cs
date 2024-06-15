using Microsoft.EntityFrameworkCore;
using KBYS.Entities.Entities;

namespace KBYS.DataAcces.KBYSDbContexts
{
    public class KBYSDbContext : DbContext
    {
        public KBYSDbContext(DbContextOptions<KBYSDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<UserDisease> UserDiseases { get; set; }
        public DbSet<UserAllergy> UserAllergies { get; set; }
        public DbSet<UserMealRecord> UserMealRecords { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<NutritionalValue> NutritionalValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary keys and relationships
            modelBuilder.Entity<UserDisease>()
                .HasKey(ud => new { ud.UserId, ud.DiseaseId });

            modelBuilder.Entity<UserDisease>()
                .HasOne(ud => ud.User)
                .WithMany(u => u.UserDiseases)
                .HasForeignKey(ud => ud.UserId);

            modelBuilder.Entity<UserDisease>()
                .HasOne(ud => ud.Disease)
                .WithMany(d => d.UserDiseases)
                .HasForeignKey(ud => ud.DiseaseId);

            modelBuilder.Entity<UserAllergy>()
                .HasKey(ua => new { ua.UserId, ua.AllergyId });

            modelBuilder.Entity<UserAllergy>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAllergies)
                .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<UserAllergy>()
                .HasOne(ua => ua.Allergy)
                .WithMany(a => a.UserAllergies)
                .HasForeignKey(ua => ua.AllergyId);

            modelBuilder.Entity<UserMealRecord>()
                .HasKey(umr => new { umr.UserId, umr.FoodId, umr.DateConsumed });

            modelBuilder.Entity<UserMealRecord>()
                .HasOne(umr => umr.User)
                .WithMany(u => u.UserMealRecords)
                .HasForeignKey(umr => umr.UserId);

            modelBuilder.Entity<UserMealRecord>()
                .HasOne(umr => umr.Food)
                .WithMany()
                .HasForeignKey(umr => umr.FoodId);


            modelBuilder.Entity<NutritionalValue>()
                .HasOne(nv => nv.Food)
                .WithMany(f => f.NutritionalValues)
                .HasForeignKey(nv => nv.FoodId);
        }
    }
}
