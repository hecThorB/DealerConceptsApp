using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

using DealerConceptsApp.Classes;
using DealerConceptsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DealerConceptsApp.Models;
using Microsoft.Owin.Security;
using DealerConceptsApp.Models.Request;
using System.Data.SqlClient;
using System.Data;
using DealerConceptsApp.Classes.Exceptions;
using DealerConceptsApp.Domain;
using WikiDataProvider.Data.Extensions;

namespace DealerConceptsApp.Services
{
    public class UserService : BaseService , IUserService, IAdminUserService
    {
        private IMessageService _messageService;
        private IConfigService _config;
        private static Dictionary<string, string> _roles;

        public UserService(IMessageService messageService, IConfigService config)
        {
            _messageService = messageService;
            _config = config;
        }

        #region - IAdminUserService -

        public AdminUser MapAdminUsers(IDataReader reader)
        {
            AdminUser currentUser = new AdminUser();

            int startingIndex = 0; //startingOrdinal
            currentUser.Id = reader.GetSafeInt32(startingIndex++);
            currentUser.UserId = reader.GetSafeString(startingIndex++);
            currentUser.DateCreated = reader.GetSafeDateTime(startingIndex++);
            currentUser.DateModified = reader.GetSafeDateTime(startingIndex++);
            currentUser.FirstName = reader.GetSafeString(startingIndex++);
            currentUser.LastName = reader.GetSafeString(startingIndex++);
            currentUser.Email = reader.GetSafeString(startingIndex++);
            currentUser.PhoneNumber = reader.GetSafeString(startingIndex++);
            currentUser.IsBlocked = reader.GetSafeBool(startingIndex++);
            return currentUser;
        }

        //GET ALL
        public List<AdminUser> GetAllUsers()
        {
            List<AdminUser> list = null;
            DataProvider.ExecuteCmd(
                GetConnection,
                "dbo.ProfileAccountInfo_SelectAll",
                inputParamMapper: delegate (SqlParameterCollection parameters)
                {
                },
                map: delegate (IDataReader reader, short set)
                {
                    AdminUser currentUser = MapAdminUsers(reader);
                    if (list == null)
                    {
                        list = new List<AdminUser>();
                    }
                    list.Add(currentUser);
                });
            return list;
        }

