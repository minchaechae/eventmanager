using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum EventType
{
    
}

public static class EventManager
{
    private static Dictionary<EventType, List<Delegate>> eventTable = new Dictionary<EventType, List<Delegate>>();

    public static void Subscribe(EventType eventName, Action listener)
    {
        Subscribe(eventName, (Delegate)listener);
    }

    public static void Subscribe(EventType eventName, Action<object> listener)
    {
        Subscribe(eventName, (Delegate)listener);
    }

    private static void Subscribe(EventType eventName, Delegate listener)
    {
        if (eventTable.ContainsKey(eventName))
        {
            // 해당 이벤트가 이미 존재하면 새로운 리스너를 리스트에 추가
            eventTable[eventName].Add(listener);
        }
        else
        {
            // 해당 이벤트가 없으면 새로운 이벤트를 생성하고 첫 번째 리스너를 추가
            eventTable[eventName] = new List<Delegate> { listener };
        }
    }

    public static void Unsubscribe(EventType eventName, Action listener)
    {
        Unsubscribe(eventName, (Delegate)listener);
    }

    public static void Unsubscribe(EventType eventName, Action<object> listener)
    {
        Unsubscribe(eventName, (Delegate)listener);
    }

    private static void Unsubscribe(EventType eventName, Delegate listener)
    {
        if (eventTable.ContainsKey(eventName))
        {
            // 해당 이벤트가 존재하면 리스너를 리스트에서 제거
            eventTable[eventName].Remove(listener);

            // 모든 리스너가 제거되면 이벤트를 제거
            if (eventTable[eventName].Count == 0)
            {
                eventTable.Remove(eventName);
            }
        }
    }

    public static void TriggerEvent(EventType eventName)
    {
        TriggerEvent(eventName, null);
    }

    public static void TriggerEvent(EventType eventName, object parameter)
    {
        if (eventTable.ContainsKey(eventName))
        {
            // 해당 이벤트에 등록된 모든 리스너 호출
            foreach (var listener in eventTable[eventName])
            {
                if (listener is Action actionListener)
                {
                    // 리스너가 Action 형태일 경우
                    actionListener?.Invoke();
                }
                else if (listener is Action<object> actionObjectListener)
                {
                    // 리스너가 Action<object> 형태일 경우
                    actionObjectListener?.Invoke(parameter);
                }
            }
        }
    }
}