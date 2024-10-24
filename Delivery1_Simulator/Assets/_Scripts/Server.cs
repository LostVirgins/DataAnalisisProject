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
        WWWForm form = new WWWForm();
        form.AddField("table", "UserInfo");
        form.AddField("playerName", playerName);
        form.AddField("country", country);
        form.AddField("age", age);
        form.AddField("gender", gender.ToString(CultureInfo.InvariantCulture));
        string formattedDate = joinDate.ToString("yyyy-MM-dd HH:mm:ss");
        form.AddField("joinDate", formattedDate);
        StartCoroutine(Upload(form, CallbackEvents.OnAddPlayerCallback));
    }

    private void OnSessionReceived(DateTime beginSessionDate, uint playerId)
    {
        WWWForm form = new WWWForm();
        form.AddField("table", "NewSessions");
        form.AddField("playerId", playerId.ToString());
        string formattedDate = beginSessionDate.ToString("yyyy-MM-dd HH:mm:ss");
        form.AddField("beginSessionDate", formattedDate);
        StartCoroutine(Upload(form, CallbackEvents.OnNewSessionCallback));
    }

    private void OnSessionEnd(DateTime endSessionDate, uint sessionId)
    {
        WWWForm form = new WWWForm();
        form.AddField("table", "NewSessions");
        form.AddField("sessionId", sessionId.ToString());
        string formattedDate = endSessionDate.ToString("yyyy-MM-dd HH:mm:ss");
        form.AddField("endSessionDate", formattedDate);
        StartCoroutine(Upload(form, CallbackEvents.OnEndSessionCallback));
    }

    private void OnBuyReceived(int itemId, DateTime buyDate, uint sessionId)
    {
        WWWForm form = new WWWForm();
        form.AddField("table", "ItemSales");
        form.AddField("sessionId", sessionId.ToString());
        form.AddField("itemId", itemId.ToString());
        string formattedDate = buyDate.ToString("yyyy-MM-dd HH:mm:ss");
        form.AddField("buyDate", formattedDate);
        StartCoroutine(Upload(form, CallbackEvents.OnItemBuyCallback));
    }

    IEnumerator Upload(WWWForm form, Action<uint> callback)
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
                string response = www.downloadHandler.text;
                Debug.Log("Server response: " + response);

                int id;
                if (int.TryParse(response, out id))
                {
                    callback.Invoke((uint)id);
                }
                else
                {
                    Debug.LogError("Error: Couldn't parse the server response.");
                }
            }
        }
    }
}
