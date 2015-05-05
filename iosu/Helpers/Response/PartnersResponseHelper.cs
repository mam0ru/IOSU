using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iosu.Entities;
using iosu.Enums;
using iosu.Interfaces.DAO;
using iosu.Interfaces.ResponseHelpers;
using iosu.Models.View;

namespace iosu.Helpers.Response
{
    public class PartnersResponseHelper: IPartnerResponseHelper
    {
        private readonly IPartnersRepository PartnersRepository;
        private readonly IProductResponseHelper ProductResponseHelper;
        private IOrdersResponseHelper OrdersResponseHelper;

        public PartnersResponseHelper(IPartnersRepository partnersRepository, IProductResponseHelper productResponseHelper, IOrdersResponseHelper ordersResponseHelper)
        {
            PartnersRepository = partnersRepository;
            ProductResponseHelper = productResponseHelper;
            OrdersResponseHelper = ordersResponseHelper;
        }

        public IList<Partner> LoadAll()
        {
            return PartnersRepository.GetAll().ToList();
        }

        public Partner GetEntityById(long? id)
        {
            return PartnersRepository.GetById(id);
        }

        public Partner SaveOrUpdate(Partner entity)
        {
            entity = PartnersRepository.SaveOrUpdate(entity);
            return entity;
        }

        public void Delete(long id)
        {
            PartnersRepository.Delete(id);
        }

        public PartnerDetailsViewModel GetPartnerDetailsViewModel(long? id)
        {
            var viewModel = new PartnerDetailsViewModel();
            viewModel.Partner = PartnersRepository.GetById(id);
            if (viewModel.Partner.PartnerType == PartnerType.Manufacturer)
            {
                viewModel.Products = ProductResponseHelper.LoadAll().Where(product => product.ManufacturerId == viewModel.Partner.Id);
            }
            viewModel.Orders = OrdersResponseHelper.LoadAll().Where(order => order.PartnerId == viewModel.Partner.Id);
            return viewModel;
        }

        public void Validate(ModelStateDictionary modelState, Partner partner)
        {
            if (partner.Name.Length == 0)
            {
                modelState.AddModelError("Name", "Hey! Name must be filled.");
            }
            if (partner.Contact.AgentFullName.Length == 0)
            {
                modelState.AddModelError("Contact.AgentFullName", "Hey! Agent Fullname must be filled.");
            }
            if (partner.Contact.Adress.Length == 0)
            {
                modelState.AddModelError("Contact.Adress", "Hey! Adress must be filled.");
            }
            if (partner.Contact.PhoneNumber.Length == 0)
            {
                modelState.AddModelError("Contact.PhoneNumber", "Hey! Phone Number must be filled.");
            }
        }
    }
}