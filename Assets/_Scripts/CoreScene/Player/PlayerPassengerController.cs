using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Scripts.CoreScene.Player
{
    public class PlayerPassengerController: MonoBehaviour
    {
        public TextMeshProUGUI PassengersCountText;
        
        private List<Passenger> m_passengers = new List<Passenger>();
        
        public void AddPassenger(Passenger passenger)
        {
            m_passengers.Add(passenger);
            PassengersCountText.text = m_passengers.Count.ToString();
        }
        
        public void RemovePassenger(Passenger passenger)
        {
            m_passengers.Remove(passenger);
            PassengersCountText.text = m_passengers.Count.ToString();
        }
    }
}