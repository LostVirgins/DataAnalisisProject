using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

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
        //Example recieve
        //Debug.Log($"[Server] Received New Player - Name: {playerName}, Country: {country}, Age: {age}, Gender: {gender}, Date: {joinDate}");

        WWWForm form = new WWWForm();
        form.AddField("playerName", playerName);
        form.AddField("country", country);
        form.AddField("age", age);
        form.AddField("gender", gender.ToString(CultureInfo.InvariantCulture));
        form.AddField("date", joinDate.ToString());
        StartCoroutine(Upload(form));

        //Exaple id
       // CallbackEvents.OnAddPlayerCallback.Invoke(1);
    }

    private void OnSessionReceived(DateTime beginSessionDate, uint playerId)
    {
        //Example recieve
        //Debug.Log($"[Server] Received New Session - PlayerID: {playerId}, Date: {beginSessionDate}");

        WWWForm form = new WWWForm();
        form.AddField("playerId", playerId.ToString());
        form.AddField("beginSessionDate", beginSessionDate.ToString());
        StartCoroutine(Upload(form));

        //Exaple id
        //CallbackEvents.OnNewSessionCallback.Invoke(1);
    }

    private void OnSessionEnd(DateTime endSessionDate, uint sessionId)
    {
        //Example recieve
        //Debug.Log($"[Server] Received New Session - Date: {endSessionDate}");

        WWWForm form = new WWWForm();
        form.AddField("sessionId", sessionId.ToString());
        form.AddField("endSessionDate", endSessionDate.ToString());
        StartCoroutine(Upload(form));

        //Exaple id
        //CallbackEvents.OnEndSessionCallback.Invoke(1);
    }

    private void OnBuyReceived(int itemId, DateTime buyDate, uint sessionId)
    {
        //Example recieve
        //Debug.Log($"[Server] Received New Session - SessionID: {sessionId}, ItemID: {itemId}, Date: {buyDate}");

        WWWForm form = new WWWForm();
        form.AddField("sessionId", sessionId.ToString());
        form.AddField("itemId", itemId.ToString());
        form.AddField("buyDate", buyDate.ToString());
        StartCoroutine(Upload(form));

        //Exaple id
        //CallbackEvents.OnItemBuyCallback.Invoke(1);
    }

    IEnumerator Upload(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~franciscofp4/dataHandlerSimplified.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.text);

            }
        }
    }
}
