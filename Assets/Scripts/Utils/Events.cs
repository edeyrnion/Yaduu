using UnityEngine;
using UnityEngine.Events;

public class Events
{
    [System.Serializable] public class EventFadeComplete : UnityEvent<bool> { }
    [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }
    [System.Serializable] public class EventOnClick : UnityEvent<Vector3, Layer> { }
    [System.Serializable] public class EventOnScroll : UnityEvent<float> { }
    [System.Serializable] public class EventGameObject : UnityEvent<GameObject, Layer> { }
}
