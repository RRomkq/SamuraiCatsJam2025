using System.Collections.Generic;
using System.Linq;
using _Scripts.CoreScene.Player;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene
{
    public class FinalLevelWindowController: MonoBehaviour
    {
        public List<TextMeshProUGUI> FeedbackTexts = new List<TextMeshProUGUI>();
        public List<StarController> StarControllers = new List<StarController>();
        public RemainingPassengersView RemainingPassengersView;
        public TextMeshProUGUI MoneyRewardText;
        
        private FeedbackSO m_feedbackSO;
        private PassengerOnBoardModel m_passengerOnBoardModel;
        private PlayerMoneyModel m_playerMoneyModel;
        
        private List<int> m_usedFeedback = new List<int>();
        
        [Inject]
        public void Construct(FeedbackSO feedbackSO, PassengerOnBoardModel passengerOnBoardModel,
            PlayerMoneyModel playerMoneyModel)
        {
            m_feedbackSO = feedbackSO;
            m_passengerOnBoardModel = passengerOnBoardModel;
            m_playerMoneyModel = playerMoneyModel;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            RemainingPassengersView.SetText(m_passengerOnBoardModel.PassengersCount, m_passengerOnBoardModel.MaxPassengersOnBoard);
            MoneyRewardText.text = m_playerMoneyModel.MoneyOnBoard.ToString();

            for (int i = 0; i < FeedbackTexts.Count; i++)
            {
                List<Feedback> unusedFeedback = m_feedbackSO.feedbacks.Where((feedback, index) => !m_usedFeedback.Contains(index)).ToList();
                if (unusedFeedback.Count == 0)
                {
                    m_usedFeedback.Clear();
                    unusedFeedback = m_feedbackSO.feedbacks;
                }
                
                Feedback feedback = unusedFeedback[Random.Range(0, unusedFeedback.Count)];
                m_usedFeedback.Add(m_feedbackSO.feedbacks.IndexOf(feedback));
            
                StartTextWrite(feedback.message, FeedbackTexts[i]).Forget();
                StarControllers[i].CreateStars(feedback.starCount).Forget();
            }
        }

        public async UniTask StartTextWrite(string text, TextMeshProUGUI feedbackText)
        {
            feedbackText.text = "";
            foreach (var c in text)
            {
                feedbackText.text += c;
                await UniTask.Delay(50);
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            StarControllers.ForEach(c => c.Clear());
        }
    }
}