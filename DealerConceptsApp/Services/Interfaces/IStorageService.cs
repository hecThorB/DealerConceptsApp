using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DealerConceptsApp.Services.Interfaces
{
    public interface IStorageService
    {
        string Upload(HttpPostedFile file);
    }
}
