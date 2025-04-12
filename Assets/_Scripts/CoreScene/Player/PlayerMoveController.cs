using System.Collections;
using System.Collections.Generic;
using _Scripts.CoreScene;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMoveController : MonoBehaviour
{
    public Transform boatModel;           // Модель лодки
    public float turnSpeed = 60f;         // Скорость поворота
    
    public float returnSpeed = 50f;  // Скорость возвращения в исходное положение
    public float maxSpeed = 10f;          // Максимальная скорость по Z
    public float maxTurnAngle = 60f;      // Максимальный угол поворота

    private float currentYaw = 0f;        // Угол поворота модели
    
    private bool isTurningLeft = false;
    private bool isTurningRight = false;

    private GameManager m_gameManager;

    private Transform m_pirsTarget;
    
    public float CurrentYaw => currentYaw;

    public HoronControls controls;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        m_gameManager = gameManager;
    }

    private void Awake()
    {
        controls = new HoronControls();  // Создаём экземпляр PlayerControls

        // Привязываем события
        controls.Movement.TurnLeft.started += ctx => StartTurn(-1);   // Поворот влево
        controls.Movement.TurnLeft.canceled += ctx => StopTurn();     // Остановка поворота влево

        controls.Movement.TurnRight.started += ctx => StartTurn(1);    // Поворот вправо
        controls.Movement.TurnRight.canceled += ctx => StopTurn();    // Остановка поворота вправо
    }

    public void GoToTarget(Transform pirsPosition)
    {
        m_pirsTarget = pirsPosition;
        m_gameManager.ShipState = ShipState.Mooring;
    }
    
    public void GoToTargetInstant(Transform pirsPosition)
    {
        transform.position = pirsPosition.position;
        m_pirsTarget = null;
    }


    private void OnEnable()
    {
        controls.Enable();  // Включаем обработку ввода
    }

    private void OnDisable()
    {
        controls.Disable(); // Отключаем обработку ввода
    }

    // Начало поворота
    private void StartTurn(int direction)
    {
        if (direction == -1) isTurningLeft = true;
        else if (direction == 1) isTurningRight = true;
    }

    // Остановка поворота
    private void StopTurn()
    {
        isTurningLeft = false;
        isTurningRight = false;
    }

    private void Update()
    {
        // Поворот лодки в зависимости от состояния
        if (isTurningLeft)
        {
            Turn(-1);
        }
        else if (isTurningRight)
        {
            Turn(1);
        }
        else
        {
            // Если не нажата ни одна кнопка, возвращаем лодку в стандартное положение
            ReturnToCenter();
        }
    }

    // Поворот лодки
    private void Turn(int direction)
    {
        float turnAmount = direction * turnSpeed * Time.deltaTime;  // Поворот с учетом времени
        currentYaw += turnAmount;  // Изменяем угол поворота
        currentYaw = Mathf.Clamp(currentYaw, -maxTurnAngle, maxTurnAngle);  // Ограничиваем угол
    }

    // Возвращение лодки в стандартное положение
    private void ReturnToCenter()
    {
        if (currentYaw != 0)
        {
            float returnAmount = returnSpeed * Time.deltaTime * Mathf.Sign(-currentYaw);  // Сколько нужно вернуть
            currentYaw += returnAmount;  // Двигаем лодку к центру
            if (Mathf.Abs(currentYaw) < 1f) // Если угол близок к нулю, останавливаем
            {
                currentYaw = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (m_gameManager.ShipState == ShipState.Mooring && m_pirsTarget != null)
        {
            transform.position = Vector3.Lerp(transform.position, m_pirsTarget.position, 0.5f * Time.deltaTime);
        }
        
        if (m_gameManager.ShipState == ShipState.Swimming)
        {
            RotateAndSwim();
        }
    }

    private void RotateAndSwim()
    {
        // Поворот лодки по оси Y
        Quaternion targetRotation = Quaternion.Euler(0f, currentYaw, 0f);
        boatModel.rotation = Quaternion.Slerp(boatModel.rotation, targetRotation, Time.deltaTime * 10f);
        
        float speedFactor = currentYaw / maxTurnAngle; // от -1 до 1
        float currentSpeed = -1 * maxSpeed * speedFactor;

        // Двигаемся по оси Z (вперёд или назад)
        transform.position += Vector3.forward * currentSpeed * Time.deltaTime;
    }
}
