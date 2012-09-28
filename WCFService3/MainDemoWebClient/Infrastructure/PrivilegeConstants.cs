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
    public static class PrivilegeConstants
    {
        public const string AdminPrivilege = "admin";

        public const string TablesUsagePrivilege = "TablesUsage";

        public const string BlobContainersUsagePrivilege = "BlobContainersUsage";

        public const string SqlUsagePrivilege = "SqlUsage";

        public const string QueuesUsagePrivilege = "QueuesUsage";

        public const string QueuePrivilegeSuffix = "_queue_access";

        public const string PublicQueuePrivilegeSuffix = "_queue_publicaccess";

        public const string TablePrivilegeSuffix = "_table_access";

        public const string PublicTablePrivilegeSuffix = "_table_publicaccess";

        public const string BlobContainerPrivilegeSuffix = "_blobcontainer_access";

        public const string PublicBlobContainerPrivilegeSuffix = "_blobcontainer_publicaccess";
    }
}