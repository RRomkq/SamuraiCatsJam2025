using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene
{
    public class StarController: MonoBehaviour
    {
        public List<RectTransform> StarList;
        public GameObject starPrefab;
        
        private IInstantiator m_instantiator;
        
        private List<GameObject> m_stars = new List<GameObject>();
        
        [Inject]
        public void Construct(IInstantiator instantiator)
        {
            m_instantiator = instantiator;
        }

        public async UniTask CreateStars(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject star = m_instantiator.InstantiatePrefab(starPrefab, StarList[i].position, Quaternion.identity, transform);
                m_stars.Add(star);
                star.SetActive(true);
                Sequence sequence = DOTween.Sequence();
                sequence.Append(star.transform.DOShakeScale(0.5f, 1f, 0));
                sequence.Play();
                await UniTask.Delay(500);
            }
        }

        public void Clear()
        {
            foreach (var star in m_stars.ToList())
            {
                star.SetActive(false);
                Destroy(star);
            }
            m_stars.Clear();
        }
    }
}