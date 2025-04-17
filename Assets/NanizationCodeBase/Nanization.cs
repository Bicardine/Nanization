using System;
using System.Collections.Generic;
using Naninovel;
using NanizationCodeBase.Components.NotMonoBehaviours;
using NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders;
using NanizationCodeBase.Services;

namespace NanizationCodeBase
{
    public static class Nanization
    {
        private static readonly Dictionary<string, NanizationSubscriber> _subscribers = new ();

        public static IEmptyNanizationBuild Bind()
        {
            return new NanizationBuild();
        }

        public static IWithKeyNanizationBuild Bind(string sourceString)
        {
            return new NanizationBuild(sourceString)
                .WithKey(sourceString);
        }

        public static async UniTask<string> LocalizeAsync(string document, string key, string fallback = null)
        {
            var textManager = await NanizationService.GetTextManagerAsync();
            
            if (textManager.DocumentLoader.IsLoaded(document) == false)
                await textManager.DocumentLoader.Load(document);
            
            var localizedValue = textManager.GetRecordValue(key, document);

            if (string.IsNullOrEmpty(localizedValue) && string.IsNullOrEmpty(fallback) == false)
                return fallback;
            
            return localizedValue;
        }

        public static INanizationSubscriber Subscribe(
            string document, string key, Action<string> callback, string fallback = null, bool localizeNow = true)
        {
            var subscriberId = GetSubscriberId(document, key, callback);
            
            return GetOrCreateSubscriber(document, key, callback, subscriberId, fallback, localizeNow);
        }
        
        public static INanizationSubscriber Subscribe(
            IReadyToExecuteNanizationBuild nanizationBuild, Action<string> callback, bool localizeNow = true)
        {
            var subscriberId = GetSubscriberId(nanizationBuild.Document, nanizationBuild.Key, callback);;
            
            return GetOrCreateSubscriber(
                nanizationBuild.Document, nanizationBuild.Key, callback, subscriberId, localizeNow: localizeNow);
        }

        public static INanizationSubscriber Subscribe(
            IReadyToExecuteNanizationBuildWithFallback nanizationBuild, Action<string> callback, bool localizeNow = true)
        {
            var subscriberId = GetSubscriberId(nanizationBuild.Document, nanizationBuild.Key, callback);
            
            return GetOrCreateSubscriber(
                nanizationBuild.Document, nanizationBuild.Key, callback, subscriberId, nanizationBuild.Fallback, localizeNow);
        }
        
        private static string GetSubscriberId(string document, string key, Action<string> callback)
        {
            var subscriberId = $"{document}_{key}_{callback.GetHashCode()}";
            UnityEngine.Debug.Log(subscriberId);
            
            return subscriberId;
        }

        private static NanizationSubscriber GetOrCreateSubscriber(
            string document, string key, Action<string> callback, string subscriberId, string fallback = null, bool localizeNow = false)
        {
            if (_subscribers.TryGetValue(subscriberId, out var subscriber))
            {
                subscriber.Resubscribe();
            }
            else
            {
                subscriber = new NanizationSubscriber(document, key, callback, () =>
                {
                    _subscribers.Remove(subscriberId);
                }, fallback);
            
                _subscribers.Add(subscriberId, subscriber);
            }
            
            if (localizeNow) subscriber.Localize();
            
            return subscriber;
        }
    }
}