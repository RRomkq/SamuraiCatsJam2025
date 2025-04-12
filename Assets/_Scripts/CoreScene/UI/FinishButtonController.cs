using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene
{
    public class FinishButtonController: MonoBehaviour
    {
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
            //m_playerMoveController.GoToTarget(screenOutTarget);
            m_screenFadeController.AlphaToAndDoAction(1, 2, ResetPlayer);
        }

        private void ResetPlayer()
        {
            m_levelStarter.StartLevel();
        }
    }
}