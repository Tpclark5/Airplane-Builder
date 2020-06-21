using PlaneBuilder.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneBuilder.Services
{
    public interface IGetEmailAddress
    {
        Task<IEnumerable<LoginViewModel>> DisplayAllEmailAddresses();
    }
}
