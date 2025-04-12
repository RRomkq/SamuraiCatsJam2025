// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using System.Collections.Generic;
using _Scripts.CoreScene.Speech;
using _Scripts.CoreScene.Speech.Model;
using _Scripts.CoreScene.Speech.UI;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene
{
    public class CoreSceneMetaInstaller : MonoInstaller
    {
        [SerializeField] private List<SpeechActor> m_actors;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SpeechManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpeechSODataSource>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpeechActorsManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpeechBubbleManager>().AsSingle();
        }
    }
}