// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.CoreScene.Speech.UI
{
    public class TestBubbleController : MonoBehaviour
    {
        [SerializeField] private Button m_testGhostBubbleButton;
        [SerializeField] private Button m_testHaronBubbleButton;

        private SpeechManager m_bubbleManager;
        
        [Inject]
        public void Construct(SpeechManager bubbleManager)
        {
            m_bubbleManager = bubbleManager;
        }
        
        private void OnEnable()
        {
            m_testGhostBubbleButton.onClick.AddListener(OnGhostButtonTest);
            m_testHaronBubbleButton.onClick.AddListener(OnHaronButtonTest);
        }

        private void OnDisable()
        {
            m_testGhostBubbleButton.onClick.RemoveListener(OnGhostButtonTest);
            m_testHaronBubbleButton.onClick.RemoveListener(OnHaronButtonTest);
        }
        
        private void OnHaronButtonTest()
        {
            m_bubbleManager.ShowRandomHaronReplic();
        }

        private void OnGhostButtonTest()
        {
            m_bubbleManager.ShowRandomGhostReplic();
        }
    }
}