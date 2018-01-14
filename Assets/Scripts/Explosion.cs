using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    private int dmg = 80;
    // Use this for initialization
    private int time = 0;
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
            Debug.Log("Kesentuh Bom!");
            col.SendMessageUpwards("Damage", dmg);
            Destroy(gameObject);
        }
    }
}
