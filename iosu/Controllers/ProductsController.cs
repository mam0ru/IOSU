using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using iosu.Entities;
using iosu.Interfaces.ResponseHelpers;
using iosu.Models;

namespace iosu.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductResponseHelper ProductResponseHelper;

        public ProductsController(IProductResponseHelper productResponseHelper)
        {
            ProductResponseHelper = productResponseHelper;
        }

        public ActionResult Index()
        {
            var products = ProductResponseHelper.LoadAll();
            return View(products);
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = ProductResponseHelper.GetEntityById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductsCreateViewModel productVM = ProductResponseHelper.GetProduct(0);
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ManufacturerIds,UnitPrice,Amount")] ProductsCreateModel product)
        {
            ProductResponseHelper.Validate(ModelState, product);
            if (ModelState.IsValid)
            {
                ProductResponseHelper.SaveProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsCreateViewModel product = ProductResponseHelper.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ManufacturerIds,UnitPrice,Amount")] ProductsCreateModel product)
        {
            if (ModelState.IsValid)
            {
                ProductResponseHelper.SaveProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = ProductResponseHelper.GetEntityById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                ProductResponseHelper.Delete(id);
            }
            catch (Exception)
            {
                Product product = ProductResponseHelper.GetEntityById(id);
                product.CantDelete = true;
                return View(product);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Report()
        {
            IEnumerable<Partner> reportInfo = ProductResponseHelper.GetReportInfo();
            return View(reportInfo);
        }

        [HttpPost]
        public ActionResult Hn9([Bind(Include = "hn9")] String hn9)
        {
            ProductResponseHelper.AddColumn(hn9);
            return RedirectToAction("Index");
        }
    }
}
