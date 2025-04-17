using NanizationCodeBase.Components.NotMonoBehaviours;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace NanizationCodeBase.Components.MonoBehaviours
{
    [RequireComponent(typeof(TMP_Text))]
    public class SetLocalizeKeyTMPText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tmpText;
        [SerializeField] private string _documentName;
        [SerializeField] private string _fallback;

        private string _key;
        
        private INanizationSubscriber _nanizationSubscriber;

        public UnityEvent<string> OnLocalized;
        
        #if UNITY_EDITOR
        private void OnValidate() => _tmpText = GetComponent<TMP_Text>();
        #endif

        public void SetKey(string key)
        {
            _key = key;
            
            _nanizationSubscriber.Unsubscribe();

            NewSubscribe();
        }

        private void Start()
        {
            NewSubscribe();
        }

        private void OnDestroy()
        {
            _nanizationSubscriber.Unsubscribe();
        }

        private void NewSubscribe()
        {
            _nanizationSubscriber = Nanization.Subscribe(_documentName, _key, Localize, _fallback);
        }


        private void Localize(string localizedValue)
        {
            _tmpText.SetText(localizedValue);
            
            OnLocalized?.Invoke(localizedValue);
        }
    }
}
