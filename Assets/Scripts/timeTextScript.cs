using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class timeTextScript : MonoBehaviour
{
    Text timeText;
    public int currentTime;
    // Use this for initialization
    void Start ()
    {
	    timeText = GetComponent<Text>();
        currentTime = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
       timeText.text = "Elapsed Time: " + (int) GameManager.currentTime;
    }
}

