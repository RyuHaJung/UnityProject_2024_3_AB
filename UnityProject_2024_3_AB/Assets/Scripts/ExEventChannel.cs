using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New EventChannel", menuName = "Events/Event Channel", order = 100)]
public class ExEventChannel : ScriptableObject
{
    public UnityEvent OnEventRaised;

    public void RaiseEvent()
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke();
    }
}
