# 💡 How to use EventManager
## 1. Define Event Types
Define some event types in your 'EventType' enum.

```C#
// EventManager.cs

public enum EventType
{
    PlayerHealthChanged,
    EnemyKilled,
    GameStarted,
    GameOver
}
```

## 2. Create Event Listeners
Listeners are methods that respond when an event is triggered.<br/>
You can create multiple listeners for a single event.

```C#
// A.cs

void OnPlayerHealthChanged(int newHealth)
{
    Debug.Log($"Player health changed to {newHealth}");
}

void OnEnemyKilled()
{
    Debug.Log("An enemy was killed!");
}

void OnGameStarted()
{
    Debug.Log("Game Started!");
}

void OnGameOver()
{
    Debug.Log("Game Over!");
}
```

## 3. Register Event Listeners
Register these listeners with the 'EventManager' so that they can be called when the relevant events are triggered.

```C#
// A.cs

void Start()
{
    EventManager.AddListener<int>(EventType.PlayerHealthChanged, OnPlayerHealthChanged);
    EventManager.AddListener(EventType.EnemyKilled, OnEnemyKilled);
    EventManager.AddListener(EventType.GameStarted, OnGameStarted);
    EventManager.AddListener(EventType.GameOver, OnGameOver);
}
```

## 4. Trigger Events
Events can be triggered from anywhere in the code, assuming the events have listeners registered.

```C#
// B.cs

void Update()
{
    if (Input.GetKeyDown(KeyCode.P))
    {
        EventManager.TriggerEvent<int>(EventType.PlayerHealthChanged, 90);
    }

    if (Input.GetKeyDown(KeyCode.K))
    {
        EventManager.TriggerEvent(EventType.EnemyKilled);
    }

    if (Input.GetKeyDown(KeyCode.S))
    {
        EventManager.TriggerEvent(EventType.GameStarted);
    }

    if (Input.GetKeyDown(KeyCode.G))
    {
        EventManager.TriggerEvent(EventType.GameOver);
    }
}
```

## 5. Unregister Event Listeners
Especially in cases where objects can be destroyed, you should unregister event listeners to prevent memory leaks or unwanted behavior.
```C#
// A.cs

void OnDestroy()
{
    EventManager.RemoveListener<int>(EventType.PlayerHealthChanged, OnPlayerHealthChanged);
    EventManager.RemoveListener(EventType.EnemyKilled, OnEnemyKilled);
    EventManager.RemoveListener(EventType.GameStarted, OnGameStarted);
    EventManager.RemoveListener(EventType.GameOver, OnGameOver);
}
```
