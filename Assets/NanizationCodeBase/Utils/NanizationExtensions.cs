using Naninovel;
using NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders;

namespace NanizationCodeBase.Utils
{
    public static class NanizationExtensions
    {
        public static ISelfNanizationBuild Bind(this string stringToLocalization)
        {
            return new NanizationBuild(stringToLocalization);
        }
        
        /// <remarks>Async: if Engine.Initislized == false will wait for initialization before localize.</remarks>>
        /// <summary>
        /// Get document-name and key from self if no document argument.
        /// Or get document-name from argument and key from self (all after first dot).
        /// </summary>
        /// <example>
        /// <code>
        /// // Will get localized string with Document = "UI", Key = "PlayGame"
        /// var localizedString = await "UI.PlayGame".LocalizeAsync();
        /// Will get localized string with Document = "UI", Key = "PlayGame"
        /// var localizedString = await "PlayGame".LocalizeAsync("UI");
        /// </code>
        /// </example>
        public static async UniTask<string> LocalizeAsync(this string stringToLocalization, string document = null, string fallback = null)
        {
            if (string.IsNullOrEmpty(document))
            {
                return await stringToLocalization
                    .Bind()
                    .AsSelfPath()
                    .SetFallback(fallback)
                    .LocalizeAsync();
            }
            
            return await Nanization.Bind(stringToLocalization)
                .WithDocument(document)
                .SetFallback(fallback)
                .LocalizeAsync();
        }
    }
}