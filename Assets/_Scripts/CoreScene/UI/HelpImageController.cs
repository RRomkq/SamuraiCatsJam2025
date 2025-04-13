// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.CoreScene
{
    public class HelpImageController : MonoBehaviour
    {
        [SerializeField] private Image m_helpImage;

        private void Awake()
        {
            Hide();
        }

        public void Hide()
        {
            m_helpImage.gameObject.SetActive(false);
        }

        public void Show()
        {
            m_helpImage.gameObject.SetActive(true);
        }
    }
}