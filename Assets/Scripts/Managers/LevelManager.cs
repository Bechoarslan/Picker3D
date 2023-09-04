﻿using System;
using Commands;
using Data.UnityObjects;
using Data.ValueObjects;
using Signal;

using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Seriliziable Variables

        [SerializeField] private Transform levelHolder;
        [SerializeField] private byte totalLevelCount;

        #endregion

        #region Private Values

        private LevelData _levelData;
        private short _currentLevel;

        private LevelLoaderCommand _levelLoaderCommand;
        private LevelDestroyerCommand _levelDestroyerCommand;
        
        #endregion

        #endregion

        private void Awake()
        {
            _levelData = GetLevelData();
            _currentLevel = GetActiveLevel();
            Init();
        }
        private void Init()
        {
            _levelLoaderCommand = new LevelLoaderCommand(levelHolder);
            _levelDestroyerCommand = new LevelDestroyerCommand(levelHolder);

        }

        private byte GetActiveLevel()
        {
            return (byte)_currentLevel;
        }

        private LevelData GetLevelData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level").Levels[_currentLevel];
        }

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            CoreGameSignals.Instance.onLevelInitialize += _levelLoaderCommand.Execute;
            CoreGameSignals.Instance.onClearActiveLevel += _levelDestroyerCommand.Execute;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
            CoreGameSignals.Instance.onGetLevelValue += GetLevelValue;
        }

        private byte GetLevelValue()
        {
            return (byte) _currentLevel;
        }

        private void OnNextLevel()
        {
            _currentLevel++;
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)_currentLevel);
        }

        private void OnRestartLevel()
        {   CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();;
            CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)_currentLevel);
            
        }
        
        private void UnSubscribeEvent()
        {
            CoreGameSignals.Instance.onLevelInitialize -= _levelLoaderCommand.Execute;
            CoreGameSignals.Instance.onClearActiveLevel -= _levelDestroyerCommand.Execute;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
            CoreGameSignals.Instance.onGetLevelValue -= GetLevelValue;
            
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        private void Start()
        {
            CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
        }
    }
}