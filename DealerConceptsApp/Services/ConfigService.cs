using DealerConceptsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Services
{
    public class ConfigService : IConfigService
    {
        private string GetSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        public string SiteName
        {
            get { return GetSetting("SiteName"); }
        }

        public string Bucket
        {
            get { return GetSetting("AwsBucket"); }
        }

        public string Key
        {
            get { return GetSetting("AwsKey"); }
        }

        public string Secret
        {
            get { return GetSetting("AwsSecret"); }
        }

        public string DomainName
        {
            get { return GetSetting("DomainName"); }
        }

        public string TwilioAccountSid
        {
            get { return GetSetting("TwilioAccountSid"); }
        }

        public string TwilioApiKey
        {
            get { return GetSetting("TwilioApiKey"); }
        }

        public string TwilioApiSecret
        {
            get { return GetSetting("TwilioApiSecret"); }
        }

        public string IpmServiceSID
        {
            get { return GetSetting("TwilioIpmServiceSid"); }
        }
        public string SiteAdminEmail
        {
            get { return GetSetting("SiteAdminEmail"); }
        }
    }
}