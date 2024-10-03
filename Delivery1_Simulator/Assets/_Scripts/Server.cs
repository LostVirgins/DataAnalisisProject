using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour
{
    void OnEnable()
    {
        Simulator.OnNewPlayer += OnPlayerReceived;
        Simulator.OnNewSession += OnSessionReceived;
        Simulator.OnEndSession += OnSessionEnd;
        Simulator.OnBuyItem += OnBuyReceived;
    }
    void OnDisable()
    {
        Simulator.OnNewPlayer -= OnPlayerReceived;
        Simulator.OnNewSession -= OnSessionReceived;
        Simulator.OnEndSession -= OnSessionEnd;
        Simulator.OnBuyItem -= OnBuyReceived;
    }

    private void OnPlayerReceived(string playerName, string country, int age, float gender, DateTime joinDate)
    {
        Debug.Log($"[Server] Received New Player - Name: {playerName}, Country: {country}, Age: {age}, Gender: {gender}, Date: {joinDate}");
        CallbackEvents.OnAddPlayerCallback.Invoke(1);
    }

    private void OnSessionReceived(DateTime beginSessionDate, uint playerId)
    {
        Debug.Log($"[Server] Received New Session - PlayerID: {playerId}, Date: {beginSessionDate}");
        CallbackEvents.OnNewSessionCallback.Invoke(1);
    }

    private void OnSessionEnd(DateTime endSessionDate, uint sessionId)
    {
        Debug.Log($"[Server] Received New Session - Date: {endSessionDate}");
        CallbackEvents.OnEndSessionCallback.Invoke(1);
    }

    private void OnBuyReceived(int itemId, DateTime buyDate, uint sessionId)
    {
        Debug.Log($"[Server] Received New Session - SessionID: {sessionId}, ItemID: {itemId}, Date: {buyDate}");
        CallbackEvents.OnItemBuyCallback.Invoke(1);
    }
}
