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
    using System.Linq;
    using System.Net;
    using System.ServiceModel.Web;
    using System.Web;
    using Microsoft.Samples.WindowsPhoneCloud.Web.UserAccountWrappers;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;

    public class BlobContainerRequestValidator
    {
        private readonly IUserPrivilegesRepository userPrivilegesRepository;
        private readonly CloudBlobClient cloudBlobClient;

#if ACS
        public BlobContainerRequestValidator()
            : this(new UserTablesServiceContext(), null)
        {
        }

        [CLSCompliant(false)]
        public BlobContainerRequestValidator(IUserPrivilegesRepository userPrivilegesRepository, CloudBlobClient cloudBlobClient)
        {
            if ((cloudBlobClient == null) && (GetStorageAccountFromConfigurationSetting() == null))
            {
                throw new ArgumentNullException("cloudBlobClient", "The Cloud Blob Client cannot be null if no configuration is loaded.");
            }

            this.userPrivilegesRepository = userPrivilegesRepository;
            this.cloudBlobClient = cloudBlobClient ?? GetStorageAccountFromConfigurationSetting().CreateCloudBlobClient();
        }

        public string GetUserId(HttpContextBase context)
        {
            var identity = HttpContext.Current.User.Identity as Microsoft.IdentityModel.Claims.IClaimsIdentity;
            return identity.Claims.Single(c => c.ClaimType == Microsoft.IdentityModel.Claims.ClaimTypes.NameIdentifier).Value;
        }
#else
        private readonly IMembershipService membershipService;
        private readonly IFormsAuthentication formsAuth;

        public BlobContainerRequestValidator()
            : this(new FormsAuthenticationService(), new AccountMembershipService(), new UserTablesServiceContext(), null)
        {
        }

        [CLSCompliant(false)]
        public BlobContainerRequestValidator(IFormsAuthentication formsAuth, IMembershipService membershipService, IUserPrivilegesRepository userPrivilegesRepository, CloudBlobClient cloudBlobClient)
        {
            if ((cloudBlobClient == null) && (GetStorageAccountFromConfigurationSetting() == null))
            {
                throw new ArgumentNullException("cloudBlobClient", "The Cloud Blob Client cannot be null if no configuration is loaded.");
            }

            this.userPrivilegesRepository = userPrivilegesRepository;
            this.formsAuth = formsAuth;
            this.membershipService = membershipService;

            this.cloudBlobClient = cloudBlobClient ?? GetStorageAccountFromConfigurationSetting().CreateCloudBlobClient();
        }

        public string GetUserId(HttpContextBase context)
        {
            string ticketValue = null;

            var cookie = context.Request.Cookies[this.formsAuth.FormsCookieName];
            if (cookie != null)
            {
                // From cookie.
                ticketValue = cookie.Value;
            }
            else if (context.Request.Headers["AuthToken"] != null)
            {
                // From HTTP header.
                ticketValue = context.Request.Headers["AuthToken"];
            }

            if (!string.IsNullOrEmpty(ticketValue))
            {
                System.Web.Security.FormsAuthenticationTicket ticket;

                try
                {
                    ticket = this.formsAuth.Decrypt(ticketValue);
                }
                catch
                {
                    throw new WebFaultException<string>("The authorization ticket cannot be decrypted.", HttpStatusCode.Unauthorized);
                }

                if (ticket != null)
                {
                    // Authorize blobs usage.
                    var userId = this.membershipService.GetUser(new System.Web.Security.FormsIdentity(ticket).Name).ProviderUserKey.ToString();
                    if (!this.userPrivilegesRepository.HasUserPrivilege(userId, PrivilegeConstants.BlobContainersUsagePrivilege))
                    {
                        throw new WebFaultException<string>("You have no permission to use blobs.", HttpStatusCode.Unauthorized);
                    }

                    return userId;
                }
                else
                {
                    throw new WebFaultException<string>("The authorization token is no longer valid.", HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                throw new WebFaultException<string>("Resource not found.", HttpStatusCode.NotFound);
            }
        }
#endif

        public bool OnValidateRequest(string userId)
        {
            return this.OnValidateRequest(userId, string.Empty);
        }

        public bool OnValidateRequest(string userId, string blobContainerName, bool isCreating = false)
        {
            if (!this.CanUseBlobContainers(userId))
            {
                throw new WebFaultException<string>("You have no permission to use blob containers.", HttpStatusCode.Unauthorized);
            }

            if (!this.CanUseBlobContainer(userId, blobContainerName, isCreating))
            {
                throw new WebFaultException<string>("You have no permission to use this blob container.", HttpStatusCode.Unauthorized);
            }

            return true;
        }

        private static CloudStorageAccount GetStorageAccountFromConfigurationSetting()
        {
            CloudStorageAccount account = null;

            try
            {
                account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            }
            catch (InvalidOperationException)
            {
                account = null;
            }

            return account;
        }

        private bool CanUseBlobContainer(string userId, string blobContainerName, bool isCreating)
        {
            if (string.IsNullOrWhiteSpace(blobContainerName))
            {
                return true;
            }

            var publicBlobContainerPrivilege = string.Format(CultureInfo.InvariantCulture, "{0}{1}", blobContainerName, PrivilegeConstants.PublicBlobContainerPrivilegeSuffix);
            if (!this.userPrivilegesRepository.PublicPrivilegeExists(publicBlobContainerPrivilege))
            {
                var accessBlobContainerPrivilege = string.Format(CultureInfo.InvariantCulture, "{0}{1}", blobContainerName, PrivilegeConstants.BlobContainerPrivilegeSuffix);
                if (!this.userPrivilegesRepository.HasUserPrivilege(userId, accessBlobContainerPrivilege))
                {
                    // Check if the user is creating a new blob container.
                    if (isCreating)
                    {
                        var container = this.cloudBlobClient.GetContainerReference(blobContainerName);
                        return !container.Exists();
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CanUseBlobContainers(string userId)
        {
            return this.userPrivilegesRepository.HasUserPrivilege(userId, PrivilegeConstants.BlobContainersUsagePrivilege);
        }
    }
}