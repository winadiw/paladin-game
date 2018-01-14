using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class displayElapsedTime : MonoBehaviour {

    Text elapsedText;
    // Use this for initialization
    void Awake()
    {
        elapsedText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedText.text = MenuManager.lastScore + " seconds";
    }
}
