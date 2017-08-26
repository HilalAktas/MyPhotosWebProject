using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyPhotos.Entities;


namespace MyPhotos.DataAccessLayer
{
  public class DatabaseContext:DbContext
    {


        public DbSet<MyPhotosUser> Users { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Liked> Likes { get; set; }
        public DbSet<Photo> Photos{ get; set; }
    }
}
