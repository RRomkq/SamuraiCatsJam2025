// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Scripts
{
    public class GameSceneManager: IInitializable
    {
        private string m_currentScene = SceneNames.START_SCENE;

        private List<string> m_scenesLoop = new List<string>()
        {
            SceneNames.START_SCENE,
            SceneNames.INTRO_SCENE,
            SceneNames.CORE_SCENE,
            SceneNames.OUTRO_SCENE
        };
        
        public void GotoNextScene()
        {
            int currentIndex = m_scenesLoop.IndexOf(m_currentScene);
            if (currentIndex == m_scenesLoop.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }

            m_currentScene = m_scenesLoop[currentIndex];
            
            SceneManager.LoadScene(m_currentScene);
        }

        public void Initialize()
        {
            m_currentScene = SceneManager.GetActiveScene().name;
        }
    }
}