using System.Web.Mvc;
using iosu.Entities;
using iosu.Models.View;

namespace iosu.Interfaces.ResponseHelpers
{
    public interface IPartnerResponseHelper: IBaseResponseHelper<Partner>
    {
        PartnerDetailsViewModel GetPartnerDetailsViewModel(long? id);
        void Validate(ModelStateDictionary modelState, Partner partner);
    }
}
