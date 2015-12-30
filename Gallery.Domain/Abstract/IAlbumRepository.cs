using Gallery.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Domain.Abstract
{
   public interface IAlbumRepository
    {
        IEnumerable<Album> All();
        Album GetById(int id);
    
        void Add(Album newAlbum);
        void Delete(Album albumToDelete);
        void Update(Album albumToUpdate);
    }
}
