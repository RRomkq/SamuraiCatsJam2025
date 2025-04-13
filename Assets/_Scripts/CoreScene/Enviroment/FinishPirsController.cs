using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene.Enviroment
{
    public class FinishPirsController: MonoBehaviour
    {
        private LineHandler m_lineHandler;
        private LevelModel m_levelModel;
        private bool m_goToVisionPosition = false;
        public Transform VisionPosition;
        private Vector3 m_startPosition;

        private GroundController m_groundController;

        [Inject]
        private void Construct(LineHandler lineHandler, LevelModel levelModel, GameManager gameManager, GroundController groundController)
        {
            m_lineHandler = lineHandler;
            m_levelModel = levelModel;
            m_startPosition = transform.position;
            m_groundController = groundController;
        }
        
        public void GoPirsToVisionPosition(Action action, float duration)
        {
            transform.DOMove(VisionPosition.position, duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                m_groundController.StopGroundMove();
                action.Invoke();
            });
        }

        public void ResetPirs()
        {
            m_goToVisionPosition = false;
            transform.position = m_startPosition;
        }
    }
}