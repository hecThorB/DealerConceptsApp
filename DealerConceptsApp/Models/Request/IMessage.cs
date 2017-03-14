using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerConceptsApp.Services.Interfaces
{
    public interface IMessage
    {
        string ToAddress { get; set; }
        string FromAddress { get; set; }
        string Subject { get; set; }
        string Body { get; }
    }
}
