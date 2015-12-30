using Gallery.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Domain.Abstract
{
    public interface IImageRepository
    {
        IEnumerable<Image> All();
        Image GetById(int id);
        
        void Add(Image newImage);
        void Delete(Image imageToDelete);
        void Update(Image imageToUpdate);
    }
}
