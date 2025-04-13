using System.Collections.Generic;
using _Scripts.CoreScene.Enviroment;
using _Scripts.CoreScene.Player;
using Zenject;

namespace _Scripts.CoreScene
{
    public class LevelModel
    {
        private LevelSettings m_levelSettings;
        private GlobalGameSettings m_globalGameSettings;
        private PlayerMoneyModel m_playerMoneyModel;

        [Inject]
        public void Construct(GlobalGameSettings globalGameSettings, PlayerMoneyModel playerMoneyModel)
        {
            m_globalGameSettings = globalGameSettings;
            m_playerMoneyModel = playerMoneyModel;
        }
        
        public void SetLevelSettings(LevelSettings levelSettings)
        {
            m_levelSettings = levelSettings;
        }
        
        public int SpawnBaricadesDelay => m_levelSettings.SpawnBaricadesDelay;
        
        public float Speed => m_levelSettings.Speed;

        public DifficultyLevel DifficultyLevel
        {
            get
            {
                var findLast = m_globalGameSettings.MinMoneyNeedForDifficultyLevel.FindLastIndex(need => need <= m_playerMoneyModel.Money);

                return (DifficultyLevel) findLast;
            }
        }

        public int BarricadesCount => m_levelSettings.BarricadesRawCount;
        
        public List<float> GroundSpeedByLevels => m_globalGameSettings.GroundSpeedByLevel;
        
        public List<float> WaterSpeedByLevels => m_globalGameSettings.WaterSpeedByLevel;

        public List<int> NeedMoneyForPassenger => m_globalGameSettings.NeedMoneyForPassenger;
        
        public int WinMoney => m_globalGameSettings.WinMoneyCount;
        
        public float HoronSpeed => m_levelSettings.HoronSpeed;
        
        public int ClickCountForClickerEvent => m_levelSettings.ClickCountForClickerEvent;
        
    }
}