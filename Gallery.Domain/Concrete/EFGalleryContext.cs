using Gallery.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Domain.Concrete
{
   public class EFGalleryContext:DbContext
    {
        public EFGalleryContext():base("MyGallery")
        {
            
        }
       public DbSet<Image> Images { get; set; }
       public DbSet<Album> Albums { get; set; }
    }
}
