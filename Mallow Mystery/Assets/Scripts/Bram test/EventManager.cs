using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<Vector3> CoinCollected;
    public static event UnityAction Dialog;
    public static event UnityAction LevelEvent;
    public static event UnityAction PlayerEvent;
    public static void OnCoinCollected(Vector3 position) => CoinCollected?.Invoke(position); 
    public static void OnDialog() => Dialog?.Invoke(); 
    public static void OnLevelEvent() => LevelEvent?.Invoke(); 
    public static void OnPlayerEvent() => PlayerEvent?.Invoke(); 

}
