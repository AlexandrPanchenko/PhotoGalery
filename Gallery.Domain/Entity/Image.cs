using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Gallery.Domain.Entity
{
    public enum Status{
        [Display(Name = "Главная страница")]
        Main,
        [Display(Name = "Архив")]
        Archive,
        [Display(Name = "Удаленная")]
        Delete
};
  public class Image
    {
        public int id { get; set; }
        public string Name { get; set; }
       
        [HiddenInput(DisplayValue = false)]
        public DateTime DateCreate { get; set; }
        [HiddenInput]
        public int Countlike { get; set; }
        public string ImagePath { get; set; }
        [HiddenInput]
        public string UserId { get; set; }
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
        public Status ImageStatus { get; set; }
    }
}
