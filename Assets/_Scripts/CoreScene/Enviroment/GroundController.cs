using System;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene.Enviroment
{
    public class GroundController: MonoBehaviour
    {
        public Material GroundMaterial;
        public Material WaterMaterial;
        
        private GameManager m_gameManager;
        private LevelModel m_levelModel;
        
        [Inject]
        private void Construct(GameManager gameManager, LevelModel levelModel)
        {
            m_gameManager = gameManager;
            m_levelModel = levelModel;
        }

        private void Awake()
        {
            GroundMaterial.SetFloat("_AllSpeed", 0);
        }

        private void Update()
        {
            if (m_gameManager.ShipState != ShipState.Swimming)
            {
                WaterMaterial.SetFloat("_AllSpeed", 0.2f);
            }
            else
            {
                float waterSpeed = m_levelModel.WaterSpeedByLevels[(int)m_levelModel.DifficultyLevel];
                WaterMaterial.SetFloat("_AllSpeed", waterSpeed);
            }
        }

        public void StartGroundMove()
        {
            float groundSpeed = m_levelModel.GroundSpeedByLevels[(int)m_levelModel.DifficultyLevel];
            GroundMaterial.SetFloat("_AllSpeed", groundSpeed);
        }

        public void StopGroundMove()
        {
            GroundMaterial.SetFloat("_AllSpeed", 0);
        }
    }
}