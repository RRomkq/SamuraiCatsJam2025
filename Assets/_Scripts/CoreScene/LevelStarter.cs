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
        
        private int m_currentLevelIndex = 0;
        
        [Inject]
        public void Construct(PlayerMoveController playerMoveController,
            ScreenFadeController screenFadeController,
            LevelModel levelModel,
            LevelsData levelsData)
        {
            m_playerMoveController = playerMoveController;
            m_screenFadeController = screenFadeController;
            m_levelModel = levelModel;
            m_levelsData = levelsData;
            
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
            m_screenFadeController.AlphaTo(0, 2);
        }
    }
}