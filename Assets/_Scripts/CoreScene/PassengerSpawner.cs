using _Scripts.CoreScene.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene
{
    public class PassengerSpawner: MonoBehaviour
    {
        private LevelModel m_levelModel;
        private IInstantiator m_instantiator;
        private PassengerQ m_passengerQ;
        
        public int SpawnTimeInMilliseconds = 1000;
        public GameObject PassengerPrefab;
        
        [Inject]
        public void Constructor(LevelModel levelModel,
            IInstantiator instantiator,
            PassengerQ passengerQ)
        {
            m_levelModel = levelModel;
            m_instantiator = instantiator;
            m_passengerQ = passengerQ;
        }
        
        public async UniTask StartSpawnPassengersForLevel()
        {
            for (int i = 0; i < m_levelModel.PassengersCount; i++)
            {
                GameObject passengerGo = m_instantiator.InstantiatePrefab(PassengerPrefab, transform.position, Quaternion.identity, transform);

                Passenger passenger = passengerGo.GetComponent<Passenger>();
                m_passengerQ.AddPassenger(passenger);

                await UniTask.Delay(SpawnTimeInMilliseconds);
            }
        }
    }
}