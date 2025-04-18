using System;
using Naninovel;
using NanizationCodeBase.Services;

namespace NanizationCodeBase.Utils
{
    public static class NaniLocalizeUtils
    {
        private const char CategorySeparator = '.';
        private const int StartDocumentIndexInSourceString = 0;
        private const int SubstringDeltaForExclusiveDot = 1;
        
        /// <remarks>Async: if Engine.Initislized == false will wait for initialization.</remarks>>
        /// <summary>
        /// Get document-name from argument and key (auto lowercase) from self.
        /// </summary>
        public static async UniTask<string> LocalizeAsync(string documentName, string key)
        {
            var textManager = await NanizationService.GetTextManagerAsync();
            if (textManager.DocumentLoader.IsLoaded(documentName) == false)
                await textManager.DocumentLoader.Load(documentName);
            
            return textManager.GetRecordValue(key, documentName);
        }
        
        public static void GetKeyAndDocumentFromString(string stringSource, out string document, out string key)
        {
            key = string.Empty;
            document = string.Empty;

            if (string.IsNullOrWhiteSpace(stringSource))
                throw new ArgumentException("Source string cannot be null or empty.", nameof(stringSource));
                
            var separatorIndex = stringSource.IndexOf(CategorySeparator);
            
            document = stringSource.Substring(StartDocumentIndexInSourceString, separatorIndex);
            key = stringSource.Substring(separatorIndex + SubstringDeltaForExclusiveDot);
        }
    }
}