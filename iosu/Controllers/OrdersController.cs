using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using iosu.Entities;
using iosu.Interfaces.ResponseHelpers;
using iosu.Models;
using Newtonsoft.Json;

namespace iosu.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersResponseHelper OrdersResponseHelper;

        public OrdersController(IOrdersResponseHelper ordersResponseHelper)
        {
            OrdersResponseHelper = ordersResponseHelper;
        }

        public ActionResult Index()
        {
            IList<Order> orders = OrdersResponseHelper.LoadAll();
            return View(orders);
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = OrdersResponseHelper.GetEntityById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        public String GetPartners(String type)
        {
            IEnumerable<SelectListItem> partners = OrdersResponseHelper.GetPartners((OrderType)Enum.Parse(typeof(OrderType), type));
            return JsonConvert.SerializeObject(partners.ToArray());
        }

        [HttpPost]
        public String GetProducts(String orderType, long? partnerId)
        {
            IEnumerable<SelectListItem> products = OrdersResponseHelper.GetProducts((OrderType)Enum.Parse(typeof(OrderType), orderType), partnerId);
            return JsonConvert.SerializeObject(products.ToArray());
        }

        [HttpPost]
        public String GetProductPrice(long? productId)
        {
            long? productPrice = OrdersResponseHelper.GetProductPrice(productId);
            return JsonConvert.SerializeObject(productPrice);
        }

        [HttpGet]
        public ActionResult Create(long? partnerId)
        {
            OrderRequestModel order = OrdersResponseHelper.GetOrder(0, partnerId);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductIds,Amount,Price,PartnerIds,OrderType")] OrderResponseModel orderResponse)
        {
            OrdersResponseHelper.Validate(ModelState, orderResponse);
            if (ModelState.IsValid)
            {
                OrdersResponseHelper.SaveOrder(orderResponse);
                return RedirectToAction("Index");
            }
            OrderRequestModel orderRequest = OrdersResponseHelper.ConvertResponseObjectToRequestObject(orderResponse);
            return View(orderRequest);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = OrdersResponseHelper.GetEntityById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OrdersResponseHelper.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult BiggestOrders()
        {
            IEnumerable<Order> orders = OrdersResponseHelper.GetBigestOrders();
            return View("Index", orders);
        }

        [HttpPost]
        public ActionResult ReleasedInPeriod([Bind(Include = "from, to")] String from, String to)
        {
            IEnumerable<Order> orders = OrdersResponseHelper.GetReleasedInPeriod(from, to);
            return View("Index", orders);
        }

        [HttpPost]
        public ActionResult SearchOrdersByProductName([Bind(Include = "name")] String name)
        {
            IEnumerable<Order> orders = OrdersResponseHelper.SearchOrdersByProductName(name);
            return View("Index", orders);
        }

        [HttpPost]
        public ActionResult SearchOrdersByPartnerName([Bind(Include = "partnerName")] String partnerName)
        {
            IEnumerable<Order> orders = OrdersResponseHelper.SearchOrdersByPartnerName(partnerName);
            return View("Index", orders);
        }
    }
}
