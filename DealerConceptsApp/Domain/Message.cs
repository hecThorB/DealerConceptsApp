using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DealerConceptsApp.Enums;
using System;

namespace DealerConceptsApp.Domain
{
    public class Message
    {
        public int Id { get; set; }

        public string SenderUserId { get; set; }

        public string RecipientUserId { get; set; }

        public MessageTypeKind TypeId { get; set; }

        public MessageStatusKind StatusId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int ParentId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string RequestId { get; set; }

        public DealerAccountInfo AccountInfo { get; set; }
    }
}