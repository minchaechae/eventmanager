using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EventType
{
    // Define event types here

}

public class EventManager : MonoBehaviour
{
    private static Dictionary<EventType, List<Delegate>> eventTable = new Dictionary<EventType, List<Delegate>>();

    // Generic method to add a listener for any type of event
    public static void AddListener<T>(EventType eventName, Action<T> listener) => AddListenerInternal(eventName, listener);

    // Non-generic overload for parameterless events
    public static void AddListener(EventType eventName, Action listener) => AddListenerInternal(eventName, listener);

    // Generic method to remove a listener for any type of event
    public static void RemoveListener<T>(EventType eventName, Action<T> listener) => RemoveListenerInternal(eventName, listener);

    // Non-generic overload for parameterless events
    public static void RemoveListener(EventType eventName, Action listener) => RemoveListenerInternal(eventName, listener);

    // Internal method to add a listener to the dictionary
    private static void AddListenerInternal(EventType eventName, Delegate listener)
    {
        if (!eventTable.TryGetValue(eventName, out var listeners))
        {
            listeners = new List<Delegate>();
            eventTable[eventName] = listeners;
        }
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    // Internal method to remove a listener from the dictionary
    private static void RemoveListenerInternal(EventType eventName, Delegate listener)
    {
        if (eventTable.TryGetValue(eventName, out var listeners))
        {
            listeners.Remove(listener);
            if (listeners.Count == 0)
            {
                eventTable.Remove(eventName);
            }
        }
    }

    // Generic method to trigger an event for any type of parameter
    public static void TriggerEvent<T>(EventType eventName, T parameter = default)
    {
        if (!eventTable.TryGetValue(eventName, out var listeners)) return;
        InvokeAll(listeners, parameter);
    }

    // Non-generic overload for parameterless events
    public static void TriggerEvent(EventType eventName)
    {
        if (!eventTable.TryGetValue(eventName, out var listeners)) return;
        InvokeAll(listeners);
    }

    // Internal method to invoke all listeners for an event with a parameter
    private static void InvokeAll<T>(List<Delegate> listeners, T parameter)
    {
        var listenersCopy = new List<Delegate>(listeners);
        foreach (Action<T> action in listenersCopy)
        {
            action.Invoke(parameter);
        }
    }

    // Internal method to invoke all listeners for a parameterless event
    private static void InvokeAll(List<Delegate> listeners)
    {
        var listenersCopy = new List<Delegate>(listeners);
        foreach (Action action in listenersCopy)
        {
            action.Invoke();
        }
    }
}
