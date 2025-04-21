using NanizationCodeBase.Components.NotMonoBehaviours;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace NanizationCodeBase.Components.MonoBehaviours
{
    [RequireComponent(typeof(TMP_Text))]
    public class ChangeLocalizeKeyTMPText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tmpText;
        [SerializeField] private string _documentName;
        [SerializeField] private string _fallback;
        
        private INanizationSubscriber _nanizationSubscriber;

        public UnityEvent<string> OnLocalized;
        
        #if UNITY_EDITOR
        private void OnValidate() => _tmpText = GetComponent<TMP_Text>();
        #endif
        
        public void ChangeKey(string key)
        {
            _nanizationSubscriber?.Dispose();

            _nanizationSubscriber = Nanization.Bind().WithDocument(_documentName).WithKey(key).SetFallback(_fallback).Subscribe(Localize);
        }

        private void OnDestroy()
        {
            _nanizationSubscriber.Dispose();
        }

        private void Localize(string localizedValue)
        {
            _tmpText.SetText(localizedValue);
            
            OnLocalized?.Invoke(localizedValue);
        }
    }
}
