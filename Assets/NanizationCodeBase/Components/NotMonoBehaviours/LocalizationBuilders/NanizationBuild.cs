using Naninovel;
using NanizationCodeBase.Utils;

namespace NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders
{
    // Mutable concept.
    // And explicit interfaces to build limitation.
    public class NanizationBuild : 
        IEmptyNanizationBuild, IEmptyNanizationBuildWithFallback,
        ISelfNanizationBuild, ISelfNanizationBuildWithFallback,
        IWithKeyNanizationBuild, IWithKeyNanizationBuildWithFallback,
        IWithDocumentNanizationBuild, IWithDocumentNanizationBuildWithFallback,
        IReadyToExecuteNanizationBuild, IReadyToExecuteNanizationBuildWithFallback
    {
        private string _sourceString;
        private string _document;
        private string _key;
        private string _fallback;
        
        string IHaveLocalizationDocument.Document => _document;
        string IHaveLocalizationKey.Key => _key;

        string IHaveFallback.Fallback => _fallback;
        
        internal NanizationBuild(string sourceString = null)
        {
            _sourceString = sourceString;
        }
        
        public IReadyToExecuteNanizationBuildWithFallback AsSelfPath() => AsSelfPathAndReturn();
        
        IReadyToExecuteNanizationBuildWithFallback IWithKeyNanizationBuildWithFallback.WithDocument(string document)
            => SetDocumentAndReturn(document);
        
        public IWithDocumentNanizationBuild WithDocument(string document) => SetDocumentAndReturn(document);

        IReadyToExecuteNanizationBuildWithFallback IWithDocumentNanizationBuildWithFallback.WithKey(string key)
            => SetKeyAndReturn(key);

        public IWithKeyNanizationBuild WithKey(string key) => SetKeyAndReturn(key);
        
        public IEmptyNanizationBuildWithFallback SetFallback(string fallback)
            => SetFallbackAndReturn(fallback);
        
        public async UniTask<string> LocalizeAsync() => await Nanization.LocalizeAsync(_document, _key, _fallback);

        private NanizationBuild AsSelfPathAndReturn()
        {
            NaniLocalizeUtils.GetKeyAndDocumentFromString(_sourceString, out _document, out _key);

            return this;
        }

        private NanizationBuild SetDocumentAndReturn(string document)
        {
            _document = document;
            
            return this;
        }

        private NanizationBuild SetDocumentFromSourceAndReturn(string document)
        {
            _document = document;
            _key = _sourceString;

            return this;
        }

        private NanizationBuild SetKeyAndReturn(string key)
        {
            _key = key;

            return this;
        }

        private NanizationBuild SetFallbackAndReturn(string fallback)
        {
            _fallback = fallback;

            return this;
        }
        
        IWithDocumentNanizationBuild IEmptyNanizationBuild.WithDocument(string document)
            => SetDocumentAndReturn(document);

        IReadyToExecuteNanizationBuild IWithDocumentNanizationBuild.WithKey(string key)
            => SetKeyAndReturn(key);

        IReadyToExecuteNanizationBuildWithFallback ISelfNanizationBuildWithFallback.WithDocument(string document)
            => SetDocumentAndReturn(document);

        IReadyToExecuteNanizationBuild ISelfNanizationBuild.AsSelfPath()
            => AsSelfPathAndReturn();

        IReadyToExecuteNanizationBuild IWithKeyNanizationBuild.WithDocument(string document)
            => SetDocumentAndReturn(document);
        
        IWithKeyNanizationBuildWithFallback IEmptyNanizationBuildWithFallback.WithKey(string key)
            => SetKeyAndReturn(key);

        IWithDocumentNanizationBuildWithFallback IEmptyNanizationBuildWithFallback.WithDocument(string document)
            => SetDocumentAndReturn(document);

        IWithKeyNanizationBuild IEmptyNanizationBuild.WithKey(string key)
            => SetKeyAndReturn(key);

        IReadyToExecuteNanizationBuild ISelfNanizationBuild.WithDocument(string document)
            => SetDocumentFromSourceAndReturn(document);

        ISelfNanizationBuildWithFallback ICanSetFallback<ISelfNanizationBuildWithFallback>.SetFallback(string fallback)
            => SetFallbackAndReturn(fallback);

        IWithKeyNanizationBuildWithFallback ICanSetFallback<IWithKeyNanizationBuildWithFallback>.SetFallback(string fallback)
            => SetFallbackAndReturn(fallback);

        IWithDocumentNanizationBuildWithFallback ICanSetFallback<IWithDocumentNanizationBuildWithFallback>.SetFallback(string fallback)
            => SetFallbackAndReturn(fallback);

        IReadyToExecuteNanizationBuildWithFallback ICanSetFallback<IReadyToExecuteNanizationBuildWithFallback>.SetFallback(string fallback)
            => SetFallbackAndReturn(fallback);
    }
}