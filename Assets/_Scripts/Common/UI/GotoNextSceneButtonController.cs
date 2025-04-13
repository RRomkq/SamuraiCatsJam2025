// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.StartScene
{
    public class GotoNextSceneButtonController : MonoBehaviour
    {
        [SerializeField] private Button m_gotoNextSceneButton;
        
        private GameSceneManager m_gameSceneManager;

        [Inject]
        public void Construct(GameSceneManager gameSceneManager)
        {
            m_gameSceneManager = gameSceneManager;
        }

        private void Awake()
        {
            m_gotoNextSceneButton.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            m_gotoNextSceneButton.onClick.AddListener(OnStartButtonClick);
        }

        private void OnDisable()
        {
            m_gotoNextSceneButton.onClick.RemoveListener(OnStartButtonClick);
        }

        private void OnStartButtonClick()
        {
            GotoNextScene();
        }

        public void ShowGotoNextSceneButton()
        {
            m_gotoNextSceneButton.gameObject.SetActive(true);
        }

        public void GotoNextScene()
        {
            m_gameSceneManager.GotoNextScene();
        }
    }
}