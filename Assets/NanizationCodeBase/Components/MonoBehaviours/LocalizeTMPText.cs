using NanizationCodeBase.Components.NotMonoBehaviours;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace NanizationCodeBase.Components.MonoBehaviours
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizeTMPText : MonoBehaviour
    {
        [SerializeField] private string _documentName;
        [SerializeField] private string _key;
        [SerializeField] private string _fallback;
        
        private TMP_Text _tmpText;
        
        private INanizationSubscriber _nanizationSubscriber;

        public UnityEvent<string> OnLocalized; 

        private void Awake()
        {
            _tmpText = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            _nanizationSubscriber = Nanization.Subscribe(_documentName, _key, Localize, _fallback);
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
