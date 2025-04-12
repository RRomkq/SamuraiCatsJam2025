using UnityEngine;

namespace _Scripts.CoreScene.Enviroment
{
    public class MooringChecker: MonoBehaviour
    {
        public Transform Pirs;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerMoveController>(out var playerMoveController))
            {
                playerMoveController.GoToTarget(Pirs);
            }
        }
    }
}