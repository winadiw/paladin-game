using UnityEngine;
using System.Collections;

public class ClawAttack : MonoBehaviour {

    private int dmg = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
      
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Minion" || col.gameObject.tag == "Castle")
        {
            Debug.Log("Kesentuh Claw!");
            col.SendMessageUpwards("Damage", dmg);
            Destroy(gameObject);
        }

    }
}
