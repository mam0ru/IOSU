using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using iosu.Entities;
using iosu.Interfaces.ResponseHelpers;
using iosu.Models.View;

namespace iosu.Controllers
{
    public class PartnersController : Controller
    {
        private readonly IPartnerResponseHelper PartnerResponseHelper;

        public PartnersController(IPartnerResponseHelper partnerResponseHelper)
        {
            PartnerResponseHelper = partnerResponseHelper;
        }

        public ActionResult Index()
        {
            IList<Partner> partners = PartnerResponseHelper.LoadAll();
            return View(partners.ToList());
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PartnerDetailsViewModel viewModel = PartnerResponseHelper.GetPartnerDetailsViewModel(id);

            if (viewModel == null || viewModel.Partner.Contact == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,Name,PartnerType,ContactId,Contact")]
            Partner partner)
        {
            PartnerResponseHelper.Validate(ModelState, partner);
            if (ModelState.IsValid)
            {
                PartnerResponseHelper.SaveOrUpdate(partner);
                return RedirectToAction("Details", "Partners", new { id = partner.Id });
            }
            return View(partner);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partner partner = PartnerResponseHelper.GetEntityById(id);
            if (partner == null)
            {
                return HttpNotFound();
            }
            return View(partner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PartnerType,ContactId,Contact")] Partner partner)
        {
            if (ModelState.IsValid)
            {
                partner = PartnerResponseHelper.SaveOrUpdate(partner);
                return RedirectToAction("Details","Partners",new {id = partner.Id});
            }
            return View(partner);
        }

        public ActionResult Delete(long? id)
        {
            var partnerCreateViewModel = PartnerResponseHelper.GetEntityById(id);
            return View(partnerCreateViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                PartnerResponseHelper.Delete(id);
            }
            catch (Exception)
            {
                Partner partnerCreateViewModel = PartnerResponseHelper.GetEntityById(id);
                partnerCreateViewModel.CantDelete = true;
                return View(partnerCreateViewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ChangePriceForProducts([Bind(Include = "percent,reduc,Partner_Id")] long? percent, long? reduc, long? Partner_Id)
        {
            if (percent.HasValue && Partner_Id.HasValue)
            {
                PartnerResponseHelper.IncreasProductsPrice(percent.Value, Partner_Id.Value);
            }
            else if (reduc.HasValue && Partner_Id.HasValue)
            {
                PartnerResponseHelper.ReducProductsPrice(reduc.Value, Partner_Id.Value);
            }

            return RedirectToAction("Index");
        }
    }
}
