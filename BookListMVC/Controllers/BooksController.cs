using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDBContext _db;

        public BooksController(ApplicationDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public IActionResult Index() //This is the index action from the __layout file
        {
            // we dont want to pass anything in cuz we want to use the API from the datatable plugin
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert() //create an object
        {
            if (ModelState.IsValid)
            {
                if (Book == null)
                {
                    //create
                    _db.Books.Add(Book);
                }
                else
                {
                    //edit
                    _db.Books.Update(Book);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(Book);
            }
        }

        // View Handler
        public IActionResult Upsert(int? id) 
        {
            Book = new Book();
            if(id == null)
            {
                //create
                return View(Book);
            }
            else
            {
                //edit
                var Book = _db.Books.FirstOrDefault(u => u.Id == id);
                if(Book == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(Book); //this returns a view regardless of whther it is for update or create, we handle the actual view else where
                }
            }
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Books.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var book = _db.Books.FirstOrDefault(u=>u.Id==id);
            if(book == null)
            {
                return Json(new { success=false, message= "Error while deleting" });
            }
            else
            {
                _db.Remove(book);
                await _db.SaveChangesAsync();
                return Json(new { success = true, message = "Deleted successfully" });

            }
        }
        #endregion


    }
}
