using DealerConceptsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using DealerConceptsApp.Models.Request;
using DealerConceptsApp.Enums;
using System.Threading.Tasks;
using DealerConceptsApp.Domain;
using System.Data;
using WikiDataProvider.Data.Extensions;
using DealerConceptsApp.Classes;
using System.Data.SqlClient;
using System.IO;

namespace DealerConceptsApp.Services
{
    public class MessageService : BaseService, IMessageService, IAdminMessageService
    {
        private IConfigService _configService;
        private static SendGridClient _sg = new SendGridClient("lalala"); // Put send grid key here

        public MessageService(IConfigService configService)
        {
            _configService = configService;
        }

        public void Send(IMessage model)
        {
            EmailAddress from = new EmailAddress(model.FromAddress ?? "admindealerconcepts@mailinator.com");
            EmailAddress to = new EmailAddress(model.ToAddress ?? "catslovedogs@mailinator.com");
            Content content = new Content("text/html", model.Body);

            //SendGridMessage mail = new SendGridMessage(from, to, model.Subject, content.Type, content.Value);

            //Send(mail);
        }


        public void Send(IMessage model, IEnumerable<string> tos)
        {
            EmailAddress from = new EmailAddress(model.FromAddress ?? "admindealerconcepts@mailinator.com");
            EmailAddress to = new EmailAddress(model.ToAddress ?? "salesperson@mailinator.com");
            Content content = new Content("text/html", model.Body);

            //MailHelper mail = new MailHelper(from, to, model.Subject, content.Type, content.Value);

            //Send(mail);
        }

        private void Send(SendGridMessage mail)
        {

            dynamic response = Task.Run(delegate () {

                //return _sg.client.mail.send.post(requestBody: mail.Get());
            });
        }



        public string GetEmailContent(EmailTemplateKind issue, List<KeyValuePair<string, string>> tokens)
        {
            string body = null;

            switch (issue)
            {
                case EmailTemplateKind.TestDrive:
                    body = File.ReadAllText(HttpContext.Current.Server.MapPath("/Views/EmailTemplates/-----.html"));
                    break;
                case EmailTemplateKind.MultipleTestDrivesIntended:
                    body = File.ReadAllText(HttpContext.Current.Server.MapPath("/Views/EmailTemplates/-----.html"));
                    break;
                default:
                    body = "Please Select File";
                    break;
            }

            foreach (var element in tokens)
            {
                body = body.Replace(element.Key, element.Value);
            }

            return body;
        }

        public void SendConfirmation(MessageCreateRequest message, Guid token)
        {
            //List<KeyValuePair<string, string>> tokens = new List<KeyValuePair<string, string>>();
            //tokens.Add(new KeyValuePair<string, string>("{domain}", _configService.DomainName));
            //tokens.Add(new KeyValuePair<string, string>("{url}", token.ToString()));

            //message.Body = GetEmailContent(EmailTemplateKind.NewChat, tokens); //what is on the userService

            //Send(message);
        }


        public void SendNewFindRequest(MessageCreateRequest message, string userName, int requestId)
        {
            //List<KeyValuePair<string, string>> tokens = new List<KeyValuePair<string, string>>();
            //tokens.Add(new KeyValuePair<string, string>("{domain}", _configService.DomainName));
            //tokens.Add(new KeyValuePair<string, string>("{requestId}", requestId.ToString()));//at "{url}" is where the name would go. 
            //tokens.Add(new KeyValuePair<string, string>("{userName}", userName));//at "{url}" is where the name would go. 

            //message.Body = GetEmailContent(EmailTemplateKind.NewRequest, tokens); //what is on the userService

            //Send(message);
        }

        public void NewFindNotification(MessageCreateRequest message, Guid token)
        {
            //List<KeyValuePair<string, string>> tokens = new List<KeyValuePair<string, string>>();
            //tokens.Add(new KeyValuePair<string, string>("{domain}", _configService.DomainName));
            //tokens.Add(new KeyValuePair<string, string>("{url}", token.ToString()));//at "{url}" is where the name would go. 
            //message.Body = GetEmailContent(EmailTemplateKind.NewFindNotification, tokens); //what is on the userService

            //Send(message);
        }

        public void SendRequestRespondedNotification(MessageCreateRequest message, int id)
        {
            //List<KeyValuePair<string, string>> tokens = new List<KeyValuePair<string, string>>();
            //tokens.Add(new KeyValuePair<string, string>("{domain}", _configService.DomainName));
            //tokens.Add(new KeyValuePair<string, string>("{url}", "/request/" + id + "/responded"));
            //message.Body = GetEmailContent(EmailTemplateKind.SendRequestRespondedNotification, tokens);
            //message.FromAddress = _configService.SiteAdminEmail;
            //message.Subject = "Your Found!t request has been responded to!";

            //Send(message);
        }

