using System.Collections.Generic;
using _Scripts.CoreScene.Player;
using UnityEngine;

namespace _Scripts.CoreScene
{
    public class PassengerQ: MonoBehaviour
    {
        public List<Transform> PassengerPositions = new List<Transform>();
        
        public List<Passenger> Passengers = new List<Passenger>();

        public void AddPassenger(Passenger passenger)
        {
            passenger.GoToTarget(PassengerPositions[Passengers.Count]);
            Passengers.Add(passenger);
        }
    }
}