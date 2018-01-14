using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class manaTextScript : MonoBehaviour 
{
	Text manaText;
    private int mana; 
	// Use this for initialization
	void Awake () 
	{
		manaText = GetComponent<Text> ();
		mana = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
        mana = (int) GameManager.currentMana;
		manaText.text = "Mana: " + mana;
	}
}