        public void RecoverEmail(MessageCreateRequest message, Guid token)
        {
            //List<KeyValuePair<string, string>> tokens = new List<KeyValuePair<string, string>>();
            //tokens.Add(new KeyValuePair<string, string>("{domain}", _configService.DomainName));
            //tokens.Add(new KeyValuePair<string, string>("{url}", token.ToString()));

            //message.Body = GetEmailContent(EmailTemplateKind.NewConfirmChat, tokens);

            //Send(message);
        }


        public void NewCustomerToAdmin(MessageCreateRequest message, string email)
        {
            //List<KeyValuePair<string, string>> tokens = new List<KeyValuePair<string, string>>();
            //tokens.Add(new KeyValuePair<string, string>("{userName}", email)); //email

            //message.Body = GetEmailContent(EmailTemplateKind.NewCustomerToAdmin, tokens);

            //Send(message);
        }


        public Message MapMessageAdmin(IDataReader reader)
        {
            Message currentMessage = new Message();
            int startingIndex = 0;

            currentMessage.Id = reader.GetInt32(startingIndex++);
            currentMessage.SenderUserId = reader.GetString(startingIndex++);
            currentMessage.RecipientUserId = reader.GetString(startingIndex++);
            currentMessage.TypeId = reader.GetSafeEnum<MessageTypeKind>(startingIndex++);
            currentMessage.StatusId = reader.GetSafeEnum<MessageStatusKind>(startingIndex++);
            currentMessage.Subject = reader.GetString(startingIndex++);
            currentMessage.Body = reader.GetString(startingIndex++);
            currentMessage.ParentId = reader.GetSafeInt32(startingIndex++);
            currentMessage.IsDeleted = reader.GetBoolean(startingIndex++);
            currentMessage.DateCreated = reader.GetDateTime(startingIndex++);
            currentMessage.DateModified = reader.GetDateTime(startingIndex++);
            currentMessage.RequestId = reader.GetString(startingIndex++);

            return currentMessage;

        }

        public PagedList<Message> Get(int pageIndex, int itemsPerPage, MessageGetRequest model)
        {
            //PagedList<Message> pagedList = null;
            //List<Message> messages = null;
            //int totalCount;

            //DataProvider.ExecuteCmd(GetConnection, "Dbo.Messages_SelectByIds_SelectForAdmin"
            //   , inputParamMapper: delegate (SqlParameterCollection parameters)
            //   {
            //       parameters.AddWithValue("@PageIndex", pageIndex);
            //       parameters.AddWithValue("@NumberOfRecords", itemsPerPage);

            //       if (model == null)
            //       {
            //           parameters.AddWithValue("@TypeId", DBNull.Value);
            //       }
            //       else
            //       {
            //           parameters.AddWithValue("@TypeId", model.TypeId);
            //       }

            //       if (model == null)
            //       {
            //           parameters.AddWithValue("@StatusId", DBNull.Value);
            //       }
            //       else
            //       {
            //           parameters.AddWithValue("@StatusId", model.StatusId);
            //       }

            //   }
            //    , map: delegate (IDataReader reader, short set)
            //    {

            //        Message currentMessage = new Message();
            //        ProfileAccountInfo accountInfo = new ProfileAccountInfo();
            //        currentMessage.AccountInfo = accountInfo;

            //        int startingIndex = 0;

            //        currentMessage.Id = reader.GetSafeInt32(startingIndex++);
            //        currentMessage.SenderUserId = reader.GetSafeString(startingIndex++);
            //        currentMessage.RecipientUserId = reader.GetSafeString(startingIndex++);
            //        currentMessage.TypeId = reader.GetSafeEnum<MessageTypeKind>(startingIndex++);
            //        currentMessage.StatusId = reader.GetSafeEnum<MessageStatusKind>(startingIndex++);
            //        currentMessage.Subject = reader.GetSafeString(startingIndex++);
            //        currentMessage.Body = reader.GetSafeString(startingIndex++);
            //        currentMessage.ParentId = reader.GetSafeInt32(startingIndex++);
            //        currentMessage.IsDeleted = reader.GetSafeBool(startingIndex++);
            //        currentMessage.DateCreated = reader.GetSafeDateTime(startingIndex++);
            //        currentMessage.DateModified = reader.GetSafeDateTime(startingIndex++);
            //        currentMessage.RequestId = reader.GetSafeString(startingIndex++);
            //        totalCount = reader.GetSafeInt32(startingIndex++);
            //        currentMessage.AccountInfo.FirstName = reader.GetSafeString(startingIndex++);
            //        currentMessage.AccountInfo.LastName = reader.GetSafeString(startingIndex++);
            //        //currentMessage.AccountInfo.Email = reader.GetSafeString(startingIndex++);

            //        if (messages == null)
            //        {
            //            messages = new List<Message>();
            //        }

            //        messages.Add(currentMessage);

            //        if (pagedList == null)
            //        {
            //            pagedList = new PagedList<Message>(messages, pageIndex, itemsPerPage, totalCount);
            //        }
            //    }
            //    );


            //return pagedList;
            return null;
        }

