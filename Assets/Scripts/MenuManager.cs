using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static int menuID = 0;
    public static int lastLevel = 0;
    public static float lastScore = 0;
    public GameObject Stars1;
    public GameObject Stars2;
    public GameObject Stars3;
    public static float highScoreCurrent = 0;
    public GameObject[] menuPanels;
    private GameObject mainMenuPanel;
    private GameObject deadPanel;
    private GameObject victoryPanel;

    public GameObject nextButton;
    // Use this for initialization

    void Start()
    {
        menuPanels = GameObject.FindGameObjectsWithTag("MenuPanel");

        mainMenuPanel = GameObject.Find("MainMenuCanvas");
        deadPanel = GameObject.Find("DeadCanvas");
        victoryPanel = GameObject.Find("VictoryCanvas");
        /*
        Stars1 = GameObject.FindGameObjectWithTag("Stars1");
        Stars2 = GameObject.FindGameObjectWithTag("Stars2");
        Stars3 = GameObject.FindGameObjectWithTag("Stars3");
        */
        
        nextButton.gameObject.SetActive(false);
        switchToMenu(menuID);

        lastLevel = GameManager.currentLevel;

        if (lastLevel == 5)
            nextButton.gameObject.SetActive(false);
        else nextButton.gameObject.SetActive(true);

        lastScore = (int) PlayerPrefs.GetFloat("lastStageTime");

        if(lastScore<120)
        {
            Stars1.gameObject.SetActive(true);
            if(lastScore<100)
            {
                Stars2.gameObject.SetActive(true);
                if(lastScore<80)
                {
                    Stars3.gameObject.SetActive(true);
                }
            }
        }
      
        Debug.Log("Last Score: " + lastScore);
        switch (lastLevel)
        {
            case 1:
                highScoreCurrent = (int)PlayerPrefs.GetFloat("highScore1");
                break;
            case 2:
                highScoreCurrent = (int)PlayerPrefs.GetFloat("highScore2");
                break;
            case 3:
                highScoreCurrent = (int)PlayerPrefs.GetFloat("highScore3");
                break;
            case 4:
                highScoreCurrent = (int)PlayerPrefs.GetFloat("highScore4");
                break;
            case 5:
                highScoreCurrent = (int)PlayerPrefs.GetFloat("highScore5");
                break;
        }

        if (lastScore < highScoreCurrent)
        {
            switch (lastLevel)
            {
                case 1:
                    PlayerPrefs.SetFloat("highScore1", lastScore);
                    break;
                case 2:
                    PlayerPrefs.SetFloat("highScore2", lastScore);
                    break;
                case 3:
                    PlayerPrefs.SetFloat("highScore3", lastScore);
                    break;
                case 4:
                    PlayerPrefs.SetFloat("highScore4", lastScore);
                    break;
                case 5:
                    PlayerPrefs.SetFloat("highScore5", lastScore);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void switchToMenu(int menuID)
    {
        foreach (GameObject panel in menuPanels)
        {
            //panel.gameObject.renderer.enabled=false;
            panel.gameObject.SetActive(false);
            Debug.Log(panel.name);
        }

        switch (menuID)
        {
            case 0:
                mainMenuPanel.gameObject.SetActive(true);
                break;
            case 1:
                deadPanel.gameObject.SetActive(true);
                break;
            case 2:
                victoryPanel.gameObject.SetActive(true);
                break;
        }
    }

    public void nextLevel()
    {
        if(GameManager.currentLevel < 5)
            Application.LoadLevel(GameManager.currentLevel + 1); 
    }

}
