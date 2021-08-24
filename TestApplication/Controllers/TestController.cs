using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApplication.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TestController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Test> objList = _db.Tests.ToList();

            return View(objList);
        }
        public IActionResult Update(int? id)
        {
            Test obj = new Test();
            if (id == null)
            {
                return View(obj);
            }
            obj = _db.Tests.FirstOrDefault(a => a.TestId == id);

            if (obj == null)
            {
                return NotFound();

            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Test obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.TestId == 0)
                {
                    //Cretate
                    _db.Tests.Add(obj);
                    
                }
                else
                {
                    //Update
                    _db.Tests.Update(obj);

                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);

        }

        public IActionResult Delete(int id)
        {
            var objDb = _db.Tests.FirstOrDefault(x => x.TestId == id);
            _db.Tests.Remove(objDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
