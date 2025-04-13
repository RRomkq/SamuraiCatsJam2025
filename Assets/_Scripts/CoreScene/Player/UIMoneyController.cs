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
        private LevelModel m_levelModel;
        
        [Inject]
        public void Construct(PlayerMoneyModel playerMoneyModel, LevelModel levelModel)
        {
            m_playerMoneyModel = playerMoneyModel;
            m_levelModel = levelModel;
        }

        private void Awake()
        {
            m_playerMoneyModel.OnMoneyChanged += UpdateMoneyText;
            UpdateMoneyText();
        }

        private void UpdateMoneyText()
        {
            MoneyText.text = m_playerMoneyModel.Money.ToString() + "/" + m_levelModel.WinMoney;
        }
    }
}