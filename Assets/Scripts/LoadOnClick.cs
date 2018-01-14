using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	//public int transitionTime = 60;

	public void LoadScene (int level)
	{
		Application.LoadLevel (level);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
