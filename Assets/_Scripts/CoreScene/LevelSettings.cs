using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.CoreScene
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Level/LevelSettings")]
    public class LevelSettings: ScriptableObject
    {
        public int SpawnBaricadesDelay;
        
        public float Speed;
        
        public int BarricadesRawCount;
    }
}