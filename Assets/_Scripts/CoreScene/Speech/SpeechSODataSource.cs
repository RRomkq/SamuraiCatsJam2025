// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using System;
using System.Linq;
using _Scripts.CoreScene.Speech.Model;
using UnityEditor;
using Zenject;

namespace _Scripts.CoreScene.Speech
{
    public class SpeechSODataSource : IInitializable
    {
        private SpeechSO m_speechSo;

        public void Initialize()
        {
            m_speechSo = AssetDatabase.LoadAssetAtPath<SpeechSO>(SpeechSO.NAME);
        }

        public string GetRandomReplic(SpeechSituation actorSpeechSituation)
        {
            SpeechReplicsBySituation replicsBySituation =
                m_speechSo.SpeechReplics.FirstOrDefault(s => s.SpeechSituation == actorSpeechSituation);

            if (replicsBySituation == null)
            {
                return null;
            }
            
            int random = new Random().Next(0, replicsBySituation.SpeechReplics.Count);
            return replicsBySituation.SpeechReplics[random];
        }
    }
}