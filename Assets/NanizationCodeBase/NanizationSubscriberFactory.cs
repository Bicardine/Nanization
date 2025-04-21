using System;
using System.Collections.Generic;
using NanizationCodeBase.Components.NotMonoBehaviours;

namespace NanizationCodeBase
{
    static partial class NanizationSubscriberFactory
    {
        private static readonly Dictionary<Guid, NanizationSubscriber> _subscribers = new ();
        
        private static Guid NewSubscriberId() => Guid.NewGuid();

        public static INanizationSubscriber NewSubscriber(
            string document, string key, Action<string> callback, string fallback = null, bool localizeNow = false)
        {
            var subscriberId = NewSubscriberId();
            var subscriber = new NanizationSubscriber(subscriberId, document, key, callback, () =>
            {
                _subscribers.Remove(subscriberId);
            }, fallback);
        
            _subscribers.Add(subscriberId, subscriber);
            
            if (localizeNow) subscriber.Localize();
            
            return subscriber;
        }
    }
}