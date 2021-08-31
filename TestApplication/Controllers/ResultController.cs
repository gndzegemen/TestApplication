using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
namespace TestApplication.Controllers
{
    public class ResultController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ResultController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Result> objList = _db.Results.Include(h=>h.Hotel).Include(t=>t.Test).ToList();

            

            return View(objList);
        }
    }
}
