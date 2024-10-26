using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Watermelon.IAPStore
{
    public class AdsGetArticle: MonoBehaviour, IIAPStoreOffer
    {
        
        [Space]
        [SerializeField] Button button;
        [SerializeField] PUType type;
        [SerializeField] Image share;
        [SerializeField] Image ads;
        public bool hasShear;

        [Space]
        [SerializeField] RectTransform cloudSpawnRectTransform;
        [SerializeField] int floatingElementsAmount = 10;

        public GameObject GameObject => gameObject;

        private RectTransform rect;
        public float Height => rect.sizeDelta.y;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }

        public void Init()
        {
            button.onClick.AddListener(OnAdButtonClicked);
            
        }

        private void OnAdButtonClicked()
        {
          
        }
    }
}