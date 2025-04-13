// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using _Scripts.CoreScene.Speech;
using _Scripts.CoreScene.Speech.Model;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene.Enviroment
{
    public class StartLevelGhostSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject m_ghostContainerSample;
        
        private StartPirsGhostsPositionHelper m_positionHelper;
        private SpeechActorsManager m_actorsManager;

        [Inject]
        public void Construct(
            StartPirsGhostsPositionHelper mPositionHelper,
            SpeechActorsManager actorsManager
        )
        {
            m_positionHelper = mPositionHelper;
            m_actorsManager = actorsManager;
        }

        public void SpawnGhosts(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject ghost = CreateGhost();
                m_actorsManager.RegisterSpeechActor(ghost.GetComponent<SpeechActor>());
                m_positionHelper.Spawn(ghost);
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