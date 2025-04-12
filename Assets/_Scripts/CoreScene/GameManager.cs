using System;

namespace _Scripts.CoreScene
{
    public class GameManager
    {
        private bool m_isTransports = false;
        
        public bool IsTransports => m_isTransports;

        public event Action StartTransports;
        
        public event Action FinishTransports;
    }
}