// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using System;
using TMPro;
using UnityEngine;

namespace _Scripts.CoreScene.Speech.UI
{
    public class SpeechBubbleController : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_text;
        
        private DateTime m_showTime;

        public void SetText(string replic)
        {
            m_text.text = replic;
        }

        public void Show()
        {
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

            if (!ShowTimeExpired())
            {
                return;
            }
            
            Hide();
        }

        private bool ShowTimeExpired()
        {
            TimeSpan diff = DateTime.Now - m_showTime;
            return diff.TotalMilliseconds > SHOW_TIME_IN_MS;
        }

        private const int SHOW_TIME_IN_MS = 3000;

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}