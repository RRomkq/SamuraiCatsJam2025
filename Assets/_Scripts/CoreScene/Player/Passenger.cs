using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _Scripts.CoreScene.Player
{
    public class Passenger: MonoBehaviour
    {
        private int m_money = 2;

        public int Speed;

        public int Money => m_money;
        
        private Transform m_currentTarget;

        public void GoToTarget(Transform target)
        {
            m_currentTarget = target;
        }

        public void Update()
        {
            if (m_currentTarget != null && gameObject.transform.position != m_currentTarget.position)
            {
                gameObject.GoToPositionLerp(m_currentTarget, Speed);
            }
        }
    }
}