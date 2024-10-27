using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Watermelon.IAPStore
{
    public class RandomArticleForCoins: MonoBehaviour, IIAPStoreOffer
    {
        [SerializeField] int coinsAmount;
        [SerializeField] TMP_Text Price;

        [Space]
        [SerializeField] Button button;

        [Space]
        [SerializeField] RectTransform cloudSpawnRectTransform;
        [SerializeField] int floatingElementsAmount = 10;

        public GameObject GameObject => gameObject;

        private RectTransform rect;
        public float Height => rect.sizeDelta.y;
        public void OnOpen()
        {
            
        }

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }

        public void Init()
        {
            button.onClick.AddListener(OnAdButtonClicked);
            Price.text = $"x{coinsAmount}";
        }

        private void OnAdButtonClicked()
        {
            AudioController.PlaySound(AudioController.Sounds.buttonSound);
            int type= Random.Range(0, 3);
            PUController.AddPowerUp((PUType)type,floatingElementsAmount);
            PUController.OpenRewardView((PUType)type,floatingElementsAmount);
        }
    }
}