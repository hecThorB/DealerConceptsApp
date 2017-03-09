using DealerConceptsApp.Domain;
using DealerConceptsApp.Services.Interfaces;
using DealerConceptsApp.Models.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WikiDataProvider.Data.Extensions;
using System.Data.SqlClient;

namespace DealerConceptsApp.Services
{
    public class DealerAccountInfoService : BaseService , IDealerAccountInfoService
    {
        private IUserService _userService;

        public DealerAccountInfoService(IUserService userService)
        {
            _userService = userService;
        }

        public static DealerAccountInfo MapProfile(IDataReader reader)
        {
            DealerAccountInfo account = new DealerAccountInfo();
            int startingIndex = 0;

            account.Id = reader.GetSafeInt32(startingIndex++);
            account.DateCreated = reader.GetSafeDateTime(startingIndex++);
            account.DateModified = reader.GetSafeDateTime(startingIndex++);
            account.UserId = reader.GetSafeString(startingIndex++);
            account.DealerName = reader.GetSafeString(startingIndex++);
            account.DealerType = reader.GetSafeInt32(startingIndex++);
            account.FirstName = reader.GetSafeString(startingIndex++);
            account.LastName = reader.GetSafeString(startingIndex++);
            account.Title = reader.GetSafeInt32(startingIndex++);
            account.Email = reader.GetSafeString(startingIndex++);
            account.PhoneNumber = reader.GetSafeString(startingIndex++);
            account.IsDeleted = reader.GetSafeBool(startingIndex++);

            return account;
        }

        public int CreateDealerAccountInfo(DealerAccountInfoCreateRequest newAccount, string userId)
        {
            int result = -1;
            DataProvider.ExecuteNonQuery(
                GetConnection, "[dbo].DealerRegistration_Insert",
                inputParamMapper: delegate (SqlParameterCollection parameters)
                {
                    parameters.AddWithValue("@UserId", "398091");
                    parameters.AddWithValue("@DealerName", newAccount.DealerName);
                    parameters.AddWithValue("@DealerType", newAccount.DealerType);
                    parameters.AddWithValue("@FirstName", newAccount.FirstName);
                    parameters.AddWithValue("@LastName", newAccount.LastName);
                    parameters.AddWithValue("@Title", newAccount.Title);
                    parameters.AddWithValue("@Email", newAccount.Email);
                    parameters.AddWithValue("@PhoneNumber", newAccount.PhoneNumber);

                    parameters.Add(new SqlParameter
                    {
                        DbType = System.Data.DbType.Int32,
                        Direction = System.Data.ParameterDirection.Output,
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

        public void UpdateDealerAccountInfo(int id, DealerAccountInfoUpdateRequest newAccount)
        {
            DataProvider.ExecuteNonQuery(
                GetConnection, "[dbo].DealerRegistration_Update",
                inputParamMapper: delegate (SqlParameterCollection parameters)
                {
                    parameters.AddWithValue("@DealerName", newAccount.DealerName);
                    parameters.AddWithValue("@DealerType", newAccount.DealerType);
                    parameters.AddWithValue("@FirstName", newAccount.FirstName);
                    parameters.AddWithValue("@LastName", newAccount.LastName);
                    parameters.AddWithValue("@Title", newAccount.Title);
                    parameters.AddWithValue("@Email", newAccount.Email);
                    parameters.AddWithValue("@PhoneNumber", newAccount.PhoneNumber);
                    parameters.AddWithValue("@Id", id);
                });          
        }

        public List<DealerAccountInfo> GetAllDealerAccountInfo()
        {
            List<DealerAccountInfo> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.DealerRegistration_SelectAll", inputParamMapper: EmptyParamMapper
                , map: (reader, set) =>
                {
                    DealerAccountInfo accountInfo = MapProfile(reader);

                    if (list == null)
                    {
                        list = new List<DealerAccountInfo>();
                    }

                    list.Add(accountInfo);
                }
                );

            return list;
        }

        private void EmptyParamMapper(SqlParameterCollection paramCollection)
        {

        }

        public DealerAccountInfo SelectById(int id)
        {
            DealerAccountInfo accountInfo = null;

            DataProvider.ExecuteCmd(
                GetConnection,
                "[dbo].[DealerRegistration_SelectById]",
                inputParamMapper: parameters => parameters.AddWithValue("@Id", id),

                map: delegate (IDataReader reader, short set)
                {
                    accountInfo = MapProfile(reader);

                });
            return accountInfo;
        }

        public void  DeleteDealerAccountInfo(int id)
        {
            DataProvider.ExecuteNonQuery(
                GetConnection,
                "[dbo].[DealerRegistration_Delete]",
                inputParamMapper: delegate (SqlParameterCollection parameters)
                {
                    parameters.AddWithValue("@Id", id);
                },
                returnParameters: delegate (SqlParameterCollection parameters)
                {
                }
                );
        }
    }
}