using TMPro;
using UnityEngine;


namespace Case_1
{
    public class GameCanvasController : MonoBehaviour
    {
        [Header("UI")] [SerializeField] 
        private GameObject inputFieldColumn;
        [SerializeField] private TextMeshProUGUI matchCountText;
        private int minValue = 3, maxValue = 20;


        private int Size
        {
            get
            {
                var textMeshPro = inputFieldColumn.GetComponent<TMP_InputField>();
                int value = int.Parse(textMeshPro.text);
                value = Mathf.Clamp(value, minValue, maxValue);
                textMeshPro.text = value.ToString();
                return value;
            }
        }

        #region MonoBehaviour

        private void OnEnable()
        {
            EventManager.OnChangeMatchCount += SetMatchCountText;
        }

        private void OnDisable()
        {
            EventManager.OnChangeMatchCount -= SetMatchCountText;
        }

        #endregion


        #region Button

        public void RebuildButton()
        {
            EventManager.OnChangeSizeValue?.Invoke(Size);
        }

        #endregion

        void SetMatchCountText(int matchCount)
        {
            matchCountText.text = "Match Count : " + matchCount;
        }
    }
}