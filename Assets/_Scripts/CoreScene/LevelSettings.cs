using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.CoreScene
{
    [Serializable]
    public class LevelSettings
    {
        public int SpawnBaricadesDelay;
        
        public float Speed;
        
        public int BarricadesRawCount;

        public int HoronSpeed;

        public int ClickCountForClickerEvent;
    }
}