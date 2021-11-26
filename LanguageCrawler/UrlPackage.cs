using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace LanguageCrawler
{
    class UrlPackage : IDisposable
    {
        private bool disposed = false;
        private string baseAddress;
        private List<String> associatedAddresses;
        public List<String> AssociatedAddresses
        {
            get => associatedAddresses;
            set => associatedAddresses = value;
        }
        private HttpClient baseClient;

        public UrlPackage(string baseAddress)
        {
            this.baseAddress = baseAddress;
            associatedAddresses = new List<string>();
        }
        
        public UrlPackage()
        {
            associatedAddresses = new List<string>();
        }

        public HttpClient MakeClient()
        {
            baseClient = new HttpClient();
            disposed = false;
            if (baseAddress != null)
            {
                baseClient.BaseAddress = new Uri(baseAddress);
            }
            return baseClient;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                baseClient.Dispose();
                disposed = true;
            }
        }

    }
}
