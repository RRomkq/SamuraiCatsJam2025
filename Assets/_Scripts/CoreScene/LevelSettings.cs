using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
        
        public List<float> GroundSpeedByLevel = new List<float>();
        
        public List<float> WaterSpeedByLevel = new List<float>();

        [FormerlySerializedAs("PassengersCountByBoardGrade")] public List<int> NeedMoneyForPassenger;

        public int WinMoneyCount;
    }
}