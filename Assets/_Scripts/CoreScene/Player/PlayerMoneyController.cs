using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.CoreScene.Player
{
    public class PlayerMoneyController: MonoBehaviour
    {
        public Transform GetMoneyPosition;
        public GameObject GetMoneyPrefab;
        public GameObject SplashEffectPrefab;
        public Transform DropMoneyPosition;
        
        public RectTransform PlayerMoneyPosition;
        public RectTransform GetMoneyRewardPosition;
        public GameObject ImageMoneyPrefab;
        
        private IInstantiator m_instantiator;
        private PlayerMoneyModel m_playerMoneyModel;
        
        [Inject]
        public void Construct(IInstantiator instantiator, PlayerMoneyModel playerMoneyModel)
        {
            m_instantiator = instantiator;
            m_playerMoneyModel = playerMoneyModel;
        }
        
        public async UniTask AddMoneyOnBoard(int amount)
        {
            m_playerMoneyModel.AddMoneyOnBoard(amount);
            for (int i = 0; i < amount; i++)
            {
                GameObject gameObject = m_instantiator.InstantiatePrefab(GetMoneyPrefab, GetMoneyPosition.position, Quaternion.identity, transform);
                SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                gameObject.transform.DOMove(DropMoneyPosition.position, 1f);
                spriteRenderer.DOFade(0, 1f);
                gameObject.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f).OnComplete(() =>
                {
                    Destroy(gameObject);
                });
                
                await UniTask.Delay(100);
            }
        }

        public async UniTask DropMoneyFromBoard(int amount)
        {
            if (m_playerMoneyModel.MoneyOnBoard == 0)
            {
                return;
            }
            
            m_playerMoneyModel.SubMoneyOnBoard(amount);
            
            for (int i = 0; i < amount; i++)
            {
                LaunchMoney();
                await UniTask.Delay(100);
            }
        }
        
        void LaunchMoney()
        {
            Vector3 start = transform.position;

            // Случайное направление (вокруг круга)
            float angle = Random.Range(0f, Mathf.PI * 2f);
            Vector3 end = start + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * 5;

            // Точка дуги (в центре между start и end, поднятая вверх)
            Vector3 control = (start + end) / 2f + Vector3.up * 3;

            // Кривая для движения
            Vector3[] path = new Vector3[] { start, control, end };

            GameObject projectile = m_instantiator.InstantiatePrefab(GetMoneyPrefab, start, Quaternion.identity, transform);
            projectile.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            
            // Отключим физику, чтобы DOTween управлял
            var rb = projectile.GetComponent<Rigidbody2D>();
            if (rb) rb.isKinematic = true;

            projectile.transform.DOPath(path, 1, PathType.CatmullRom, PathMode.TopDown2D)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    GameObject splash = Instantiate(SplashEffectPrefab, end, Quaternion.Euler(0, 180, 0));
                    Destroy(projectile);
                    Destroy(splash, 2f);
                });
        }

        public async UniTask GetMoneyFromBoard()
        {
            for (int i = 0; i < m_playerMoneyModel.MoneyOnBoard; i++)
            {
                GameObject gameObject = m_instantiator.InstantiatePrefab(ImageMoneyPrefab, GetMoneyRewardPosition.position, Quaternion.identity, GetMoneyRewardPosition);
                Image spriteRenderer = gameObject.GetComponent<Image>();
                RectTransform rect = gameObject.GetComponent<RectTransform>();
                gameObject.transform.DOMove(PlayerMoneyPosition.position, 0.5f);
                spriteRenderer.DOFade(0.5f, 0.5f).OnComplete(() =>
                {
                    Destroy(gameObject);
                    m_playerMoneyModel.AddMoney(1);
                });
                
                await UniTask.Delay(100);
            }
            
            m_playerMoneyModel.SubMoneyOnBoard(m_playerMoneyModel.MoneyOnBoard);
        }
    }
}