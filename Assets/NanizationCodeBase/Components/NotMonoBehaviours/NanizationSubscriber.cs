using System;
using Naninovel;
using NanizationCodeBase.Model.Data;
using NanizationCodeBase.Services;

namespace NanizationCodeBase.Components.NotMonoBehaviours
{
    public class NanizationSubscriber : INanizationSubscriber
    {
        private readonly Guid _guid;
        
        private readonly string _document;
        private readonly string _key;
        private readonly string _fallback;
        private readonly Action<string> _callback;
        private readonly Action _onDispose;
        
        private bool _isDisposed;
        private bool _isPaused;
        
        public Guid Id => _guid;
        
        public bool IsActive => _isDisposed == false && _isPaused == false;

        public NanizationSubscriber(Guid guid, string document, string key, Action<string> callback, Action onDispose, string fallback = null)
        {
            _guid = guid;
            
            _document = document;
            _key = key;
            _callback = callback;
            _onDispose = onDispose;
            _fallback = fallback;

            SubscribeToLocaleChanged();
        }
        
        public void Localize()
        {
            UpdateValue().Forget();
        }

        public void Pause()
        {
            if (IsActive == false) return;

            UnsubscribeFromLocaleChanged();
            _isPaused = true;
        }

        public void Resume(bool localizeNow = true)
        {
            if (_isDisposed) return;
            
            SubscribeToLocaleChanged();
            
            _isPaused = false;
            
            if (localizeNow) Localize();
        }

        public void Resubscribe()
        {
            if (_isDisposed) return;
            
            SubscribeToLocaleChanged();
            _isPaused = false;
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            UnsubscribeFromLocaleChanged();
            _isDisposed = true;
            
            _onDispose?.Invoke();
        }

        private void SubscribeToLocaleChanged()
        {
            NanizationService.OnLocaleChanged += HandleOnLocaleChanged;
        }

        private void TryUpdateIfActive()
        {
            if (IsActive)
                UpdateValue().Forget();
        }

        private async UniTask UpdateValue()
        {
            var localizedValue = await Nanization.LocalizeAsync(_document, _key, _fallback);
            _callback?.Invoke(localizedValue);
        }

        private void HandleOnLocaleChanged(LocaleChangedArgs args)
        {
            OnLocaleChanged();
        }

        private void OnLocaleChanged()
        {
            TryUpdateIfActive();
        }

        private void UnsubscribeFromLocaleChanged()
        {
            NanizationService.OnLocaleChanged -= HandleOnLocaleChanged;
        }
    }
}