// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.CoreScene.Speech.Model
{
    [CreateAssetMenu(fileName = "SpeechSO", menuName = NAME)]
    public class SpeechSO : ScriptableObject
    {
        public const string NAME = "SpeechSO";

        [SerializeField]
        private List<SpeechReplicsBySituation> m_speechReplics;

        [SerializeField]
        private List<SpeechReplicsBySituation> m_haronSpeechReplics;
        
        public List<SpeechReplicsBySituation> SpeechReplics => m_speechReplics;
        
        public List<SpeechReplicsBySituation> HaronSpeechReplics => m_haronSpeechReplics;
    }

    [Serializable]
    public class SpeechReplicsBySituation
    {
        [SerializeField]
        private SpeechSituation m_speechSituation;
        
        [SerializeField]
        private List<string> m_speechReplics;

        public SpeechSituation SpeechSituation => m_speechSituation;

        public List<string> SpeechReplics => m_speechReplics;
    }
}