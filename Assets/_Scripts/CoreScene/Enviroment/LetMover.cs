using UnityEngine;
using Zenject;

public class LetMover : MonoBehaviour
{
    public float moveSpeed = 2f; // Будет задаваться спавнером
    public float MaxSlowdownFactor = 0.3f;
    
    private PlayerMoveController m_playerMoveController;

    [Inject]
    public void Construct(PlayerMoveController playerMoveController)
    {
        m_playerMoveController = playerMoveController;
    }

    void Update()
    {
        float slowdownFactor = (Mathf.Abs(m_playerMoveController.CurrentYaw) / m_playerMoveController.maxTurnAngle) * MaxSlowdownFactor;
        
        transform.position += Vector3.left * ((1 - slowdownFactor) * moveSpeed * Time.deltaTime);
    }
}
