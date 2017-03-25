using DealerConceptsApp.Domain;
using DealerConceptsApp.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Services.Interfaces
{
    public interface IDealerAccountInfoService
    {
        int CreateDealerAccountInfo(DealerAccountInfoCreateRequest newAccount, string userId);

        void UpdateDealerAccountInfo(int id, DealerAccountInfoUpdateRequest newAccount);

        void DeleteDealerAccountInfo(int id);

        DealerAccountInfo SelectById(int id);

        DealerAccountInfo SelectByUserId(string currentUser);

        List<DealerAccountInfo> GetAllDealerAccountInfo();

        DealerAccountInfo SelectByEmail(string email);
    }
}