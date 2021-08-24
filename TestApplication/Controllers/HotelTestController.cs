using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApplication.Controllers
{
    public class AllHotelsAndTests : Controller
    {
        private readonly ApplicationDbContext _db;
        public AllHotelsAndTests(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            List<AllHotelsAndTests> objList = _db..ToList();

            return View(objList);
        }

        //public IActionResult AddTestToHotel(int hotelId, int testId)
        //{
        //    _db.HotelTests.Add(new HotelTest
        //    {
        //        HotelId = hotelId,
        //        TestId = testId
        //    });
        //    return Redirect("");
        //}
    }
}
