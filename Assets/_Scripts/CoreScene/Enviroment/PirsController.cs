using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene.Enviroment
{
    public class PirsController: MonoBehaviour
    {
        private LineHandler m_lineHandler;
        private GameManager m_gameManager;
        private LevelModel m_levelModel;
        private bool m_alreadyStart = false;
        public Transform VisionPosition;

        [Inject]
        private void Construct(LineHandler lineHandler, LevelModel levelModel, GameManager gameManager)
        {
            m_lineHandler = lineHandler;
            m_levelModel = levelModel;
            m_gameManager = gameManager;
        }
        
        private void Update()
        {
            if (m_gameManager.ShipState == ShipState.Swimming || m_alreadyStart)
            {
                m_alreadyStart = true;
                transform.position += m_lineHandler.DirectionToHarold * m_levelModel.Speed * Time.deltaTime;
            }
        }

        public void ResetPirs()
        {
            m_alreadyStart = false;
            transform.position = VisionPosition.position;
        }
    }
}