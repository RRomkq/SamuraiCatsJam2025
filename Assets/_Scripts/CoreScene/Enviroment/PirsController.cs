using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene.Enviroment
{
    public class PirsController: MonoBehaviour
    {
        private PassengerSpawner m_passengerSpawner;
        
        [Inject]
        public void Construct(PassengerSpawner passengerSpawner)
        {
            m_passengerSpawner = passengerSpawner;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                m_passengerSpawner.StartSpawnPassengersForLevel().Forget();
            }
        }
    }
}