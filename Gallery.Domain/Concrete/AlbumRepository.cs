using Gallery.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.Domain.Entity;

namespace Gallery.Domain.Concrete
{
   public class AlbumRepository : IAlbumRepository
    {
        private EFGalleryContext _context = new EFGalleryContext(); 
        public void Add(Album newAlbum)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Album> All()
        {
            return _context.Albums;
        }

        public void Delete(Album albumToDelete)
        {
            throw new NotImplementedException();
        }

        public Album GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Album albumToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
