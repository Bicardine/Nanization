using System;
using Naninovel;

namespace NanizationCodeBase.Services
{
    public static class NanizationService
    {
        private static ITextManager _textManager;
        private static ILocalizationManager _localizationManager;
        
        private static UniTaskCompletionSource<bool> _initializationSource;
        
        public static event Action<LocaleChangedArgs> OnLocaleChanged;

        static NanizationService()
        {
            EnsureInitialized();
        }

        public static async UniTask<ITextManager> GetTextManagerAsync()
        {
            if (Engine.Initialized == false)
                await _initializationSource.Task;

            return _textManager;
        }

        public static ITextManager GetTextManager()
        {
            return _textManager;
        }

        public static async UniTask<ILocalizationManager> GetLocalizationManager()
        {
            if (Engine.Initialized == false)
                await _initializationSource.Task;
            
            return _localizationManager;
        }

        private static void EnsureInitialized()
        {
            _initializationSource = new UniTaskCompletionSource<bool>();

            if (Engine.Initialized)
                CompleteInitialization();
            else
                Engine.OnInitializationFinished += OnEngineInitialized;
        }

        private static void OnEngineInitialized()
        {
            Engine.OnInitializationFinished -= OnEngineInitialized;
            CompleteInitialization();
        }

        private static void CompleteInitialization()
        {
            _textManager = Engine.GetService<ITextManager>();
            _localizationManager = Engine.GetService<ILocalizationManager>();
            _localizationManager.OnLocaleChanged += HandleOnLocaleChanged;

            Engine.OnDestroyed += OnDestroy;
            
            _initializationSource.TrySetResult(true);
        }

        private static void HandleOnLocaleChanged(LocaleChangedArgs localeChangedArgs)
        {
            OnLocaleChanged?.Invoke(localeChangedArgs);
        }

        private static void OnDestroy()
        {
            Engine.OnDestroyed -= OnDestroy;
            Dispose();
        }

        private static void Dispose()
        {
            _localizationManager.OnLocaleChanged -= HandleOnLocaleChanged;
        }
    }
}