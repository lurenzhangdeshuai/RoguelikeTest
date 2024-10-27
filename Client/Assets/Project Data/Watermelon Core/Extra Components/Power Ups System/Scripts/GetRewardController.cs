using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Watermelon
{
    public class GetRewardController: MonoBehaviour
    {
        private PUController powerUpController;
        private RectTransform rectTransform;
        [SerializeField] public Button btnClose;
        [SerializeField] public Button btnOk;
        [SerializeField] public GameObject undo;
        [SerializeField] public GameObject shuffle;
        [SerializeField] public GameObject hit;
        [SerializeField] public GameObject addsolt;
        [SerializeField] public TextMeshProUGUI counttext;
        
        public void Initialise(PUController powerUpController)
        {
            this.powerUpController = powerUpController;
            powerUpController.CloseRewardView();
            rectTransform = (RectTransform)transform;
            btnClose.onClick.AddListener(CloseGetRewardView);
            btnOk.onClick.AddListener(CloseGetRewardView);
        }

        private void CloseGetRewardView()
        {
            powerUpController.CloseRewardView();
        }

        public void OnGetReward(PUType type,int count)
        {
            undo.SetActive(type==PUType.Undo);
            shuffle.SetActive(type==PUType.Shuffle);
            hit.SetActive(type==PUType.Hint);
            addsolt.SetActive(type==PUType.ExtraSlot);
            counttext.text = "x" + count;
        }

    }
}