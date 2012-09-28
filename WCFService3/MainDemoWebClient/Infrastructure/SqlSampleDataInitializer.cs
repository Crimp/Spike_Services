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
    using System.Collections.Generic;
    using System.Data.Entity;
    using Microsoft.Samples.WindowsPhoneCloud.Web.Models;

    // Summary:
    //     Sample Entity Framework 4.1 data initializer for SQL Azure.
    //     The seed method is executed after the database is created using EF 4.1 Code-First, 
    //     giving the developer the opportunity to insert "seed" (i.e. initial) data.
    //     For more information, visit the ADO.NET Entity Framework website at http://msdn.microsoft.com/data/aa937723
    public class SqlSampleDataInitializer : DropCreateDatabaseIfModelChanges<SqlSampleDataContext>
    {
        protected override void Seed(SqlSampleDataContext context)
        {
            var data = new List<SqlSampleData>
            {
                new SqlSampleData
                {
                    Id = 1,
                    Title = "I am the first title",
                    IsPublic = true,
                    Date = DateTime.UtcNow
                },

                new SqlSampleData
                {
                    Id = 2,
                    Title = "I am the second title",
                    IsPublic = true,
                    Date = DateTime.UtcNow
                }
            };

            data.ForEach(d => context.SqlSampleData.Add(d));
        } 
    }
}