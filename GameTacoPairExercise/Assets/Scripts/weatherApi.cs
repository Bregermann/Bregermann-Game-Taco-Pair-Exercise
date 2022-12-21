using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class weatherApi : MonoBehaviour
{
    public so_key key;
    public TMP_InputField cityInput;
    public TMP_InputField tempGuessText;
    public int tempGuess;

    //user input city
    //user guess temperature, if with 5 degrees, win

    private void Start()
    {
        // example
    }

    public void GetWeatherPress()
    {
        StartCoroutine(GetWeather(cityInput.text));
    }

    private IEnumerator GetWeather(string city)
    {
        string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=imperial&appid=" + key.apiKey;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;

                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    public void CreateFromJSON(string pages)
    {
        return JsonUtility.FromJson<temp>(string pages);
    }

    private void CheckTemp()
    {
        //
    }
}