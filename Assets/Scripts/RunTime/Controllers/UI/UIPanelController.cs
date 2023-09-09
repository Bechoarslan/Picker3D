using System;
using System.Collections.Generic;
using RunTime.Enums;
using RunTime.Signal;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace RunTime.Controllers.UI
{
    public class UIPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialize Variables

         [SerializeField] private List<Transform> layers = new List<Transform>();

        #endregion

        #endregion


        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            CoreUISignals.Instance.onClosePanel += OnClosePanel;
            CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
            CoreUISignals.Instance.onCloseAllPanels += OnCloseAllPanels;
        }
        
        [Button("Open Panel")]
        private void OnOpenPanel(UIPanelTypes panelType, int value)
        {
            OnClosePanel(value);
            Instantiate(Resources.Load<GameObject>($"Screens/{panelType}Panel"),layers[value]);

        }
   
        [Button("Close All Panels")]
        private void OnCloseAllPanels()
        {
            foreach (var layer in layers)
            {
                if (layer.childCount <= 0) continue;
                
                    Destroy(layer.GetChild(0).gameObject);
                

                

            }
        }
        [Button("One Panel")]
        private void OnClosePanel(int value)
        {
            if (layers[value].childCount <= 0) return;
              Destroy(layers[value].GetChild(0).gameObject);

  
        }

       
        
        
        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        private void UnSubscribeEvent()
        {
            CoreUISignals.Instance.onClosePanel -= OnClosePanel;
            CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
            CoreUISignals.Instance.onCloseAllPanels -= OnCloseAllPanels;
            
        }
    }
}