using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.CoreScene.Enviroment
{
    public class LineHandler : MonoBehaviour
    {
        public List<Transform> HaronLines;
        public List<Transform> BarricadeLines;

        private Vector3 m_directionToHarold = Vector3.zero;

        public Vector3 DirectionToHarold
        {
            get
            {
                if (m_directionToHarold == Vector3.zero)
                {
                    m_directionToHarold = (HaronLines.First().position - BarricadeLines.First().position).normalized;
                }

                return m_directionToHarold;
            }
        }
    }
}