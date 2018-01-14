using UnityEngine;
using System.Collections;

public class Castle : MonoBehaviour {

	private int health = 100;

    public Rigidbody2D minion;
    public Transform castleDoor;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Damage(int damage)
	{
		health -= damage;
		//rigidbody.AddForce ((new Vector2 (50f, 0f)) * 100);
		Debug.Log (health);
	}
}
