using DealerConceptsApp.Classes;
using DealerConceptsApp.Domain;
using DealerConceptsApp.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerConceptsApp.Services.Interfaces
{
    interface IAdminUserService
    {
        List<AdminUser> GetAllUsers();
        void UpdateUser(AdminUserUpdateRequest existingUser);
        PagedList<AdminUser> GetUsersByPageIndex(int pageIndex, int itemsPerPage);
        PagedList<AdminUser> GetUsersBySearchWord(int pageIndex, int itemsPerPage, string searchText);
        string CreateRoleId(AspNetUserRoleCreateRequest newRoleId);
        void DeleteSalesPerson(string UserId, string RoleId, bool IsSalesPerson);
    }
}
