using System.Collections.Generic;
using DG.Tweening;
using RunTime.Signal;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace RunTime.Controllers.UI
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables


        [SerializeField] private List<Image> stageImages = new List<Image>();
        [SerializeField] private List<TextMeshProUGUI> levelTexts = new List<TextMeshProUGUI>();

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onSetNewLevelValue += OnSetNewLevelValue;
            UISignals.Instance.onSetStageColor += OnSetStageColor;
        }

       
        [Button]
        private void OnSetStageColor(byte levelColor)
        {
            stageImages[levelColor].DOColor(new Color(0.6156863f, 0.2235294f, 0.07058824f), 0.5f);

        }
        

        
        private void OnSetNewLevelValue(byte levelValue)
        {
            var levelTextValue = ++levelValue;
            levelTexts[0].text = levelTextValue.ToString();
            levelTextValue++;
            levelTexts[1].text = levelTextValue.ToString();

        }

        private void UnSubscribeEvents()
        {
            UISignals.Instance.onSetNewLevelValue -= OnSetNewLevelValue;
            UISignals.Instance.onSetStageColor -= OnSetStageColor;
        }

        private void OnDisable()
        {
            
            UnSubscribeEvents();
        }
    }
}