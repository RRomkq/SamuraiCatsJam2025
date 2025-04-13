// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using _Scripts.CoreScene.Speech.Model;
using UnityEngine;

namespace _Scripts.CoreScene.Speech.UI
{
    public class SpeechBubbleManager : MonoBehaviour
    {
        [SerializeField] private SpeechBubbleController m_ghostBubbleSample;
        [SerializeField] private SpeechBubbleController m_haronBubbleSample;
        
        public void ShowSpeechBubble(SpeechActor actor, string replic)
        {
            Debug.LogWarning(replic);

            SpeechBubbleController bubble = GetBubbleByActor(actor);
            if (bubble == null)
            {
                return;
            }
            bubble.SetText(replic);
            bubble.Show(actor);
        }

        private SpeechBubbleController GetBubbleByActor(SpeechActor actor)
        {
            switch (actor.SpeechActorType)
            {
                case SpeechActorType.HARON: return m_haronBubbleSample;
                case SpeechActorType.GHOST: return m_ghostBubbleSample;
            }

            return null;
        }
    }
}