        //UPDATE
        public void UpdateUser(AdminUserUpdateRequest existingUser)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "[dbo].[AdminUserIsBlocked_Update]",
                inputParamMapper: delegate (SqlParameterCollection parameters)
                {
                    parameters.AddWithValue("@Id", existingUser.Id);
                    parameters.AddWithValue("@IsBlocked", existingUser.IsBlocked);
                });
        }

        //PAGINATION
        public PagedList<AdminUser> GetUsersByPageIndex(int pageIndex, int itemsPerPage)
        {
            PagedList<AdminUser> pagedList = null;
            List<AdminUser> UsersList = null;
            int totalCount;

            DataProvider.ExecuteCmd(
                GetConnection,
                "[dbo].[Dealer_Account_SelectAll_for_Pagination]",
                inputParamMapper: delegate (SqlParameterCollection parameters)
                {
                    parameters.AddWithValue("@PageIndex", pageIndex);
                    parameters.AddWithValue("@NumberOfRecords", itemsPerPage);

                },
                map: delegate (IDataReader reader, short set)
                {
                    AdminUser userInfo = new AdminUser();
                    int startingIndex = 0;

                    userInfo.Id = reader.GetSafeInt32(startingIndex++);
                    userInfo.UserId = reader.GetSafeString(startingIndex++);
                    userInfo.DateCreated = reader.GetSafeDateTime(startingIndex++);
                    userInfo.DateModified = reader.GetSafeDateTime(startingIndex++);
                    userInfo.FirstName = reader.GetSafeString(startingIndex++);
                    userInfo.LastName = reader.GetSafeString(startingIndex++);
                    userInfo.Email = reader.GetSafeString(startingIndex++);
                    userInfo.PhoneNumber = reader.GetSafeString(startingIndex++);
                    userInfo.IsBlocked = reader.GetSafeBool(startingIndex++);
                    totalCount = reader.GetSafeInt32(startingIndex++);

                    if (UsersList == null)
                    {
                        UsersList = new List<AdminUser>();
                    }
                    UsersList.Add(userInfo);
                    if (pagedList == null)
                    {
                        pagedList = new PagedList<AdminUser>(UsersList, pageIndex, itemsPerPage, totalCount);
                    }
                });
            return pagedList;
        }

        //PAGINATION BY SEARCH
        public PagedList<AdminUser> GetUsersBySearchWord(int pageIndex, int itemsPerPage, string searchText)
        {
            PagedList<AdminUser> searchPagedList = null;
            List<AdminUser> SearchList = null;
            int totalCount;

            DataProvider.ExecuteCmd(
                GetConnection,
                "[dbo].[Profile_Account_Search]",
                inputParamMapper: delegate (SqlParameterCollection parameters)
                {
                    parameters.AddWithValue("@PageIndex", pageIndex);
                    parameters.AddWithValue("@NumberOfRecords", itemsPerPage);
                    parameters.AddWithValue("@SearchText", searchText);
                },
                map: delegate (IDataReader reader, short set)
                {
                    AdminUser searchInfo = new AdminUser();
                    int startingIndex = 0;

                    searchInfo.Id = reader.GetSafeInt32(startingIndex++);
                    searchInfo.UserId = reader.GetSafeString(startingIndex++);
                    searchInfo.DateCreated = reader.GetSafeDateTime(startingIndex++);
                    searchInfo.DateModified = reader.GetSafeDateTime(startingIndex++);
                    searchInfo.FirstName = reader.GetSafeString(startingIndex++);
                    searchInfo.LastName = reader.GetSafeString(startingIndex++);
                    searchInfo.Email = reader.GetSafeString(startingIndex++);
                    searchInfo.PhoneNumber = reader.GetSafeString(startingIndex++);
                    searchInfo.IsBlocked = reader.GetSafeBool(startingIndex++);
                    totalCount = reader.GetSafeInt32(startingIndex++);

                    if (SearchList == null)
                    {
                        SearchList = new List<AdminUser>();
                    }
                    SearchList.Add(searchInfo);
                    if (searchPagedList == null)
                    {
                        searchPagedList = new PagedList<AdminUser>(SearchList, pageIndex, itemsPerPage, totalCount);
                    }
                });
            return searchPagedList;
        }

        //CREATE ROLE ID
        public string CreateRoleId(AspNetUserRoleCreateRequest newRoleId)
        {
            string result = null;
            DataProvider.ExecuteNonQuery(GetConnection, "Asp_Net_User_Role_Insert",
                inputParamMapper: delegate (SqlParameterCollection parameters)
                {
                    parameters.AddWithValue("@UserId", newRoleId.UserId);
                    parameters.AddWithValue("@RoleId", newRoleId.RoleId);
                    parameters.AddWithValue("@IsShopper", newRoleId.IsSalesPerson);


                },
                returnParameters: delegate (SqlParameterCollection parameters)
                {
                    result = parameters["@UserId"].Value.ToString();
                });
            return result;
        }

        //REMOVE ROLE ID
        public void DeleteSalesPerson(string userId, string roleId, bool isShopper)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Asp_Net_User_Role_Delete",
                inputParamMapper: delegate (SqlParameterCollection parameters)
                {
                    parameters.AddWithValue("@UserId", userId);
                    parameters.AddWithValue("@RoleId", roleId);
                    parameters.AddWithValue("@IsSalesPerson", isShopper);
                });
        }

        public List<string> GetSiteEmails()
        {

            List<string> lst = null;

            DataProvider.ExecuteCmd(
                GetConnection,
                "[dbo].[Dealer_Account_Search]",
                inputParamMapper: delegate (SqlParameterCollection parameters)
                {

                },
                map: delegate (IDataReader reader, short set)
                {

                    int startingIndex = 0;


                    string email = reader.GetSafeString(startingIndex);

                    if (lst == null)
                    {
                        lst = new List<string>();
                    }

                    lst.Add(email);

                });

            return lst;
        }


        #endregion

        #region - IUserService -

        private static ApplicationUserManager GetUserManager()
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public void SendConfirmation(ApplicationUser newUser)
        {
            MessageCreateRequest messageModel = new MessageCreateRequest();
            Guid token = Guid.NewGuid();

            messageModel.ToAddress = newUser.Email;
            messageModel.Subject = "Dealer Concepts Registration Confirmation";

            _messageService.SendConfirmation(messageModel, token);

            AddUserToken(token, newUser);
        }

        public IdentityUser CreateUser(string email, string password, string roleName = "Consumer")
        {
            ApplicationUserManager userManager = GetUserManager();

            ApplicationUser newUser = new ApplicationUser { UserName = email, Email = email, LockoutEnabled = false };

            if (roleName != null)
            {
                IdentityUserRole role = new IdentityUserRole();

                string roleId = Roles[roleName];

                role.RoleId = roleId;
                role.UserId = newUser.Id;
                newUser.Roles.Add(role);
            }



            IdentityResult result = null;

            try
            {
                result = userManager.Create(newUser, password);

            }
            catch (Exception e)
            {
                throw e;
            }

            if (result.Succeeded)
            {


                /*-----NEW TEST DRIVE EMAIL TO ADMIN------*/
                MessageCreateRequest newUserAdminMsg = new MessageCreateRequest();

                newUserAdminMsg.ToAddress = _config.SiteAdminEmail;
                newUserAdminMsg.Subject = "You have a new Found!t Customer";

                _messageService.NewCustomerToAdmin(newUserAdminMsg, email);


                /*-----CONFRIMATION EMAIL-----*/
                SendConfirmation(newUser);

                return newUser;
            }
            else
            {
                throw new IdentityResultException(result);
            }
        }

        public int AddUserToken(Guid token, ApplicationUser newUser)
        {
            int result = -1;
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.UserGuidTokens_Insert",
                    inputParamMapper: delegate (SqlParameterCollection parameters)
                    {
                        parameters.AddWithValue("@Guid", token);
                        parameters.AddWithValue("@UserId", newUser.Id);

                        parameters.Add(new SqlParameter
                        {
                            DbType = DbType.Int32,
                            Direction = ParameterDirection.Output,
                            ParameterName = "@Id"
                        });
                    },
                    returnParameters: delegate (SqlParameterCollection parameters)
                    {
                        result = int.Parse(parameters["@Id"].Value.ToString());
                    }
                );
            return result;
        }

        public string UpdateConfirmation(Guid token)
        {
            string userId = null;

            userId = GetUserId(token);

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsers_UpdateIsConfirmed",
             inputParamMapper: delegate (SqlParameterCollection parameters)
             {
                 parameters.AddWithValue("@Id", userId);
             }
            );

            DeleteToken(token);

            return userId;
        }

        private static void DeleteToken(Guid token)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.UserGuidTokens_UpdateIsDeleted",

                inputParamMapper: delegate (SqlParameterCollection parameters)
                {
                    parameters.AddWithValue("@Guid", token);
                });
        }

        public bool IsValidToken(Guid token)
        {
            string userId = GetUserId(token);

            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public ApplicationUser GetUserByToken(Guid token)
        {
            string userId = GetUserId(token);

            var user = GetUserById(userId);

            return user;
        }

        public string GetUserId(Guid token)
        {
            string userId = null;
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.UserGuidTokens_SelectByToken"
               , inputParamMapper: delegate (SqlParameterCollection parameters)
               {
                   parameters.AddWithValue("@Guid", token);

                   SqlParameter p = new SqlParameter("@userId", System.Data.SqlDbType.NVarChar);
                   p.Direction = System.Data.ParameterDirection.Output;

                   p.Size = 128;
                   parameters.Add(p);
               },
                returnParameters: delegate (SqlParameterCollection parameters)
                {
                    userId = parameters["@userId"].Value.ToString();
                }
            );
            return userId;
        }

        public Dictionary<string, string> Roles
        {
            get
            {
                if (_roles == null)
                {
                    // not bothering to lock shared resource - not a big cost

                    var context = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
                    var rolesSet = context.Set<IdentityRole>();
                    _roles = rolesSet.ToDictionary(r => r.Name, r => r.Id);
                }
                return _roles;
            }
        }

        public IList<string> GetCurrentRoles()
        {
            return GetRoles(GetCurrentUserId());
        }

        public IList<string> GetRoles(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return null;

            }

            ApplicationUserManager userManager = GetUserManager();

            return userManager.GetRoles(userId);
        }

        public static bool IsUser(string emailaddress)
        {
            bool result = false;

            ApplicationUserManager userManager = GetUserManager();
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            ApplicationUser user = userManager.FindByEmail(emailaddress);


            if (user != null)
            {

                result = true;

            }

            return result;
        }

        public ApplicationUser GetUser(string emailaddress)
        {
            ApplicationUserManager userManager = GetUserManager();
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            ApplicationUser user = userManager.FindByEmail(emailaddress);

            return user;
        }

        public ApplicationUser UpdateUserById(string email)
        {
            ApplicationUserManager userManager = GetUserManager();
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            ApplicationUser user = userManager.FindByEmail(email);

            MessageCreateRequest messageModel = new MessageCreateRequest();
            Guid token = Guid.NewGuid();

            messageModel.ToAddress = email;
            messageModel.Subject = "Dealer Concepts Registration Password Recovery";

            _messageService.RecoverEmail(messageModel, token);

            AddUserToken(token, user);

            return user;
        }

        public ApplicationUser GetUserById(string userId)
        {

            ApplicationUserManager userManager = GetUserManager();
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            ApplicationUser user = userManager.FindById(userId);

            return user;
        }

        public bool ChangePassword(string userId, string newPassword)
        {
            bool result = false;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(newPassword))
            {
                throw new Exception("You must provide a userId and a password");
            }

            ApplicationUser user = GetUserById(userId);

            if (user != null)
            {

                ApplicationUserManager userManager = GetUserManager();

                userManager.RemovePassword(userId);
                IdentityResult res = userManager.AddPassword(userId, newPassword);

                result = res.Succeeded;

            }

            return result;
        }

        public bool Logout()
        {
            bool result = false;

            IdentityUser user = GetCurrentUser();

            if (user != null)
            {
                IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                result = true;
            }

            return result;
        }

        public IdentityUser GetCurrentUser()
        {
            if (!IsLoggedIn())
                return null;

            string userId = GetCurrentUserId();


            IdentityUser currentUserId = GetUserById(userId);
            return currentUserId;
        }

        public string GetCurrentUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }


        public bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(GetCurrentUserId());

        }

        #endregion
    }
}