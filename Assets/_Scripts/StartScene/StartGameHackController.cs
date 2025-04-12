// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using UnityEngine;
using Zenject;

namespace _Scripts.StartScene
{
    public class StartGameHackController : MonoBehaviour
    {
        private GameSceneManager m_gameSceneManager;

        [Inject]
        public void Construct(GameSceneManager gameSceneManager)
        {
            m_gameSceneManager = gameSceneManager;
        }
        
#if IN_DEVELOPMENT
        private void Update()
        {
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                return;
            }
            
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                return;
            }
            
            if (!Input.GetKey(KeyCode.LeftAlt))
            {
                return;
            }
            
            if (!Input.GetKeyDown(KeyCode.G))
            {
                return;
            }
            
            m_gameSceneManager.GotoNextScene();
        }
#endif
    }
}