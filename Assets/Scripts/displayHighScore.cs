using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class displayHighScore : MonoBehaviour
{
    Text highScoreText;
    // Use this for initialization
    void Awake()
    {
        highScoreText = GetComponent<Text>();
        Debug.Log("High Score 1: " + PlayerPrefs.GetFloat("highScore1"));
    }

    // Update is called once per frame
    void Update()
    {
        switch(MenuManager.lastLevel)
        {
            case 1:
                highScoreText.text = (int) PlayerPrefs.GetFloat("highScore1") + " seconds";
                break;
            case 2:
                highScoreText.text = (int) PlayerPrefs.GetFloat("highScore2") + " seconds";
                break;
            case 3:
                highScoreText.text = (int) PlayerPrefs.GetFloat("highScore3") + " seconds";
                break;
            case 4:
                highScoreText.text = (int) PlayerPrefs.GetFloat("highScore4") + " seconds";
                break;
            case 5:
                highScoreText.text = (int) PlayerPrefs.GetFloat("highScore5") + " seconds";
                break;
        }
        
    }
}
