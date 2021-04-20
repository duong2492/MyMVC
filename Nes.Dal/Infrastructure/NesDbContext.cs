using Nes.Common;
using Nes.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nes.Dal.Infrastructure
{
    public class NesDbContext : DbContext
    {
        public NesDbContext()
            : base(SystemConsts.NES_CONNECTION_STRING)
        {
            Database.SetInitializer<NesDbContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        //core entity
        public DbSet<Function> Functions { set; get; }
        public DbSet<Language> Languages { set; get; }
        public DbSet<Permission> Permissions { set; get; }
        public DbSet<Role> Roles { set; get; }
        public DbSet<SystemConfig> SystemConfigs { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<UserActivityLog> UserActivityLogs { set; get; }
        public DbSet<UserGroup> UserGroups { set; get; }
        public DbSet<UserRole> UserRoles { set; get; }

        //cms entity
        public DbSet<Category> Categories { set; get; }
        public DbSet<News> Newses { set; get; }
        public DbSet<NewsTag> NewsTags { set; get; }
        public DbSet<Tag> Tags { set; get; }

        public DbSet<MenuType> MenuTypes { set; get; }
        public DbSet<Menu> Menus { set; get; }
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<GroupSlide> GroupSlides { set; get; }
        public DbSet<Slide> Slides { set; get; }

        //product
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }

        //photos
        public DbSet<Album> Albums { set; get; }
        public DbSet<Photo> Photos { set; get; }
        public DbSet<Footer> Footers { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }
        public DbSet<About> Abouts { set; get; }

        public DbSet<Test> Test { set; get; }
    }
}
