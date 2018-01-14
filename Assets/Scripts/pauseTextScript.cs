using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pauseTextScript : MonoBehaviour
{
    Text pauseText;
    // Use this for initialization
    void Start ()
    {
        pauseText = GetComponent<Text>();
        pauseText.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        pauseText.text = "PAUSED";
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameManager.paused == false)
            {
                Time.timeScale = 0;
                GameManager.paused = true;
                pauseText.enabled = true;
            }
            else
            {
                pauseText.enabled = false;
                Time.timeScale = 1;
                GameManager.paused = false;
            }
        }
    }
}
