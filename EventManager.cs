using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum EventType
{
    // Add event type
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
            eventTable[eventName].Add(listener);
        }
        else
        {
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
            eventTable[eventName].Remove(listener);

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
            foreach (var listener in eventTable[eventName])
            {
                if (listener is Action actionListener)
                {
                    actionListener?.Invoke();
                }
                else if (listener is Action<object> actionObjectListener)
                {
                    actionObjectListener?.Invoke(parameter);
                }
            }
        }
    }
}
