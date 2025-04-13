// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using UnityEngine;

namespace _Scripts.CoreScene.Speech.Model
{
    public class SpeechActor : MonoBehaviour
    {
        [SerializeField] 
        private GameObject m_bubbleAnchor;

        [SerializeField] 
        private SpeechActorType m_speechActorType;

        public GameObject BubbleAnchor => m_bubbleAnchor;

        public SpeechActorType SpeechActorType => m_speechActorType;
        
        public SpeechSituation SpeechSituation { get; set; } = SpeechSituation.QUEUE;
    }
}