using UnityEngine;

namespace _Scripts.CoreScene.Enviroment
{
    public class LetDestroyer: MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // Проверяем, есть ли на объекте компонент LetMover
            LetMover letMoverComponent = other.GetComponent<LetMover>();

            // Если компонент найден, уничтожаем объект
            if (letMoverComponent != null)
            {
                Destroy(other.gameObject);
            }
        }
    }
}