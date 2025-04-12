using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene.Enviroment
{
    public class GroundController: MonoBehaviour
    {
        public List<Material> GroundMaterials;
        
        private GameManager m_gameManager;
        private LevelModel m_levelModel;
        
        [Inject]
        private void Construct(GameManager gameManager, LevelModel levelModel)
        {
            m_gameManager = gameManager;
            m_levelModel = levelModel;
        }

        private void Update()
        {
            if (m_gameManager.ShipState != ShipState.Swimming)
            {
                foreach (var material in GroundMaterials)
                {
                    material.SetFloat("_AllSpeed", 0);
                }
            }
            else
            {
                foreach (var material in GroundMaterials)
                {
                    material.SetFloat("_AllSpeed", m_levelModel.GroundSpeedByLevels[(int)m_levelModel.DifficultyLevel]);
                }
                
            }
        }
    }
}