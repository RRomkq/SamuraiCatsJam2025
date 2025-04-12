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

        [Inject]
        private void Construct(LineHandler lineHandler, LevelModel levelModel, GameManager gameManager)
        {
            m_lineHandler = lineHandler;
            m_levelModel = levelModel;
            m_startPosition = transform.position;
        }
        
        public void GoPirsToVisionPosition()
        {
            transform.DOMove(VisionPosition.position, 3f);
        }

        public void ResetPirs()
        {
            m_goToVisionPosition = false;
            transform.position = m_startPosition;
        }
    }
}