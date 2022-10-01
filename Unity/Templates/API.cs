using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class API : MonoBehaviour
{
    private static API s_Instance = null;
    private static API Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(API)) as API;
            }

            if (s_Instance == null)
            {
                GameObject obj = new GameObject("API");
                s_Instance = obj.AddComponent(typeof(API)) as API;
                Debug.Log("Could not locate a API object. API was generated automatically.");
            }
            return s_Instance;
        }
    }

    public static void Get<T>(string url, System.Action<T> callback)
    {
        Instance.LocalGet<T>(url, callback);
    }

    public static void Post<T>(string url, T data, System.Action<T> callback)
    {
        Instance.LocalPost<T>(url, data, callback);
    }

    private void LocalGet<T>(string url, System.Action<T> callback)
    {
        StartCoroutine(GetRequest<T>(url, callback));
    }

    private void LocalPost<T>(string url, T data, System.Action<T> callback)
    {
        StartCoroutine(PostRequest<T>(url, data, callback));
    }

    private IEnumerator GetRequest<T>(string uri, System.Action<T> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                var result = JsonUtility.FromJson<T>(webRequest.downloadHandler.text);
                callback(result);
            }
            else
            {
                Debug.Log("Error while sending GET request: " + webRequest.error);
            }
        }
    }

    private IEnumerator PostRequest<T>(string uri, T data, System.Action<T> callback)
    {
        var json = JsonUtility.ToJson(data);
        var bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);

        using (UnityWebRequest webRequest = new UnityWebRequest(uri, "POST"))
        {
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                var result = JsonUtility.FromJson<T>(webRequest.downloadHandler.text);
                callback(result);
            }
            else
            {
                Debug.Log("Error while sending POST request: " + webRequest.error);
            }
        }
    }

}
