using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene
{
    public class StarController: MonoBehaviour
    {
        public List<RectTransform> StarList;
        public RectTransform StarSpawnPosition;
        public GameObject starPrefab;
        
        private IInstantiator m_instantiator;
        
        [Inject]
        public void Construct(IInstantiator instantiator)
        {
            m_instantiator = instantiator;
        }

        public async UniTask CreateStars(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject star = m_instantiator.InstantiatePrefab(starPrefab, StarSpawnPosition.position, Quaternion.identity, transform);
                Sequence sequence = DOTween.Sequence();
                sequence.Append(star.GetComponent<RectTransform>().DOAnchorPos(StarList[i].anchoredPosition, 1f));
                sequence.Append(star.transform.DOShakeScale(0.5f, 1f, 0));
                sequence.Play();
                await UniTask.Delay(500);
            }
        }
    }
}