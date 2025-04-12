using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene.Enviroment
{
    public class GroundController: MonoBehaviour
    {
        public Transform NotVisionPosition;
        public Transform VisionPosition;
        public float speed;
        public Transform PirsPosition;

        private Transform m_target;
        
        private PlayerMoveController m_playerMoveController;
        
        [Inject]
        public void Construct(PlayerMoveController playerMoveController)
        {
            m_playerMoveController = playerMoveController;
        }

        public void GoToVisionPosition()
        {
            m_target = VisionPosition;
            //m_playerMoveController.GoToTarget(PirsPosition);
        }

        public void GoToNotVisionPosition()
        {
            m_target = NotVisionPosition;
        }
        
        private void Update()
        {
            // Плавно перемещаем объект к целевому Transform
            if (m_target != null)
            {
                // Плавное движение с использованием Lerp (линейной интерполяции)
                transform.position = Vector3.Lerp(transform.position, m_target.position, speed * Time.deltaTime);
            }
        }
    }
}