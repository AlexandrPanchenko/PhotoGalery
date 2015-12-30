using Gallery.Domain.Abstract;
using Gallery.Domain.Entity;
using LoginExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginExample.Controllers
{
    public class HomeController : Controller
    {
        private IImageRepository _imageRepository;
        private IAlbumRepository _albumRepository;
        int pageSize = 3;

      
        public HomeController(IImageRepository imageRepository, IAlbumRepository albumRepository)
        {
            _imageRepository = imageRepository;
            _albumRepository = albumRepository;
        }
        public ActionResult Index(string search = null,int page = 1)
        {
            ImageViewModel model;
            if (search == null)
            {
                model = new ImageViewModel()
                {

                    Albums = _albumRepository.All().OrderBy(x => x.id)
                                   .Skip((page - 1) * pageSize)
                                   .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = _albumRepository.All().Count()
                    }
                };
            }
            else
            {
                model = new ImageViewModel()
                {

                    Albums = _albumRepository.All().Where(x=>x.Name==search).OrderBy(x => x.id),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = _albumRepository.All().Count()
                    }
                };
            }
           
            ViewBag.Image = _imageRepository.All().Where(x=>x.ImageStatus==Status.Main);
            return View(model);
        }

        //public ActionResult AlbumInfo()
        //{
        //    ViewBag.Album = _albumRepository.All();
        //    return View(_imageRepository.All());
        //
        public ActionResult AlbumInfo(int id)
        {
            IEnumerable<Image> images = _imageRepository.All().Where(x => x.AlbumId == id);
            return View(images);
        }

        public ActionResult FullScreen(int id)
        {
            Image image = _imageRepository.All().First(x => x.id == id);
            return View(image);
        }

        // [Authorize(Roles = "admin")] //Ограничить доступ всем кроме админа
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Users()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                users = db.Users.ToList();
            }

            return View(users);
        }

        public ActionResult AlbumsDetail(string id)
        {
            IEnumerable<Album> albums = _albumRepository.All().Where(x=>x.UserId==id);

            return View(albums);
        }

        //   [Authorize] // Доступ всем кто зарегистрировался
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}