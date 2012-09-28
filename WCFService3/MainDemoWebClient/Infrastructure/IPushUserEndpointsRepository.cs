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
    using System.Linq;
    using Microsoft.Samples.WindowsPhoneCloud.Web.Models;

    [CLSCompliant(false)]
    public interface IPushUserEndpointsRepository
    {
        void AddPushUserEndpoint(PushUserEndpoint pushUserEndpoint);

        void UpdatePushUserEndpoint(PushUserEndpoint pushUserEndpoint);

        void RemovePushUserEndpoint(PushUserEndpoint pushUserEndpoint);        

        IEnumerable<string> GetAllPushUsers();

        IEnumerable<PushUserEndpoint> GetPushUsersByName(string userId);
        
        PushUserEndpoint GetPushUserByApplicationAndDevice(string applicationId, string deviceId);
        
#if SQLONLY
        void AddQueuedPushNotification(QueuedPushNotification queuedPushNotification);

        void DeleteQueuedMessage(QueuedPushNotification message);

        PushUserEndpoint GetPushUserEndpointByChannel(Uri channelUri);

        IEnumerable<QueuedPushNotification> GetQueuedPushNotificationsByChannel(Uri channelUri);
#endif
    }
}
