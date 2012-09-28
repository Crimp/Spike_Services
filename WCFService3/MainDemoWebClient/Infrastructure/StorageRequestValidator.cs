// ----------------------------------------------------------------------------------
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
    using System.Web;
    using System.Web.Security;
    using Microsoft.Samples.WindowsPhoneCloud.Web.UserAccountWrappers;

    public abstract class StorageRequestValidator : IStorageRequestValidator
    {
        private readonly IFormsAuthentication formsAuth;
        private readonly IMembershipService membershipService;

        protected StorageRequestValidator(IFormsAuthentication formsAuth, IMembershipService membershipService)
        {
            this.formsAuth = formsAuth;
            this.membershipService = membershipService;
        }

        public bool ValidateRequest(HttpContext context)
        {
            var userName = this.GetUserId(context);
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }

            return this.OnValidateRequest(userName, context);
        }

        public string GetUserId(HttpContext context)
        {
            string ticketValue = null;

            var cookie = context.Request.Cookies[this.formsAuth.FormsCookieName];
            if (cookie != null)
            {
                // From cookie
                ticketValue = cookie.Value;
            }
            else if (context.Request.Headers["AuthToken"] != null)
            {
                // From http header
                ticketValue = context.Request.Headers["AuthToken"];
            }

            if (!string.IsNullOrEmpty(ticketValue))
            {
                FormsAuthenticationTicket ticket = null;
                try
                {
                    ticket = this.formsAuth.Decrypt(ticketValue);
                }
                catch (ArgumentException)
                {
                    context.Response.EndWithDataServiceError(401, "Unauthorized", "The Authorization Token is no longer valid.");
                }

                if (ticket != null)
                {
                    return this.membershipService.GetUser(new FormsIdentity(ticket).Name).ProviderUserKey.ToString();
                }
            }

            context.Response.EndWithDataServiceError(404, "Not Found", "Resource Not Found.");

            return string.Empty;
        }

        protected abstract bool OnValidateRequest(string userId, HttpContext context);
    }
}