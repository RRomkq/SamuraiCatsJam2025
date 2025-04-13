// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using System.Collections.Generic;
using _Scripts.CoreScene.Speech;
using _Scripts.CoreScene.Speech.Model;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene.Enviroment
{
    public class StartLevelGhostsRepository : MonoBehaviour
    {
        [SerializeField] private GameObject m_ghostContainerSample;
        
        private StartPirsGhostsPositionHelper m_positionHelper;
        private SpeechActorsManager m_actorsManager;

        private List<GameObject> m_spawnedGhosts = new List<GameObject>();
        
        [Inject]
        public void Construct(
            StartPirsGhostsPositionHelper mPositionHelper,
            SpeechActorsManager actorsManager
        )
        {
            m_positionHelper = mPositionHelper;
            m_actorsManager = actorsManager;
        }

        public void DisposeGhosts()
        {
            foreach (var mSpawnedGhost in m_spawnedGhosts)
            {
                DestroyImmediate(mSpawnedGhost);
            }
            m_spawnedGhosts.Clear();
            m_actorsManager.Clear();
            m_positionHelper.Reset();
        }
        
        public void SpawnGhosts(int count)
        {
            m_positionHelper.Init();
            
            for (int i = 0; i < count; i++)
            {
                GameObject ghost = CreateGhost();
                var speechActor = ghost.GetComponent<SpeechActor>();
                m_actorsManager.RegisterSpeechActor(speechActor);
                speechActor.SpeechSituation = SpeechSituation.QUEUE;
                m_positionHelper.Spawn(ghost);
                
                m_spawnedGhosts.Add(ghost);
            }
        }

        private GameObject CreateGhost()
        {
            GameObject ghost = Instantiate(m_ghostContainerSample);
            ghost.transform.localScale = Vector3.one;
            return ghost;
        }
    }
}