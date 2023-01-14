using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldMapPlayer : MonoBehaviour
{
    [SerializeField] private WorldMapPlayerAnimationController anim;
    [SerializeField] private bool canMove = true;
    [SerializeField] private WorldLevel currentStandingLevel;
    
    private InputMapping input;
    private Coroutine translationRoutine;
    
    private void Awake()
    {
        input = new InputMapping();
        input.Enable();

        input.WorldMap.Up.performed += TryGoUp;
        input.WorldMap.Down.performed += TryGoDown;
        input.WorldMap.Left.performed += TryGoLeft;
        input.WorldMap.Right.performed += TryGoRight;
    }

    private void OnDestroy()
    {
        input.WorldMap.Up.performed -= TryGoUp;
        input.WorldMap.Down.performed -= TryGoDown;
        input.WorldMap.Left.performed -= TryGoLeft;
        input.WorldMap.Right.performed -= TryGoRight;
        
        input.Disable();
        input.Dispose();
        input = null;
    }

    private void Start()
    {
        if (currentStandingLevel != null)
        {
            transform.position = currentStandingLevel.transform.position;
        }
        anim.SetIdle();
    }

    private void TryGoUp(InputAction.CallbackContext context)
    {
        if (!canMove) return;
        
        if (currentStandingLevel != null)
        {
            var newLevelToStand = currentStandingLevel.GetLevelInThatDirection(Direction.Up);
            if (newLevelToStand.Level != null)
            {
                MoveTo(newLevelToStand);
            }
        }
    }
    
    private void TryGoDown(InputAction.CallbackContext context)
    {
        if (!canMove) return;
        
        if (currentStandingLevel != null)
        {
            var newLevelToStand = currentStandingLevel.GetLevelInThatDirection(Direction.Down);
            if (newLevelToStand.Level != null)
            {
                MoveTo(newLevelToStand);
            }
        }
    }
    
    private void TryGoLeft(InputAction.CallbackContext context)
    {
        if (!canMove) return;
        
        if (currentStandingLevel != null)
        {
            var newLevelToStand = currentStandingLevel.GetLevelInThatDirection(Direction.Left);
            if (newLevelToStand.Level != null)
            {
                MoveTo(newLevelToStand);
            }
        }
    }
    
    private void TryGoRight(InputAction.CallbackContext context)
    {
        if (!canMove) return;
        
        if (currentStandingLevel != null)
        {
            var newLevelToStand = currentStandingLevel.GetLevelInThatDirection(Direction.Right);
            if (newLevelToStand.Level != null)
            {
                MoveTo(newLevelToStand);
            }
        }
    }

    private void MoveTo(LevelAndPath newStandingPosition)
    {
        if(translationRoutine != null)
            StopCoroutine(translationRoutine);
        translationRoutine = StartCoroutine(Translation(newStandingPosition));
    }

    private IEnumerator Translation(LevelAndPath newStandingPosition)
    {
        anim.SetRunning();
        canMove = false;
        currentStandingLevel = null;
        var progress = 0f;
        var initialPos = transform.position;

        var line = newStandingPosition.Path.GetLineRendererPoints;
        if (newStandingPosition.isReverse)
        {
            for (var i = 0; i < line.positionCount; i++)
            {
                var finalPos = line.GetPosition(i);
                transform.forward = finalPos - transform.position;
                transform.position = finalPos;
                yield return null;
            }
        }
        else
        {
            for (var i = line.positionCount - 1; i >= 0; i--)
            {
                var finalPos = line.GetPosition(i);
                transform.forward = finalPos - transform.position;
                transform.position = finalPos;
                yield return null;
            }
        }

        transform.forward = Vector3.back;
        transform.position = newStandingPosition.Level.transform.position;
        
        currentStandingLevel = newStandingPosition.Level;
        canMove = true;
        anim.SetIdle();
    }
}
