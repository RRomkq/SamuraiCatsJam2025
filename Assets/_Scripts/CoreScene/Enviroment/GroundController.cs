using System;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene.Enviroment
{
    public class GroundController: MonoBehaviour
    {
        public Transform NotVisionPosition;
        public Transform VisionPosition;
        public float speed;

        private Transform m_target;
        
        private GameManager m_gameManager;
        
        [Inject]
        public void Construct(GameManager gameManager)
        {
            m_gameManager = gameManager;
        }

        private void Awake()
        {
            m_gameManager.StartTransports += OnStartTransports;
        }

        private void OnDisable()
        {
            m_gameManager.StartTransports -= OnStartTransports;
        }

        private void OnStartTransports()
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