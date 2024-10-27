using Assets.HeroEditor.Common.CommonScripts;
using TMPro;
using Unity.VisualScripting;
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
        public void OnOpen()
        {
            SetImage();
        }

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }

        public void Init()
        {
            button.onClick.AddListener(OnAdButtonClicked);
        }

        public void SetImage()
        {
            share.SetActive(hasShear);
            ads.SetActive(!hasShear);
        }

        private void OnAdButtonClicked()
        {
            if (hasShear)
            {
                ShareManager.Share((reward =>
                {
                    hasShear = false;
                    SetImage();
                    PUController.AddPowerUp(type,floatingElementsAmount);
                    PUController.OpenRewardView(type,floatingElementsAmount);
                }));
            }
            else
            {
                AdsManager.ShowRewardBasedVideo((watched) =>
                {
                    PUController.AddPowerUp(type,floatingElementsAmount);
                    PUController.OpenRewardView(type,floatingElementsAmount);
                });
            }

            
        }
    }
}