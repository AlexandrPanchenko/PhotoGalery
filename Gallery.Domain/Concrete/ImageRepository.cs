using Gallery.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.Domain.Entity;

namespace Gallery.Domain.Concrete
{
   public class ImageRepository : IImageRepository
    {
        private EFGalleryContext _context = new EFGalleryContext();
        public void Add(Image newImage)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Image> All()
        {
            return _context.Images;
        }

        public void Delete(Image imageToDelete)
        {
            throw new NotImplementedException();
        }

        public Image GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Image imageToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
