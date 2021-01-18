using Bookah.Data.Interfaces;
using Bookah.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bookah.Web.Controllers
{
    public class BookController : Controller
    {
        IBookRepo BookRepo;


        public BookController(IBookRepo bookRepo)
        {
            BookRepo = bookRepo;
        }
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadBook()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadBook(Book book, HttpPostedFileBase file)
        {
            FileStream stream;
            if (file != null && file.ContentLength > 0)
            {
                string path = Path.Combine(Server.MapPath("~/Content/"), file.FileName);
                file.SaveAs(path);
                //var stream = new MemoryStream(Encoding.ASCII.GetBytes(path));
                stream = new FileStream(Path.Combine(path), FileMode.Open);
                await Task.Run(() => BookRepo.Upload(stream, file.FileName));
                book.file = path;


            }

            try
            {
                if (ModelState.IsValid)
                {
                    await BookRepo.Create(book);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(book);

        }

    }
}