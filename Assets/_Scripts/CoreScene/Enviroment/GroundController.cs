using UnityEngine;

namespace _Scripts.CoreScene.Enviroment
{
    public class GroundController: MonoBehaviour
    {
        public Transform NotVisionPosition;
        public Transform VisionPosition;
        public float speed;

        private Transform m_target;

        public void GoToVisionPosition()
        {
            m_target = VisionPosition;
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