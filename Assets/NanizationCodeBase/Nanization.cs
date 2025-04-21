using System;
using Naninovel;
using NanizationCodeBase.Components.NotMonoBehaviours;
using NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders;
using NanizationCodeBase.Services;

namespace NanizationCodeBase
{
    public static class Nanization
    {
        public static IEmptyNanizationBuild Bind() => new NanizationBuild();

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
        
        public static async UniTask<string> LocalizeAsync(INanizationBuild nanizationBuild)
            => await nanizationBuild.LocalizeAsync();

        public static INanizationSubscriber Subscribe(
            string document, string key, Action<string> callback, string fallback = null, bool localizeNow = true) =>
                NanizationSubscriberFactory.NewSubscriber(document, key, callback, fallback, localizeNow);

        public static INanizationSubscriber Subscribe(
            INanizationBuild nanizationBuild, Action<string> callback, bool localizeNow = true) =>
                nanizationBuild.Subscribe(callback, localizeNow);
    }
}