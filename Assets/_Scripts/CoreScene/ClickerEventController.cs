using System;
using System.Collections.Generic;
using _Scripts.CoreScene.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using IInitializable = Unity.VisualScripting.IInitializable;
using Random = UnityEngine.Random;

namespace _Scripts.CoreScene
{
    public class ClickerEventController: MonoBehaviour, IDisposable
    {
        public List<GameObject> Enemies = new List<GameObject>();
        
        private ClickerByCircle m_clickerByCircle;
        private LevelModel m_levelModel;
        private LevelStarter m_levelStarter;
        private PlayerMoneyController m_playerMoneyController;
        private GameManager m_gameManager;

        [Inject]
        public void Construct(ClickerByCircle clickerByCircle,
            LevelModel levelModel,
            LevelStarter levelStarter,
            PlayerMoneyController playerMoneyController,
            GameManager gameManager)
        {
            m_clickerByCircle = clickerByCircle;
            m_levelModel = levelModel;
            m_levelStarter = levelStarter;
            m_playerMoneyController = playerMoneyController;
            m_gameManager = gameManager;
            
            m_clickerByCircle.FinishClickEvent += OnFinishClickEnvent;
            m_gameManager.LevelStarted += OnLevelStarted;
        }

        private void OnFinishClickEnvent(bool result)
        {
            if (!result)
            {
                m_playerMoneyController.DropMoneyFromBoard(2).Forget();
                // TODO: drop passenger
            }
            
            Enemies.ForEach(e => e.SetActive(false));
        }

        private void OnLevelStarted()
        {
            int maxTimeToSpawn = (m_levelModel.BarricadesCount - 2) * m_levelModel.SpawnBaricadesDelay * 1000;

            int delayBeforeStart = Random.Range(0, maxTimeToSpawn);
            
            StartClickEvent(delayBeforeStart).Forget();
        }

        public async UniTask StartClickEvent(int delayBeforeStart)
        {
            await UniTask.Delay(delayBeforeStart);

            m_clickerByCircle.StartClickEvent(m_levelModel.ClickCountForClickerEvent);
            int enemyIndex = Random.Range(0, Enemies.Count);
            Enemies[enemyIndex].SetActive(true);
        }

        public void Dispose()
        {
            m_gameManager.LevelStarted -= OnLevelStarted;
            m_clickerByCircle.FinishClickEvent -= OnFinishClickEnvent;
        }
    }
}