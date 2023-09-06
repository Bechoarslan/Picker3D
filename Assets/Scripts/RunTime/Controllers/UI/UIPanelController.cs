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

   
        private void OnCloseAllPanels()
        {
            foreach (var layer in layers)
            { 
                if (layer.childCount <= 0) return;
#if UNITY_EDITOR           
                DestroyImmediate(layer.GetChild(0).gameObject);
               
#else 
              Destroy(layer.GetChild(0).gameObject);
                
#endif

            }
        }

      
        private void OnClosePanel(int value)
        {
            if (layers[value].childCount <= 0) return;
#if UNITY_EDITOR           
            DestroyImmediate(layers[value].GetChild(0).gameObject);
#else 
              Destroy(layers[value].GetChild(0).gameObject);
#endif
  
        }

       
        private void OnOpenPanel(UIPanelTypes panelType, int value)
        {
            OnClosePanel(value);
            Instantiate(Resources.Load<GameObject>($"Screens/{panelType}Panel"),layers[value]);

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