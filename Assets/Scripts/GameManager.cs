using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int totalPoints;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI result;
    public GameObject panel;
    public static bool lost = false;

    void Start()
    {
        Time.timeScale = 1.25f;
        lost = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.SetText("Монеты: "+AddPoints.score.ToString());
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("Main Menu");
        }
        if(AddPoints.score >= totalPoints){
            result.SetText("Вы победили!!! Нажмите клавишу R, чтобы сыграть снова");
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
        if(lost){
            result.SetText("Вы проиграли!!! Нажмите клавишу R, чтобы сыграть снова");
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