        public PagedList<Message> GetByUserId(int pageIndex, int itemsPerPage, string userId, MessageGetRequest model)
        {
            PagedList<Message> pagedList = null;
            List<Message> messages = null;
            int totalCount;

            DataProvider.ExecuteCmd(GetConnection, "[dbo].[Messages_SelectByIds_SelectForAdminByUserId]"
               , inputParamMapper: delegate (SqlParameterCollection parameters)
               {
                   parameters.AddWithValue("@PageIndex", pageIndex);
                   parameters.AddWithValue("@NumberOfRecords", itemsPerPage);
                   parameters.AddWithValue("@UserId", userId);

                   if (model == null)
                   {
                       parameters.AddWithValue("@TypeId", DBNull.Value);
                   }
                   else
                   {
                       parameters.AddWithValue("@TypeId", model.TypeId);
                   }

                   if (model == null)
                   {
                       parameters.AddWithValue("@StatusId", DBNull.Value);
                   }
                   else
                   {
                       parameters.AddWithValue("@StatusId", model.StatusId);
                   }
               }
                , map: delegate (IDataReader reader, short set)
                {

                    Message message = new Message();
                    DealerAccountInfo accountInfo = new DealerAccountInfo();
                    message.AccountInfo = accountInfo;

                    int startingIndex = 0;

                    message.Id = reader.GetSafeInt32(startingIndex++);
                    message.SenderUserId = reader.GetSafeString(startingIndex++);
                    message.RecipientUserId = reader.GetSafeString(startingIndex++);
                    message.TypeId = reader.GetSafeEnum<MessageTypeKind>(startingIndex++);
                    message.StatusId = reader.GetSafeEnum<MessageStatusKind>(startingIndex++);
                    message.Subject = reader.GetSafeString(startingIndex++);
                    message.Body = reader.GetSafeString(startingIndex++);
                    message.ParentId = reader.GetSafeInt32(startingIndex++);
                    message.IsDeleted = reader.GetSafeBool(startingIndex++);
                    message.DateCreated = reader.GetSafeDateTime(startingIndex++);
                    message.DateModified = reader.GetSafeDateTime(startingIndex++);
                    message.RequestId = reader.GetSafeString(startingIndex++);
                    totalCount = reader.GetSafeInt32(startingIndex++);
                    message.AccountInfo.FirstName = reader.GetSafeString(startingIndex++);
                    message.AccountInfo.LastName = reader.GetSafeString(startingIndex++);
                    //message.AccountInfo.Email = reader.GetSafeString(startingIndex++);

                    if (messages == null)
                    {
                        messages = new List<Message>();
                    }

                    messages.Add(message);

                    if (pagedList == null)
                    {
                        pagedList = new PagedList<Message>(messages, pageIndex, itemsPerPage, totalCount);
                    }
                }
                );


            return pagedList;

        }

        public List<MessageContainer> GetTotal(MessageGetRequest model)
        {
            List<MessageContainer> list = null;

            DataProvider.ExecuteCmd(GetConnection, "[dbo].[Messages_SelectStatusAndTypeCount]"
                , inputParamMapper: delegate (SqlParameterCollection parameters)
                {
                    if (model == null)
                    {
                        parameters.AddWithValue("@TypeId", DBNull.Value);
                    }
                    else
                    {
                        parameters.AddWithValue("@TypeId", model.TypeId);
                    }

                    if (model == null)
                    {
                        parameters.AddWithValue("@StatusId", DBNull.Value);
                    }
                    else
                    {
                        parameters.AddWithValue("@StatusId", model.StatusId);
                    }
                }
                , map: delegate (IDataReader reader, short set)
                {
                    MessageContainer allCounts = new MessageContainer();
                    int startingIndex = 0;

                    allCounts.Id = reader.GetSafeInt32(startingIndex++);
                    allCounts.Name = reader.GetSafeString(startingIndex++);
                    allCounts.Total = reader.GetSafeInt32(startingIndex++);


                    if (list == null)
                    {
                        list = new List<MessageContainer>();
                    }

                    list.Add(allCounts);
                }
                );

            return list;
        }

