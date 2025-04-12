using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene
{
    public class FinishButtonController: MonoBehaviour
    {
        private const int DURATION = 2;
        
        public Transform screenOutTarget;
        
        private PlayerMoveController m_playerMoveController;
        private ScreenFadeController m_screenFadeController;
        private LevelStarter m_levelStarter;
        
        [Inject]
        public void Construct(PlayerMoveController playerMoveController,
            ScreenFadeController screenFadeController,
            LevelStarter levelStarter)
        {
            m_playerMoveController = playerMoveController;
            m_screenFadeController = screenFadeController;
            m_levelStarter = levelStarter;
        }

        public void FinishLevel()
        {
            m_playerMoveController.MoveTo(screenOutTarget, DURATION * 2);
            m_screenFadeController.AlphaToAndDoAction(1, DURATION, ResetPlayer);
        }

        private void ResetPlayer()
        {
            m_levelStarter.StartLevel();
        }
    }
}