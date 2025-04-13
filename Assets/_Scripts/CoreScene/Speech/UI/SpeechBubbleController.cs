// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using System;
using _Scripts.CoreScene.Speech.Model;
using TMPro;
using UnityEngine;

namespace _Scripts.CoreScene.Speech.UI
{
    public class SpeechBubbleController : MonoBehaviour
    {
        private const int SHOW_TIME_IN_MS = 3000;
        
        [SerializeField] private TMP_Text m_text;
        
        private DateTime m_showTime;

        private SpeechActor m_currentActor;
        
        private RectTransform m_rectTransform;

        private void Awake()
        {
            m_rectTransform = GetComponent<RectTransform>();
        }

        public void SetText(string replic)
        {
            m_text.text = replic;
        }

        public void Show(SpeechActor actor)
        {
            m_currentActor = actor;
            
            this.gameObject.SetActive(true);

            StartTimer();
        }

        private void StartTimer()
        {
            m_showTime = DateTime.Now;
        }

        private void Update()
        {
            if (!this.gameObject.activeSelf)
            {
                return;
            }

            if (m_currentActor == null)
            {
                Hide();
                
                return;
            }
            
            UpdatePositionNearActor();

            if (!ShowTimeExpired())
            {
                return;
            }
            
            Hide();
        }

        private void UpdatePositionNearActor()
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(m_currentActor.BubbleAnchor.transform.position);
            m_rectTransform.position = pos;
        }

        private bool ShowTimeExpired()
        {
            TimeSpan diff = DateTime.Now - m_showTime;
            return diff.TotalMilliseconds > SHOW_TIME_IN_MS;
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}