using Gallery.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Gallery.Domain.Concrete
{
   public class Initializer : DropCreateDatabaseIfModelChanges<EFGalleryContext>
    {

        protected override void Seed(EFGalleryContext context)
        {

            base.Seed(context);
            Album album = new Album();
            album.Name = "Animal";
            album.Description = "This is my first album , I took pictures of beautiful animals that I have seen in my life";
            album.AlbumStatus = Status.Main;
            album.DateCreate = DateTime.Now;
            album.UserId = "e625159c-9b7e-420e-8813-e49f5497532a";
       

            var Image1 =  new Image { Name = "Cat", Countlike = 12, DateCreate = DateTime.Now, ImageStatus = Status.Main, ImagePath = "/Image/2.jpg", Album=album } ;
            var Image2 =  new Image { Name = "Carrot", Countlike = 22, DateCreate = DateTime.Now, ImageStatus = Status.Main, ImagePath = "/Image/3.jpg", Album = album } ;
            var Image3 =  new Image { Name = "Tiger", Countlike = 15, DateCreate = DateTime.Now, ImageStatus = Status.Main, ImagePath = "/Image/1.jpg", Album = album } ;
            context.Images.Add(Image1);
            context.Images.Add(Image2);
            context.Images.Add(Image3);
            album.Images = new List<Image> { Image1, Image2, Image3 };
           
            album.titleImagePath = "/Image/2.jpg";
            context.Albums.Add(album);

            Album album1 = new Album();
            album1.Name = "City";
            album1.Description = "This is my best photos . Photos of beautiful places in large cities";
            album1.AlbumStatus = Status.Main;
            album1.DateCreate = DateTime.Now;
            album1.UserId = "8d769093-3845-4733-b70c-010062513ca9";
          
            var Image4 = new Image { Name = "CityFeature", Countlike = 112, DateCreate = DateTime.Now, ImageStatus = Status.Main, ImagePath = "/Image/City1.jpg", Album = album1 } ;
            var Image5 = new Image { Name = "Winter", Countlike = 222, DateCreate = DateTime.Now, ImageStatus = Status.Main, ImagePath = "/Image/City2.jpg", Album = album1 } ;
            var Image6 = new Image { Name = "Panorama", Countlike = 135, DateCreate = DateTime.Now, ImageStatus = Status.Main, ImagePath = "/Image/City3.jpg", Album = album1 } ;
            context.Images.Add(Image4);
            context.Images.Add(Image5);
            context.Images.Add(Image6);
            album1.Images = new List<Image> { Image4, Image5, Image6 };
            album1.titleImagePath = "/Image/City2.jpg";
            context.Albums.Add(album1);
            context.SaveChanges();

        }

    }
}