using System;
using _Scripts.CoreScene.Player;
using Cysharp.Threading.Tasks;
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
        private GameManager m_gameManager;

        [Inject]
        public void Costruct(PlayerMoneyModel playerMoneyModel, GameManager gameManager)
        {
            m_playerMoneyModel = playerMoneyModel;
            m_playerMoneyModel.OnMoneyOnBoardChanged += MoneyOnBoardChanged;
            m_gameManager = gameManager;
            m_gameManager.LevelFinished += Hide;
            MoneyOnBoardChanged();
        }

        private void MoneyOnBoardChanged()
        {
            AddMoneyAsync().Forget();
        }

        private async UniTask AddMoneyAsync()
        {
            int count = m_playerMoneyModel.MoneyOnBoard;
            for (int i = 0; i <= count; i++)
            {
                MoneyCount.text = i.ToString();
                await UniTask.Delay(100);
            }
        }

        private void Update()
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(MoneyPosition.position);
            m_rectTransform.position = pos;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}