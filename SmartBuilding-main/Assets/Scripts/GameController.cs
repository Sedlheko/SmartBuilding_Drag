using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public string key_openweather = "aaf0e01f42167306177e7108e864ba05";
    [SerializeField]
    public string base_openweather_url = "https://api.openweathermap.org/data/2.5/weather";
    public string lat,lon;
    public string weather_main,weather_description,weather_temp,weather_humidity;
    [SerializeField]
    public TMP_Text[] textmesh;

    public bool isToggle = false;
    public string jsonData;
    void Start()
    {
        string url = base_openweather_url+"?lat="+lat+"&lon="+lon+"&appid="+key_openweather;
        UnityWebRequest www = new UnityWebRequest(url);
        StartCoroutine(FetchWeatherData(url));
    }

    IEnumerator FetchWeatherData(string URL)
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();
 
        if (www.isNetworkError || www.isHttpError){
            Debug.Log(www.error);
        }else{
            jsonData = www.downloadHandler.text;
            JSONNode jsonNode = SimpleJSON.JSON.Parse(jsonData);
            for (int i = 0; i < jsonNode.Count; i++)
            {
                weather_main = jsonNode["weather"][0]["main"].ToString();
                weather_description = jsonNode["weather"][0]["description"].ToString();
                double kelvin = jsonNode["main"]["temp"];
                double real_temp = kelvin-273.15;
                weather_temp = "Temp:" +real_temp.ToString()+" °C";
                weather_humidity = jsonNode["main"]["humidity"].ToString();
                textmesh[0].text = weather_main;
                textmesh[1].text = weather_description;
                textmesh[2].text = "Humidity: "+weather_humidity;
                textmesh[3].text = weather_temp;
            }
        }
    }

    public string get_weather_main(){
        string resultValue;
        resultValue = weather_main;
        return resultValue;
    }

    public string get_weather_description(){
        string resultValue;
        resultValue = weather_description;
        return resultValue;
    }

    public string get_weather_temp(){
        string resultValue;
        resultValue = weather_temp;
        return resultValue;
    }

    public string get_weather_humidity(){
        string resultValue;
        resultValue = weather_humidity;
        return resultValue;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Toggle(){
        if(isToggle){
            isToggle = false;
        }else{
            isToggle = true;
        }
    }
}
