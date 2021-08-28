using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.ViewModel;

namespace TestApplication.Controllers
{
    public class HotelTestController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HotelTestController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Test> test = _db.Tests.ToList();

            List<Hotel> hotel = _db.Hotels.ToList();

            HotelsAndTests hotelsAndTests = new HotelsAndTests();
            hotelsAndTests.tests = test;
            hotelsAndTests.hotels = hotel;



            return View(hotelsAndTests);
        }

        //public IActionResult Run(int hotelId, int testId)
        //{
        //    _db.HotelTests.Add(new HotelTest
        //    {
        //        HotelId = hotelId,
        //        TestId = testId
        //    });
        //    return Redirect("Run");
        //}


        [HttpPost]
        public IActionResult Go( int hotelId, int testId)
        {

            //int AdultsNumber = 1;
            //int ChildrenNumber = 1;

            //int[] childAge = new int[] { 10, 4, 7 };
            //string[] childNames = new string[] { "Ali", "Veli", "Mehmet" };
            //string[] childSurnames = new string[] { "Gündüz", "Gece", "Yıldız" };
            //string[] adultNames = new string[] { "Elif", "Ekin", "Sema" };
            //string[] adultSurnames = new string[] { "Çay", "Kahve", "Şeker" };

            List<int> list1 = new List<int> { 10, 4, 7 };
            List<string> list2 = new List<string> { "Ali", "Veli", "Mehmet" };
            List<string> list3 = new List<string> { "Gündüz", "Gece", "Yıldız" };
            List<string> list4 = new List<string> { "Elif", "Ekin", "Sema" };
            List<string> list5 = new List<string> { "Çay", "Kahve", "Şeker" };

            Hotel hotel = _db.Hotels.Find(hotelId);
            Test test = _db.Tests.Find(testId);
            string hotelURL = hotel.HotelUrl;
            _db.Results.Add(new Result
            {
                HotelId = hotelId,
                TestId = testId
            });
            _db.SaveChanges();

            var bService = new TestApplication.Services.BookingTesterService();
            bService.TestBooking(hotelURL, 1, 3, list1, list2, list3, list4, list5);
            return RedirectToAction("Index","Result");
        }
    }
}
