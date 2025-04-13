using System;
using _Scripts.CoreScene.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.CoreScene.Enviroment
{
    public class LastBarricade: MonoBehaviour
    {
        private GameManager m_gameManager;
        private PlayerMoneyController m_playerMoneyController;
        private PassengerOnBoardModel m_passengerOnBoardModel;
        public bool IsReallyLastBarricade;
        private bool m_isDestroy = false;

        public Sprite m_destroySprite;
        public SpriteRenderer m_image;
        
        [Inject]
        public void Construct(GameManager gameManager, PlayerMoneyController playerMoneyController, PassengerOnBoardModel passengerOnBoardModel)
        {
            m_gameManager = gameManager;
            m_playerMoneyController = playerMoneyController;
            m_passengerOnBoardModel = passengerOnBoardModel;
            Destroy(gameObject, 20f);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!m_gameManager.IsLastBarricadeComplete && IsReallyLastBarricade)
                {
                    m_gameManager.IsLastBarricadeComplete = true;
                    m_gameManager.FinishLevel();
                }
                else if (!IsReallyLastBarricade && !m_isDestroy)
                {
                    m_isDestroy = true;
                    m_passengerOnBoardModel.PassengersCount -= 1;
                    m_playerMoneyController.DropMoneyFromBoard(2).Forget();

                    m_image.sprite = m_destroySprite;
                    m_image.sortingOrder = 0;
                }
            }
        }
    }
}