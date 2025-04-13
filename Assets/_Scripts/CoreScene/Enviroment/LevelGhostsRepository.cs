// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using _Scripts.CoreScene.Speech;
using _Scripts.CoreScene.Speech.Model;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene.Enviroment
{
    public class LevelGhostsRepository : MonoBehaviour
    {
        [SerializeField] private GameObject m_ghostContainerSample;
        
        private StartPirsGhostsPositionHelper m_startPositionHelper;
        private SpeechActorsManager m_actorsManager;

        private List<GameObject> m_spawnedGhosts = new List<GameObject>();

        public List<GameObject> SpawnedGhosts => m_spawnedGhosts;

        [Inject]
        public void Construct(
            StartPirsGhostsPositionHelper mPositionHelper,
            SpeechActorsManager actorsManager
        )
        {
            m_startPositionHelper = mPositionHelper;
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
            m_startPositionHelper.Reset();
        }
        
        public void SpawnGhosts(int count)
        {
            m_startPositionHelper.Init();
            
            for (int i = 0; i < count; i++)
            {
                GameObject ghost = CreateGhost();
                var speechActor = ghost.GetComponent<SpeechActor>();
                m_actorsManager.RegisterSpeechActor(speechActor);
                speechActor.SpeechSituation = SpeechSituation.QUEUE;
                m_startPositionHelper.Spawn(ghost);
                
                m_spawnedGhosts.Add(ghost);
            }
        }

        private GameObject CreateGhost()
        {
            GameObject ghost = Instantiate(m_ghostContainerSample);
            ghost.transform.localScale = Vector3.one;
            return ghost;
        }

        public async void KillGhost()
        {
            if (m_spawnedGhosts.Count == 0)
            {
                return;
            }
            
            int lastIndex = m_spawnedGhosts.Count - 1;
            GameObject ghost = m_spawnedGhosts[lastIndex];
            m_spawnedGhosts.RemoveAt(lastIndex);
            
            m_actorsManager.UnregisterSpeechActor(ghost.GetComponent<SpeechActor>());
            
            Sequence blinkSequence = DOTween.Sequence();

            List<SpriteRenderer> spriteRenderers = ghost.GetComponentsInChildren<SpriteRenderer>().ToList();
            
            for (int i = 0; i < 3; i++)
            {
                foreach (var spriteRenderer in spriteRenderers)
                {
                    // Исчезновение (цвет становится прозрачным)
                    blinkSequence.Append(spriteRenderer.DOFade(0f, 0.25f));
                    // Появление (возвращаем альфу в 1)
                    blinkSequence.Append(spriteRenderer.DOFade(1f, 0.25f));
                }
            }

            await UniTask.Delay(1500);
            
            DestroyImmediate(ghost);
        }
    }
}