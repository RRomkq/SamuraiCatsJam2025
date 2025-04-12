using UnityEngine;

namespace _Scripts.CoreScene
{
    public static class MoveElementsUtils
    {
        public static void GoToPositionLerp(this GameObject moveObject, Transform target, float speed)
        {
            moveObject.transform.position = Vector3.Lerp(moveObject.transform.position, target.position, speed * Time.deltaTime);
        }
    }
}