using _Scripts.CoreScene.Enviroment;

namespace _Scripts.CoreScene
{
    public class LevelModel
    {
        private LevelSettings m_levelSettings;

        public void SetLevelSettings(LevelSettings levelSettings) => m_levelSettings = levelSettings;
        
        public int LevelDurationInSeconds => m_levelSettings.LevelDurationInSeconds;

        public int SpawnBaricadesDelay => m_levelSettings.SpawnBaricadesDelay;
        
        public float MaxSlowdownFactor => m_levelSettings.MaxSlowdownFactor;

        public float Speed => m_levelSettings.Speed;
        
        public int PassengersCount => m_levelSettings.PassengersCount;

        public DifficultyLevel DifficultyLevel { get; set; } = DifficultyLevel.Hard;
        
        public int BarricadesCount => m_levelSettings.BarricadesRawCount;
    }
}