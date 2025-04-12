using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene.Player
{
    public class PlayerPassengerController: MonoBehaviour
    {
        public List<Transform> PassengerPositions;
        
        public TextMeshProUGUI PassengersCountText;
        
        private List<Passenger> m_passengers = new List<Passenger>();
        
        private PlayerResource m_playerResource;
        
        [Inject]
        public void Construct(PlayerResource playerResource)
        {
            m_playerResource = playerResource;
        }
        
        public void AddPassenger(Passenger passenger)
        {
            m_passengers.Add(passenger);
            PassengersCountText.text = m_passengers.Count.ToString();
            passenger.GoToTarget(PassengerPositions[m_passengers.Count - 1]);
        }
        
        public void RemovePassenger(Passenger passenger)
        {
            m_passengers.Remove(passenger);
            PassengersCountText.text = m_passengers.Count.ToString();
            m_playerResource.Money += passenger.Money;
        }
    }
}