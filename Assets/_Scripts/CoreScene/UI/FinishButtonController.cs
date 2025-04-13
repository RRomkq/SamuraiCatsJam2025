using System;
using _Scripts.CoreScene.Player;
using Cysharp.Threading.Tasks;
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
        private FinalLevelWindowController m_finalLevelWindowController;
        private PlayerMoneyController m_playerMoneyController;
        
        [Inject]
        public void Construct(PlayerMoveController playerMoveController,
            ScreenFadeController screenFadeController,
            LevelStarter levelStarter,
            FinalLevelWindowController finalLevelWindowController,
            PlayerMoneyController playerMoneyController
            )
        {
            m_playerMoveController = playerMoveController;
            m_screenFadeController = screenFadeController;
            m_levelStarter = levelStarter;
            m_finalLevelWindowController = finalLevelWindowController;
            m_playerMoneyController = playerMoneyController;
        }

        public void FinishLevel()
        {
            m_finalLevelWindowController.Hide();
            m_playerMoneyController.GetMoneyFromBoard().Forget();
            m_playerMoveController.MoveTo(screenOutTarget, DURATION * 2);
            m_screenFadeController.AlphaToAndDoAction(1, DURATION * 2, ResetPlayer);
        }

        private void ResetPlayer()
        {
            m_levelStarter.StartLevel();
        }
    }
}