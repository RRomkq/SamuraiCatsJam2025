// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene
{
    public class CameraController
    {
        private const float CAMERA_MOVE_TO_START_DURATION = 2f;
        private const float CAMERA_MOVE_TO_SWIM_DURATION = 2f;
        private const float CAMERA_MOVE_TO_END_DURATION = 2f;

        private Camera Camera => Camera.main;
        
        public void ResetCamera()
        {
            Camera.transform.position = new Vector3(0, 0);
            Camera.orthographicSize = 20;
        }
        
        public void MoveToStartPoint()
        {
            Vector2 cameraEndPos = new Vector2(-12, 12);
            
            Sequence parallelAnimations = DOTween.Sequence();
            
            parallelAnimations.Join(Camera.transform.DOMove(cameraEndPos, CAMERA_MOVE_TO_START_DURATION));
            parallelAnimations.Join(Camera.DOOrthoSize(10, CAMERA_MOVE_TO_START_DURATION));
            parallelAnimations.SetEase(Ease.InOutSine);
        }

        public void MoveToSwimMode()
        {
            Vector2 cameraEndPos = new Vector2(0, 0);
            
            Sequence parallelAnimations = DOTween.Sequence();
            
            parallelAnimations.Join(Camera.transform.DOMove(cameraEndPos, CAMERA_MOVE_TO_SWIM_DURATION));
            parallelAnimations.Join(Camera.DOOrthoSize(20, CAMERA_MOVE_TO_SWIM_DURATION));
            parallelAnimations.SetEase(Ease.InOutSine);
        }

        public void MoveToFinishPoint()
        {
            Vector2 cameraEndPos = new Vector2(35, 12);
            
            Sequence parallelAnimations = DOTween.Sequence();
            
            parallelAnimations.Join(Camera.transform.DOMove(cameraEndPos, CAMERA_MOVE_TO_END_DURATION));
            parallelAnimations.Join(Camera.DOOrthoSize(10, CAMERA_MOVE_TO_END_DURATION));
            parallelAnimations.SetEase(Ease.InOutSine);
        }
    }
}