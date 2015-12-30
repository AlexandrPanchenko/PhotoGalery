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
using LoginExample.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using Microsoft.AspNet.Identity.Owin;
namespace LoginExample.Views
{
    public class AlbumsController : Controller
    {
        string currentUserId; 
        private EFGalleryContext db = new EFGalleryContext();
       
       
        // GET: Albums
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

            if (roles.Contains("admin")) return View(db.Albums);
            return View(await db.Albums.Where(x => x.UserId == currentUserId).ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = await db.Albums.FindAsync(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Name,titleImagePath,Description,DateCreate,UserId,AlbumStatus")] Album album, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file!=null)
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
                album.DateCreate = DateTime.Now;
                album.titleImagePath = "/Image/" + fileName;
                currentUserId = User.Identity.GetUserId();
                album.UserId = currentUserId;
                db.Albums.Add(album);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = await db.Albums.FindAsync(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Name,titleImagePath,Description,DateCreate,UserId,AlbumStatus")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = await db.Albums.FindAsync(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Album album = await db.Albums.FindAsync(id);
            db.Albums.Remove(album);
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
