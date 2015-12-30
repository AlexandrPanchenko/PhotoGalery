using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gallery.Domain.Concrete;
using Gallery.Domain.Entity;
using Microsoft.AspNet.Identity;
using System.IO;
using LoginExample.Models;
using Microsoft.AspNet.Identity.Owin;

namespace LoginExample.Views
{
    public class ImagesController : Controller
    {

        
        private EFGalleryContext db = new EFGalleryContext();
        string currentUserId;
        
        // GET: Images
        public async Task<ActionResult> Index()
        {
            currentUserId = User.Identity.GetUserId();
            //////
            IList<string> roles = new List<string> { "Роль не определена" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            /////
            
            var images = db.Images.Include(i => i.Album);
            if (roles.Contains("admin")) return View(images);
            return View(await images.Where(x=>x.UserId==currentUserId).ToListAsync());
        }

        // GET: Images/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = await db.Images.FindAsync(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            ViewBag.AlbumId = new SelectList(db.Albums, "id", "Name");
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Name,DateCreate,Countlike,ImagePath,UserId,AlbumId,ImageStatus")] Image image, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                List<string> extensions = new List<string>() { ".jpg", ".png" };
                if (extensions.Contains(extension))
                {
                    file.SaveAs(Server.MapPath("/Image/" + fileName));
                    ViewBag.Message = "Файл сохранен";
                }
                else
                {
                    ViewBag.Message = "Ошибка расширения файлов ";
                }
                image.DateCreate = DateTime.Now;
                image.ImagePath = "/Image/" + fileName;
                currentUserId = User.Identity.GetUserId();
                image.UserId = currentUserId;
                db.Images.Add(image);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumId = new SelectList(db.Albums, "id", "Name", image.AlbumId);
            return View(image);
        }

        // GET: Images/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = await db.Images.FindAsync(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "id", "Name", image.AlbumId);
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Name,DateCreate,Countlike,ImagePath,UserId,AlbumId,ImageStatus")] Image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "id", "Name", image.AlbumId);
            return View(image);
        }

        // GET: Images/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = await db.Images.FindAsync(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Image image = await db.Images.FindAsync(id);
            db.Images.Remove(image);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
