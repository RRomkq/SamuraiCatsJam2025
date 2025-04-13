using System;
using UnityEngine;

namespace _Scripts.CoreScene.Enviroment
{
    public class Tentakle: MonoBehaviour
    {
        public AudioSource AudioSource;

        private void OnEnable()
        {
            AudioSource.Play();
        }
    }
}