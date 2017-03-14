using System;
using System.Collections.Generic;
using DealerConceptsApp.Models.Request;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerConceptsApp.Services.Interfaces
{
    public interface IMessageService
    {
        void Send(IMessage model);

        void SendConfirmation(MessageCreateRequest message, Guid token);

        string GetEmailContent(Enums.EmailTemplateKind issue, List<KeyValuePair<string, string>> token);

        void RecoverEmail(MessageCreateRequest model, Guid token);

        void SendRequestRespondedNotification(MessageCreateRequest model, int id);

        void NewFindNotification(MessageCreateRequest message, Guid token);

        void SendNewFindRequest(MessageCreateRequest message, string userName, int requestId);

        void NewCustomerToAdmin(MessageCreateRequest message, string email);
    }
}
