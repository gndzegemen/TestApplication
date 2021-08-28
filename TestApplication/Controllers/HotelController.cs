using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApplication.Controllers
{
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HotelController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Hotel> objList = _db.Hotels.ToList();

            return View(objList);
        }
        public IActionResult Update(int? id)
        {
            Hotel obj = new Hotel();
            if (id == null)
            {
                return View(obj);
            }
            obj = _db.Hotels.FirstOrDefault(a => a.HotelId == id);

            if (obj == null)
            {
                return NotFound();

            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]


        public IActionResult Update(Hotel obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.HotelId == 0)
                {
                    //Cretate
                    _db.Hotels.Add(obj);

                }
                else
                {
                    //Update
                    _db.Hotels.Update(obj);

                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);

        }

        public IActionResult Delete(int id)
        {
            var objDb = _db.Hotels.FirstOrDefault(x => x.HotelId == id);
            _db.Hotels.Remove(objDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddMultipleData()
        {
            List<Hotel> HotelList = new List<Hotel>();

            for (int i = 1; i <= 5; i++)
            {
                HotelList.Add(new Hotel { HotelName = Guid.NewGuid().ToString(), HotelUrl= Guid.NewGuid().ToString() });
            }

            _db.Hotels.AddRange(HotelList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteMultipleData()
        {
            IEnumerable<Hotel> HotelList = _db.Hotels.OrderByDescending(a => a.HotelId).Take(5).ToList();

            

            _db.Hotels.RemoveRange(HotelList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
