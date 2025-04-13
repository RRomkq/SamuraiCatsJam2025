// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using System;
using System.Timers;
using _Scripts.CoreScene.Speech.Model;
using _Scripts.CoreScene.Speech.UI;
using Zenject;

namespace _Scripts.CoreScene.Speech
{
    public class SpeechManager : IInitializable
    {
        private const int MIN_INTERVAL = 3;
        private const int MAX_INTERVAL = 10;
        
        private SpeechSODataSource m_speechSODataSource;
        private SpeechActorsManager m_actorsManager;
        private SpeechBubbleManager m_speechBubbleManager;

        private Timer m_timer;
        
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

        private void StartTimer()
        {
            double randomInterval = new Random().Next(MIN_INTERVAL, MAX_INTERVAL);
            m_timer = new Timer(randomInterval);
            m_timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            ShowRandomGhostReplic();
        }

        public void ShowRandomGhostReplic()
        {
            SpeechActor actor = m_actorsManager.GetRandomActor();
            string replic = m_speechSODataSource.GetRandomGhostReplic(actor.SpeechSituation);
            if (replic == null)
            {
                return;
            }

            m_speechBubbleManager.ShowSpeechBubble(actor, replic);
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