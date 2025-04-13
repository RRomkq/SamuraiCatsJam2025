using System;
using _Scripts.CoreScene.Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene
{
    public class BoardResController: MonoBehaviour
    {
        public TextMeshProUGUI MoneyCount;
        public Transform MoneyPosition;
        public RectTransform m_rectTransform;

        private PlayerMoneyModel m_playerMoneyModel;

        [Inject]
        public void Costruct(PlayerMoneyModel playerMoneyModel)
        {
            m_playerMoneyModel = playerMoneyModel;
            m_playerMoneyModel.OnMoneyOnBoardChanged += MoneyOnBoardChanged;
            MoneyOnBoardChanged();
        }

        private void MoneyOnBoardChanged()
        {
            MoneyCount.text = m_playerMoneyModel.MoneyOnBoard.ToString();
        }

        private void Update()
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(MoneyPosition.position);
            m_rectTransform.position = pos;
        }
    }
}