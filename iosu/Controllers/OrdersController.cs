using System.Net;
using System.Web.Mvc;
using iosu.Entities;
using iosu.Interfaces.ResponseHelpers;
using iosu.Models;

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
            var orders = OrdersResponseHelper.LoadAll();
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

        public ActionResult Create()
        {
            //            OrderCreateViewModel add and use
            var order = OrdersResponseHelper.GetOrder(0);
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

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderRequestModel order = OrdersResponseHelper.GetOrder(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductIds,Amount,Price,PartnerIds,OrderType")] OrderResponseModel orderResponse)
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
    }
}
