using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExEventSubscriber : MonoBehaviour
{
    public ExEventChannel eventChannel;

    void OnEventRaised()
    {
        Debug.Log(gameObject.name + " 에서 이벤트 발생");
    }
    private void OnEnable()
    {
        eventChannel.OnEventRaised.AddListener(OnEventRaised);
    }
    private void OnDisable()
    {
        eventChannel.OnEventRaised.RemoveListener(OnEventRaised);
    }
}
