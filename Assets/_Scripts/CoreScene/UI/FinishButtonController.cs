using System;
using _Scripts.CoreScene.Player;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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

        private Tween m_tween;
        
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
            m_tween = m_playerMoveController.MoveTo(screenOutTarget, DURATION * 2);
            m_screenFadeController.AlphaToAndDoAction(1, DURATION, ResetPlayer);
        }

        private void ResetPlayer()
        {
            if (m_tween != null)
            {
                m_tween.Kill();
                m_tween = null;
            }
            
            m_levelStarter.StartLevel();
        }
    }
}