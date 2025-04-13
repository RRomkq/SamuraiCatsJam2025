// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using _Scripts.CoreScene.Speech.Model;

namespace _Scripts.CoreScene.Speech
{
    public class SpeechActorsManager
    {
        private List<SpeechActor> m_actors = new List<SpeechActor>();
        private SpeechActor m_haronActor; 

        public SpeechActorsManager(SpeechActor haronActor)
        {
            m_haronActor = haronActor;
        }

        public void RegisterSpeechActor(SpeechActor actor)
        {
            m_actors.Add(actor);
        }

        public SpeechActor GetRandomActor()
        {
            if (m_actors.Count == 0)
            {
                return null;
            }
            
            int index = new Random().Next(0, m_actors.Count);
            return m_actors[index];
        }

        public void Clear()
        {
            m_actors = new List<SpeechActor>();
        }

        public SpeechActor GetHaronActor()
        {
            return m_haronActor;
        }
    }
}