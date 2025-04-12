using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.CoreScene;
using _Scripts.CoreScene.Enviroment;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMoveController : MonoBehaviour
{
    private LineHandler m_lineHandler;
    
    public HoronControls controls;
    public float ForceToLineDuration = 1;

    private int m_currentLine = 1;

    [Inject]
    public void Construct(LineHandler lineHandler)
    {
        m_lineHandler = lineHandler;
    }

    private void Awake()
    {
        controls = new HoronControls();  // Создаём экземпляр PlayerControls

        controls.Movement.TurnLeft.performed += ctx => GoToLine(-1);
        controls.Movement.TurnRight.performed += ctx => GoToLine(1);
    }

    private void GoToLine(int i)
    {
        int index = m_currentLine + i;

        if (index < 0 || index >= m_lineHandler.HaronLines.Count)
        {
            return;
        }
        
        MoveTo(m_lineHandler.HaronLines[index]);
        m_currentLine = index;
    }

    public void GoToFirstLine()
    {
        MoveTo(m_lineHandler.HaronLines.First());
    }
    
    public void GoToLastLine()
    {
        MoveTo(m_lineHandler.HaronLines.Last());
    }

    private void MoveTo(Transform target)
    {
        transform.DOMove(target.position, ForceToLineDuration).SetEase(Ease.InOutQuad);
    }

    public void GoToTargetInstant(Transform target)
    {
        transform.position = target.position;
    }

    private void OnEnable()
    {
        controls.Enable();  // Включаем обработку ввода
    }

    private void OnDisable()
    {
        controls.Disable(); // Отключаем обработку ввода
    }

  
}