        public List<MessageContainer> GetTotalByUserId(MessageGetRequest model, string userId)
        {
            List<MessageContainer> list = null;

            DataProvider.ExecuteCmd(GetConnection, "[dbo].[Messages_SelectStatusAndTypeCountByUserId]"
                , inputParamMapper: delegate (SqlParameterCollection parameters)
                {
                    if (model == null)
                    {
                        parameters.AddWithValue("@TypeId", DBNull.Value);
                    }
                    else
                    {
                        parameters.AddWithValue("@TypeId", model.TypeId);
                    }

                    if (model == null)
                    {
                        parameters.AddWithValue("@StatusId", DBNull.Value);
                    }
                    else
                    {
                        parameters.AddWithValue("@StatusId", model.StatusId);
                    }

                    parameters.AddWithValue("@UserId", userId);

                }
                , map: delegate (IDataReader reader, short set)
                {
                    MessageContainer allCounts = new MessageContainer();
                    int startingIndex = 0;

                    allCounts.Id = reader.GetSafeInt32(startingIndex++);
                    allCounts.Name = reader.GetSafeString(startingIndex++);
                    allCounts.Total = reader.GetSafeInt32(startingIndex++);


                    if (list == null)
                    {
                        list = new List<MessageContainer>();
                    }

                    list.Add(allCounts);
                }
                );

            return list;
        }

        public PagedList<Message> GetByQuery(int pageIndex, int itemsPerPage, MessageQueryRequest model)
        {
            PagedList<Message> pagedList = null;
            List<Message> messages = null;
            int totalCount;

            DataProvider.ExecuteCmd(GetConnection, "[dbo].[Messages_SelectByIds_SelectByQuery]"
               , inputParamMapper: delegate (SqlParameterCollection parameters)
               {
                   parameters.AddWithValue("@PageIndex", pageIndex);
                   parameters.AddWithValue("@NumberOfRecords", itemsPerPage);
                   parameters.AddWithValue("@Query", model.Query);

               }
                , map: delegate (IDataReader reader, short set)
                {

                    Message message = new Message();
                    DealerAccountInfo accountInfo = new DealerAccountInfo();
                    message.AccountInfo = accountInfo;

                    int startingIndex = 0;

                    message.Id = reader.GetSafeInt32(startingIndex++);
                    message.SenderUserId = reader.GetSafeString(startingIndex++);
                    message.RecipientUserId = reader.GetSafeString(startingIndex++);
                    message.TypeId = reader.GetSafeEnum<MessageTypeKind>(startingIndex++);
                    message.StatusId = reader.GetSafeEnum<MessageStatusKind>(startingIndex++);
                    message.Subject = reader.GetSafeString(startingIndex++);
                    message.Body = reader.GetSafeString(startingIndex++);
                    message.ParentId = reader.GetSafeInt32(startingIndex++);
                    message.IsDeleted = reader.GetSafeBool(startingIndex++);
                    message.DateCreated = reader.GetSafeDateTime(startingIndex++);
                    message.DateModified = reader.GetSafeDateTime(startingIndex++);
                    message.RequestId = reader.GetSafeString(startingIndex++);
                    totalCount = reader.GetSafeInt32(startingIndex++);
                    message.AccountInfo.FirstName = reader.GetSafeString(startingIndex++);
                    message.AccountInfo.LastName = reader.GetSafeString(startingIndex++);
                    //message.AccountInfo.Email = reader.GetSafeString(startingIndex++);

                    if (messages == null)
                    {
                        messages = new List<Message>();
                    }

                    messages.Add(message);

                    if (pagedList == null)
                    {
                        pagedList = new PagedList<Message>(messages, pageIndex, itemsPerPage, totalCount);
                    }
                }
                );


            return pagedList;
            //return null;
        }

