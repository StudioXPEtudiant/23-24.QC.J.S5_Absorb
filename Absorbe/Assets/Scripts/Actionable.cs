using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Actionable : MonoBehaviour
{
    [SerializeField] private UnityEvent onStartLookingAt;
    [SerializeField] private UnityEvent onStopLookingAt;

    [SerializeField] private UnityEvent onMainAction;
    [SerializeField] private UnityEvent onSecondaryAction;
    
    public void StartLookingAt()
    {
        onStartLookingAt.Invoke();
    }

    public void StopLookingAt()
    {
        onStopLookingAt.Invoke();
    }

    public void ActionateMainAction()
    {
        onMainAction.Invoke();
    }

    public void ActionateSecondaryAction()
    {
        onSecondaryAction.Invoke();
    }
}
