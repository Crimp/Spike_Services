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
    using System.Web;
    using Microsoft.Samples.WindowsPhoneCloud.Web.UserAccountWrappers;

    public class TableRequestValidator : StorageRequestValidator
    {
        private readonly IUserPrivilegesRepository userPrivilegesRepository;

        public TableRequestValidator()
            : this(new FormsAuthenticationService(), new AccountMembershipService(), new UserTablesServiceContext())
        {
        }

        [CLSCompliant(false)]
        public TableRequestValidator(IFormsAuthentication formsAuth, IMembershipService membershipService, IUserPrivilegesRepository userPrivilegesRepository)
            : base(formsAuth, membershipService)
        {
            this.userPrivilegesRepository = userPrivilegesRepository;
        }

        protected override bool OnValidateRequest(string userId, HttpContext context)
        {
            if (!this.CanUseTables(userId))
            {
                context.Response.EndWithDataServiceError(401, "Unauthorized", "You have no permission to use tables.");
            }

            var tableName = StorageRequestAnalyzer.GetRequestedTable(context.Request);
            if (!this.CanUseTable(userId, tableName, context.Request))
            {
                context.Response.EndWithDataServiceError(401, "Unauthorized", "You have no permission to use this table.");
            }

            return true;
        }

        private bool CanUseTable(string userId, string tableName, HttpRequest request)
        {
            var publicTablePrivilege = string.Format(CultureInfo.InvariantCulture, "{0}{1}", tableName, PrivilegeConstants.PublicTablePrivilegeSuffix);
            if (!this.userPrivilegesRepository.PublicPrivilegeExists(publicTablePrivilege))
            {
                var accessTablePrivilege = string.Format(CultureInfo.InvariantCulture, "{0}{1}", tableName, PrivilegeConstants.TablePrivilegeSuffix);
                if (!this.userPrivilegesRepository.HasUserPrivilege(userId, accessTablePrivilege))
                {
                    // Check if the user is listing the available tables or creating a new table.
                    return StorageRequestAnalyzer.IsListingTables(request) || StorageRequestAnalyzer.IsCreatingTable(request, tableName);
                }
            }

            return true;
        }

        private bool CanUseTables(string userId)
        {
            return this.userPrivilegesRepository.HasUserPrivilege(userId, PrivilegeConstants.TablesUsagePrivilege);
        }
    }
}