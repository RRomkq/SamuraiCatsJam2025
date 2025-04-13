using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.CoreScene;
using _Scripts.CoreScene.Enviroment;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Random = UnityEngine.Random;

public class PlayerMoveController : MonoBehaviour
{
    private LineHandler m_lineHandler;
    public List<GameObject> Boards;
    
    public HoronControls controls;
    public List<AudioSource> m_audioSources;

    private AudioSource m_audioS;
    public float ForceToLineDuration => m_levelModel.HoronSpeed;

    private int m_currentLine = 1;
    private LevelModel m_levelModel;
    private FinishPirsController m_finishPirsController;
    private GameModel m_gameModel;

    private Tween m_lastTween;

    public event Action Click;

    [Inject]
    public void Construct(LineHandler lineHandler, LevelModel levelModel, FinishPirsController finishPirsController, GameModel gameModel)
    {
        m_lineHandler = lineHandler;
        m_levelModel = levelModel;
        m_finishPirsController = finishPirsController;
        m_gameModel = gameModel;
    }

    private void Awake()
    {
        controls = new HoronControls();  // Создаём экземпляр PlayerControls

        controls.Movement.TurnLeft.performed += ctx => GoToLine(-1);
        controls.Movement.TurnRight.performed += ctx => GoToLine(1);
        controls.Movement.Click.performed += ctx => Click?.Invoke();
    }

    private void GoToLine(int i)
    {
        int index = m_currentLine + i;

        if (index < 0 || index >= m_lineHandler.HaronLines.Count - 1 || m_gameModel.ShipState != ShipState.Swimming)
        {
            return;
        }

        if (m_audioS == null)
        {
            if (m_audioS.isPlaying)
            {
                m_audioS.Stop();
            }
            m_audioS = m_audioSources[Random.Range(0, m_audioSources.Count)];
            m_audioS.Play();
        }
        
        MoveTo(m_lineHandler.HaronLines[index]);
        m_currentLine = index;
    }

    public void GoToFirstLine(Action onComplete)
    {
        if (m_lastTween != null)
        {
            m_lastTween.Kill();
        }
        
        m_lastTween = transform.DOMove(m_lineHandler.HaronLines.First().position, 3)
            .SetEase(Ease.Linear)
            .OnComplete(onComplete.Invoke);
        m_currentLine = 0;
    }

    public void GoToCenterLine()
    {
        MoveTo(m_lineHandler.HaronLines[2], ForceToLineDuration * 2);
        m_currentLine = 2;
    }
    
    public void GoToLastLine(Action action)
    {
        MoveTo(m_lineHandler.HaronLines.Last(), ForceToLineDuration*(m_lineHandler.HaronLines.Count - m_currentLine));
        m_finishPirsController.GoPirsToVisionPosition(action, ForceToLineDuration*(m_lineHandler.HaronLines.Count - m_currentLine));
        m_currentLine = m_lineHandler.HaronLines.Count - 1;
    }
    
    public Tween MoveTo(Transform target)
    {
        return MoveTo(target, ForceToLineDuration);
    }

    public Tween MoveTo(Transform target, float duration)
    {
        if (m_lastTween != null)
        {
            m_lastTween.Kill();
        }
        
        m_lastTween = transform.DOMove(target.position, duration).SetEase(Ease.Linear);

        return m_lastTween;
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

    public void SetBoard(int id)
    {
        if (Boards[id].activeSelf)
        {
            return;
        }
        
        Boards.ForEach(b => b.SetActive(false));
        Boards[id].SetActive(true);
    }

  
}
