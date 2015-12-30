using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace Gallery.Domain.Entity
{
    public class Album
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string titleImagePath { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
   
        [HiddenInput(DisplayValue = false)]
        public DateTime DateCreate { get; set; }
        [HiddenInput]
        public string UserId { get; set; }
        public Status AlbumStatus { get; set; }
        public virtual IEnumerable<Image> Images { get; set; }
    }
}
