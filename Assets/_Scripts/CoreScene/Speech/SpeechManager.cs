// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using System;
using _Scripts.CoreScene.Speech.Model;
using _Scripts.CoreScene.Speech.UI;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Scripts.CoreScene.Speech
{
    public class SpeechManager : IInitializable
    {
        private const int MIN_INTERVAL = 5000;
        private const int MAX_INTERVAL = 15000;
        
        private SpeechSODataSource m_speechSODataSource;
        private SpeechActorsManager m_actorsManager;
        private SpeechBubbleManager m_speechBubbleManager;

        public SpeechManager(SpeechSODataSource mSpeechSoDataSource, 
            SpeechBubbleManager speechBubbleManager,
            SpeechActorsManager actorsManager)
        {
            m_speechSODataSource = mSpeechSoDataSource;
            m_actorsManager = actorsManager;
            m_speechBubbleManager = speechBubbleManager;
        }

        public void Initialize()
        {
            StartTimer();
        }

        private async void StartTimer()
        {
            int randomInterval = new Random().Next(MIN_INTERVAL, MAX_INTERVAL);
            await UniTask.Delay(randomInterval);

            if (new Random().Next(0, 10) > 8)
            {
                ShowRandomHaronReplic();
            }
            else
            {
                ShowRandomGhostReplic();
            }
        }

        public void ShowRandomGhostReplic()
        {
            SpeechActor actor = m_actorsManager.GetRandomActor();
            if (actor == null)
            {
                StartTimer();
                
                return;
            }
            
            string replic = m_speechSODataSource.GetRandomGhostReplic(actor.SpeechSituation);
            if (replic == null)
            {
                StartTimer();
                
                return;
            }

            m_speechBubbleManager.ShowSpeechBubble(actor, replic);
            
            StartTimer();
        }

        public void ShowRandomHaronReplic()
        {
            SpeechActor actor = m_actorsManager.GetHaronActor();
            string replic = m_speechSODataSource.GetRandomHaronReplic(actor.SpeechSituation);
            if (replic == null)
            {
                return;
            }

            m_speechBubbleManager.ShowSpeechBubble(actor, replic);
        }
    }
}