        public PagedList<Message> GetByQueryAndUserId(string userId, int pageIndex, int itemsPerPage, MessageQueryRequest model)
        {
            PagedList<Message> pagedList = null;
            List<Message> messages = null;
            int totalCount;

            DataProvider.ExecuteCmd(GetConnection, "[dbo].[Messages_SelectByIds_SelectByQueryAndUserId]"
               , inputParamMapper: delegate (SqlParameterCollection parameters)
               {
                   parameters.AddWithValue("@PageIndex", pageIndex);
                   parameters.AddWithValue("@NumberOfRecords", itemsPerPage);
                   parameters.AddWithValue("@Query", model.Query);
                   parameters.AddWithValue("@UserId", userId);
               }
                , map: delegate (IDataReader reader, short set)
                {

                    Message message = new Message();
                    DealerAccountInfo accountInfo = new DealerAccountInfo();
                    message.AccountInfo = accountInfo;

                    int startingIndex = 0;

                    message.Id = reader.GetSafeInt32(startingIndex++);
                    message.SenderUserId = reader.GetSafeString(startingIndex++);
                    message.RecipientUserId = reader.GetSafeString(startingIndex++);
                    message.TypeId = reader.GetSafeEnum<MessageTypeKind>(startingIndex++);
                    message.StatusId = reader.GetSafeEnum<MessageStatusKind>(startingIndex++);
                    message.Subject = reader.GetSafeString(startingIndex++);
                    message.Body = reader.GetSafeString(startingIndex++);
                    message.ParentId = reader.GetSafeInt32(startingIndex++);
                    message.IsDeleted = reader.GetSafeBool(startingIndex++);
                    message.DateCreated = reader.GetSafeDateTime(startingIndex++);
                    message.DateModified = reader.GetSafeDateTime(startingIndex++);
                    message.RequestId = reader.GetSafeString(startingIndex++);
                    totalCount = reader.GetSafeInt32(startingIndex++);
                    message.AccountInfo.FirstName = reader.GetSafeString(startingIndex++);
                    message.AccountInfo.LastName = reader.GetSafeString(startingIndex++);
                    //message.AccountInfo.Email = reader.GetSafeString(startingIndex++);

                    if (messages == null)
                    {
                        messages = new List<Message>();
                    }

                    messages.Add(message);

                    if (pagedList == null)
                    {
                        pagedList = new PagedList<Message>(messages, pageIndex, itemsPerPage, totalCount);
                    }
                }
                );


            return pagedList;
            //return null;
        }

        //public PagedList<Message> GetByClient(int pageIndex, int itemsPerPage, string userId, MessageGetByClientRequest model)
        //{
        //    PagedList<Message> pagedList = null;
        //    List<Message> messages = null;
        //    int totalCount;

        //    DataProvider.ExecuteCmd(GetConnection, "[dbo].[Messages_SelectByIds_SelectForAdminByUserIdAndClientUserId]"
        //       , inputParamMapper: delegate (SqlParameterCollection parameters)
        //       {
        //           parameters.AddWithValue("@PageIndex", pageIndex);
        //           parameters.AddWithValue("@NumberOfRecords", itemsPerPage);
        //           parameters.AddWithValue("@UserId", userId);
        //           parameters.AddWithValue("@ClientUserId", model.ClientUserId);

        //       }
        //        , map: delegate (IDataReader reader, short set)
        //        {

        //            Message message = new Message();
        //            ProfileAccountInfo accountInfo = new ProfileAccountInfo();
        //            message.AccountInfo = accountInfo;

        //            int startingIndex = 0;

        //            message.Id = reader.GetSafeInt32(startingIndex++);
        //            message.SenderUserId = reader.GetSafeString(startingIndex++);
        //            message.RecipientUserId = reader.GetSafeString(startingIndex++);
        //            message.TypeId = reader.GetSafeEnum<MessageTypeKind>(startingIndex++);
        //            message.StatusId = reader.GetSafeEnum<MessageStatusKind>(startingIndex++);
        //            message.Subject = reader.GetSafeString(startingIndex++);
        //            message.Body = reader.GetSafeString(startingIndex++);
        //            message.ParentId = reader.GetSafeInt32(startingIndex++);
        //            message.IsDeleted = reader.GetSafeBool(startingIndex++);
        //            message.DateCreated = reader.GetSafeDateTime(startingIndex++);
        //            message.DateModified = reader.GetSafeDateTime(startingIndex++);
        //            message.RequestId = reader.GetSafeString(startingIndex++);
        //            totalCount = reader.GetSafeInt32(startingIndex++);
        //            message.AccountInfo.FirstName = reader.GetSafeString(startingIndex++);
        //            message.AccountInfo.LastName = reader.GetSafeString(startingIndex++);
        //            //message.AccountInfo.Email = reader.GetSafeString(startingIndex++);

        //            if (messages == null)
        //            {
        //                messages = new List<Message>();
        //            }

        //            messages.Add(message);

        //            if (pagedList == null)
        //            {
        //                pagedList = new PagedList<Message>(messages, pageIndex, itemsPerPage, totalCount);
        //            }
        //        }
        //        );


        //    return pagedList;

        //}
    }
}