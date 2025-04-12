using System;
using _Scripts.CoreScene.Enviroment;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene
{
    public class LevelStarter: MonoBehaviour
    {
        public Transform StartPlayerPosition;
        
        private PlayerMoveController m_playerMoveController;
        private ScreenFadeController m_screenFadeController;
        private LevelModel m_levelModel;
        private LevelsData m_levelsData;
        private GameManager m_gameManager;
        private PirsController m_pirsController;
        private ClickerByCircle m_clickerByCircle;
        private FinishPirsController m_finishPirsController;
        
        private int m_currentLevelIndex = 0;
        
        [Inject]
        public void Construct(PlayerMoveController playerMoveController,
            ScreenFadeController screenFadeController,
            LevelModel levelModel,
            LevelsData levelsData,
            GameManager gameManager,
            PirsController pirsController,
            ClickerByCircle clickerByCircle,
            FinishPirsController finishPirsController)
        {
            m_playerMoveController = playerMoveController;
            m_screenFadeController = screenFadeController;
            m_levelModel = levelModel;
            m_levelsData = levelsData;
            m_gameManager = gameManager;
            m_pirsController = pirsController;
            m_clickerByCircle = clickerByCircle;
            m_finishPirsController = finishPirsController;
        }

        public void Awake()
        {
            StartLevel();
        }

        public void StartLevel()
        {
            if (m_currentLevelIndex == m_levelsData.LevelSettingsMap.Count)
            {
                m_currentLevelIndex--;
            }
            
            m_levelModel.SetLevelSettings(m_levelsData.LevelSettingsMap[m_currentLevelIndex]);
            m_currentLevelIndex++;
            
            m_playerMoveController.GoToTargetInstant(StartPlayerPosition);
            m_screenFadeController.AlphaTo(1, 0);
            m_gameManager.IsLastBarricadeComplete = false;
            m_pirsController.ResetPirs();
            m_finishPirsController.ResetPirs();
            StartLevelAsync().Forget();
        }

        private async UniTask StartLevelAsync()
        {
            await UniTask.DelayFrame(1);
            m_playerMoveController.GoToFirstLine();
            m_screenFadeController.AlphaTo(0, 5);
        }
    }
}