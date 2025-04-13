using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.CoreScene
{
    [CreateAssetMenu(fileName = "GlobalGameSettings", menuName = "ScriptableObjects/GlobalGameSettings")]
    public class GlobalGameSettings: ScriptableObject
    {
        public List<int> NeedMoneyForPassenger;
        
        public List<int> MinMoneyNeedForDifficultyLevel;

        public int WinMoneyCount;
        
        public List<float> GroundSpeedByLevel = new List<float>();
        
        public List<float> WaterSpeedByLevel = new List<float>();
    }
}