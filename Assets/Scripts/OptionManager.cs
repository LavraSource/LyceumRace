using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public static float soundVolume=1f;
    public Slider slider;// Start is called before the first frame update
    public void GameStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void QuitGame(){
        Application.Quit();
    }
    void Awake()
    {
        slider.value=soundVolume;
    }
    // Update is called once per frame
    void Update()
    {
        soundVolume = slider.value;
    }
}
