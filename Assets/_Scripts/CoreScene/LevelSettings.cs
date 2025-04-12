using UnityEngine;

namespace _Scripts.CoreScene
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Level/LevelSettings")]
    public class LevelSettings: ScriptableObject
    {
        public int LevelDurationInSeconds;

        public float WaterFlowForce;
        
        public int SpawnBaricadesDelay;
        
        public float MaxSlowdownFactor;

        public float Speed;
        
        public int PassengersCount;
        
        public int BarricadesRawCount;
    }
}