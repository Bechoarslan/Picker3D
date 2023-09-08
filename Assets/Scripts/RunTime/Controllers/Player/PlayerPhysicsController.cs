using System;
using RunTime.Managers;
using RunTime.Signal;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Rigidbody rigidbody;
        

        #endregion

        #region Private Variables
        private readonly string _stageArea = "StageArea";
        private readonly string _finish = "FinishArea";
        private readonly string _miniGame = "MiniGameArea";




        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_stageArea))
            {
                manager.ForceCommand.Execute();
                CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
            }

            if (other.CompareTag(_finish))
            {
                CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
                return;

            }

            if (other.CompareTag(_miniGame))
            {
                // minigame
            }
        }

        internal void OnReset()
        {
            
        }
    }
}