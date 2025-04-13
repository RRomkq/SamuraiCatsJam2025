// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.CoreScene.Enviroment
{
    public class StartPirsGhostsPositionHelper : MonoBehaviour
    {
        private List<StartPirsSpawnPoint> m_points;

        private Dictionary<StartPirsSpawnPoint, bool> m_bisyPoints;
        
        private void Awake()
        {
            m_points = GetComponentsInChildren<StartPirsSpawnPoint>().ToList();

            m_bisyPoints = new Dictionary<StartPirsSpawnPoint, bool>();
            foreach (var startPirsSpawnPoint in m_points)
            {
                m_bisyPoints.Add(startPirsSpawnPoint, false);
            }
        }

        public void Reset()
        {
            foreach (var (key, value) in m_bisyPoints)
            {
                m_bisyPoints[key] = false;
            }
        }
        
        public void Spawn(GameObject ghost)
        {
            StartPirsSpawnPoint point = GetFirstFreePoint();
            ghost.transform.position = point.transform.position;
            m_bisyPoints[point] = true;
        }

        private StartPirsSpawnPoint GetFirstFreePoint()
        {
            return m_bisyPoints.FirstOrDefault(kv => kv.Value == false).Key;
        }
    }
}