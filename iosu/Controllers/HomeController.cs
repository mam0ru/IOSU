using System;
using System.Web.Mvc;
using iosu.Entities;
using iosu.Interfaces.DAO;

namespace iosu.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository StoreRepository;

        public HomeController(IStoreRepository storeRepository)
        {
            StoreRepository = storeRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public String GetCash()
        {
            Store store = StoreRepository.GetCash();
            return store.Cash.ToString();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}