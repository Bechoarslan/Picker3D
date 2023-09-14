using System;
using RunTime.Commands.Player;
using RunTime.Controllers.Player;
using RunTime.Data.UnityObjects;
using RunTime.Data.ValueObjects;
using RunTime.Keys;
using RunTime.Signal;
using UnityEngine;

namespace RunTime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public byte StageValue;
        internal ForceBallsToPoolCommand ForceCommand;

        #endregion

        #region  Private Variables

        private PlayerData _data;



        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerMeshController playerMeshController;
        [SerializeField] private PlayerPhysicsController playerPhysicsController;

        #endregion

        #endregion


        private void Awake()
        {
            _data = GetPlayerData();
            SetDataToController();
            Init();
        }
        
       

        private void Init()
        {
            ForceCommand = new ForceBallsToPoolCommand(this, _data.PlayerForceData);
        }
        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }

        private void SetDataToController()
        {
            playerMovementController.SetData(_data.MovementData);
            playerMeshController.SetMeshData(_data.PlayerMeshData);
        }

        

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            InputSignals.Instance.onInputTaken += () => playerMovementController.IsReadyToMove(true);
            InputSignals.Instance.onInputReleased += () => playerMovementController.IsReadyToMove(false);
            InputSignals.Instance.onInputDragged += OnInputDragged;
            UISignals.Instance.onPlay += () => playerMovementController.IsReadyToPlay(true);
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onStageAreaSuccessful += OnStageAreaSuccesful;
            CoreGameSignals.Instance.onFinishAreaEntered += OnFinishAreaEntered;
            CoreGameSignals.Instance.onStageAreaEntered += () =>playerMovementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onLevelSuccessful +=() => playerMovementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onLevelFail += () => playerMovementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onMiniGameAreaEntered += OnMiniGameAreaEntered;
            CoreGameSignals.Instance.onSetMultiplier += playerPhysicsController.SetMultiplier;
            CoreGameSignals.Instance.onGetMultiplyValue += playerPhysicsController.GetMultiplyValue;
            


        }

       

        private void OnInputDragged(HorizontalInputParams inputParams)
        {
            playerMovementController.UpdateInputParams(inputParams);
        }


        private void OnStageAreaSuccesful(byte value)
        {
            StageValue = ++value;
            playerMovementController.IsReadyToPlay(true);
            playerMeshController.ScaleUpPlayer();
            playerMeshController.PlayConfettiParticle();
            playerMeshController.ShowText();
            
        }
        private void OnMiniGameAreaEntered(short value)
        {
            if (CoreGameSignals.Instance.onWhichMiniGameAreaEntered?.Invoke() == 1)
            {
                playerMovementController.IsReadyToPlay(false);
                var newForwardSpeed = (_data.MovementData.ForwardSpeed * value / 100) + _data.MovementData.ForwardSpeed;
                _data.MovementData.ForwardSpeed = newForwardSpeed;
                
            }
            else
            {
                playerMovementController.IsReadyToPlay(false);
                var newForwardSpeed = (_data.MovementData.ForwardSpeed * value /50) + _data.MovementData.ForwardSpeed;
                _data.MovementData.ForwardSpeed = newForwardSpeed;
                
            }
           
        }

        private void OnFinishAreaEntered()
        {
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            // player minigames
        }
        

        private void OnReset()
        {
            StageValue = 0;
            playerMovementController.OnReset();
            playerMeshController.OnReset();
            playerPhysicsController.OnReset();
            _data.MovementData.ForwardSpeed = 9;

        }

        private void UnSubscribeEvent()
        {
            InputSignals.Instance.onInputReleased -= () =>playerMovementController.IsReadyToMove(false);
            InputSignals.Instance.onInputTaken -= () =>playerMovementController.IsReadyToMove(true);
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            UISignals.Instance.onPlay -= () => playerMovementController.IsReadyToPlay(true);
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnStageAreaSuccesful;
            CoreGameSignals.Instance.onFinishAreaEntered -= OnFinishAreaEntered;
            CoreGameSignals.Instance.onStageAreaEntered -= () =>playerMovementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onLevelSuccessful -= () =>playerMovementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onLevelFail -= () =>playerMovementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onMiniGameAreaEntered -= OnMiniGameAreaEntered;
            CoreGameSignals.Instance.onSetMultiplier -= playerPhysicsController.SetMultiplier;
            CoreGameSignals.Instance.onGetMultiplyValue -= playerPhysicsController.GetMultiplyValue;
           
            
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }
    }

    
}