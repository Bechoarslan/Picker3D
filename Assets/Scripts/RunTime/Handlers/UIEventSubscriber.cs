using System;
using RunTime.Enums;
using RunTime.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace RunTime.Handlers
{
    public class UIEventSubscriber : MonoBehaviour
    {
        #region Self Variables

        #region Seriliziable Variables

        [SerializeField] private UIEventSubscriberTypes types;
        

        

        #endregion

        #region Private Variables

        private UIManager _manager;
        private Button _button;

        #endregion

        #endregion


        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _manager = FindObjectOfType<UIManager>();
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            switch (types)
            {
                case UIEventSubscriberTypes.Play:
                    _button.onClick.AddListener(_manager.Play);
                    break;
                case UIEventSubscriberTypes.OnNextLevel:
                    _button.onClick.AddListener(_manager.NextLevel);
                    break;
                case UIEventSubscriberTypes.OnFailLevel:
                    _button.onClick.AddListener(_manager.LevelFailed);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();        
                
            }
            
        }

        private void OnDisable()
        {
            switch (types)
            {       
                case UIEventSubscriberTypes.Play:
                    _button.onClick.RemoveListener(_manager.Play);
                    break;
                case UIEventSubscriberTypes.OnNextLevel:
                    _button.onClick.RemoveListener(_manager.NextLevel);
                    break;
                case UIEventSubscriberTypes.OnFailLevel:
                    _button.onClick.RemoveListener(_manager.LevelFailed);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();        
                
            }
        }
    }
}