﻿// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

namespace Microsoft.Samples.WindowsPhoneCloud.Web.Infrastructure
{
    using System;
    using System.Globalization;
    using System.Security.Cryptography.X509Certificates;

    public static class CertificateUtil
    {
        public static X509Certificate2 GetCertificate(StoreName name, StoreLocation location, string thumbprint)
        {
            var store = new X509Store(name, location);
            X509Certificate2Collection certificates = null;
            store.Open(OpenFlags.ReadOnly);

            try
            {
                X509Certificate2 result = null;

                // Every time we call store.Certificates property, a new collection will be returned.
                certificates = store.Certificates;

                for (int i = 0; i < certificates.Count; i++)
                {
                    X509Certificate2 cert = certificates[i];

                    if (cert.Thumbprint.Equals(thumbprint, StringComparison.OrdinalIgnoreCase))
                    {
                        result = new X509Certificate2(cert);
                        break;
                    }
                }

                if (result == null)
                {
                    throw new ApplicationException(string.Format(CultureInfo.InvariantCulture, "No certificate was found for thumbprint {0}", thumbprint));
                }

                return result;
            }
            finally
            {
                if (certificates != null)
                {
                    for (int i = 0; i < certificates.Count; i++)
                    {
                        X509Certificate2 cert = certificates[i];
                        cert.Reset();
                    }
                }

                store.Close();
            }
        }
    }
}