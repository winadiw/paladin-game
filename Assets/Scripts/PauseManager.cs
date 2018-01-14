using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public static int pauseID = 0;
    public GameObject[] menuPanels;

    private GameObject pausePanel;
    private GameObject mainCanvasPanel;
    // Use this for initialization
    void Start ()
    {
        menuPanels = GameObject.FindGameObjectsWithTag("MenuPanel");
        mainCanvasPanel = GameObject.Find("Canvas");
        pausePanel = GameObject.Find("PauseMenuCanvas");


    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.paused == true && pauseID == 1)
            switchToMenu(1);
        else if (GameManager.paused == false && pauseID == 0)
            switchToMenu(0);
    }

    public void switchToMenu(int pauseID)
    {
        foreach (GameObject panel in menuPanels)
        {
            //panel.gameObject.renderer.enabled=false;
            panel.gameObject.SetActive(false);
            Debug.Log(panel.name);
        }

        switch (pauseID)
        {
            case 0: //Main Panel
                mainCanvasPanel.gameObject.SetActive(true);
                pausePanel.gameObject.SetActive(false);
                break;
            case 1: //Pause Panel
                mainCanvasPanel.gameObject.SetActive(false);
                pausePanel.gameObject.SetActive(true);
                break;
        }
    }
        
}
