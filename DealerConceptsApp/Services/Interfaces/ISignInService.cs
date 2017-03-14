using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerConceptsApp.Services.Interfaces
{
    public interface ISignInService
    {
        bool Signin(string emailaddress, string password, bool allowUnconfirmed = false);
    }
}
