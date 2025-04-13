// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.StartScene
{
    public class StartGameController : MonoBehaviour
    {
        [SerializeField] private Button m_startButton;
        [SerializeField] private GameObject m_releaseDateContainer;

        private void Awake()
        {
            ChangeState();
        }

        private async void ChangeState()
        {
            await UniTask.Delay(1000);
            
#if IN_DEVELOPMENT
            m_releaseDateContainer.SetActive(true);
            m_startButton.gameObject.SetActive(false);
#else
            m_releaseDateContainer.SetActive(false);
            m_startButton.gameObject.SetActive(true);
#endif
        }
    }
}