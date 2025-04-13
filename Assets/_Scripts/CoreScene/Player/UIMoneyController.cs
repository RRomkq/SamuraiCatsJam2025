using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene.Player
{
    public class UIMoneyController: MonoBehaviour
    {
        public TextMeshProUGUI MoneyText;
        
        private PlayerMoneyModel m_playerMoneyModel;
        
        [Inject]
        public void Construct(PlayerMoneyModel playerMoneyModel)
        {
            m_playerMoneyModel = playerMoneyModel;
        }

        private void Awake()
        {
            m_playerMoneyModel.OnMoneyChanged += UpdateMoneyText;
            UpdateMoneyText();
        }

        private void UpdateMoneyText()
        {
            MoneyText.text = m_playerMoneyModel.Money.ToString();
        }
    }
}