namespace MyBlog.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyBlogDBModel : DbContext
    {
        public MyBlogDBModel()
            : base("name=MyBlogDBModel")
        {
        }

        public virtual DbSet<BestMoment> BestMoments { get; set; }
        public virtual DbSet<DailyPost> DailyPosts { get; set; }
        public virtual DbSet<previewTrip> previewTrips { get; set; }
        public virtual DbSet<SlideImage> SlideImages { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<userInfo> userInfoes { get; set; }
        public virtual DbSet<userRole> userRoles { get; set; }
        public virtual DbSet<GalleryHome> GalleryHomes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<user>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.useremail)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasOptional(e => e.userInfo)
                .WithRequired(e => e.user);

            modelBuilder.Entity<userInfo>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<userRole>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<userRole>()
                .HasMany(e => e.users)
                .WithOptional(e => e.userRole)
                .HasForeignKey(e => e.role_id);
        }
    }
}
