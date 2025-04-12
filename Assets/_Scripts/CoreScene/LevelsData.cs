using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.CoreScene
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "Level/LevelsData")]
    public class LevelsData: ScriptableObject
    {
        public List<LevelSettings> LevelSettingsMap;
    }
}