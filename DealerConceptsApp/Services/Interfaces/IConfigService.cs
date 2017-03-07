using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerConceptsApp.Services.Interfaces
{
    public interface IConfigService
    {
        string SiteName { get; }
        string Bucket { get; }
        string Key { get; }
        string Secret { get; }
        string DomainName { get; }
        string SiteAdminEmail { get; }
    }
}
