using UnityEngine;
using System.Collections;

public class Explosion_Minion : MonoBehaviour
{
    private int dmg = 30;
    // Use this for initialization
    private int time = 0;
	void Start () {
        //Physics2D.IgnoreLayerCollision(12, 10);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyCastle")
        {
            Debug.Log("Kesentuh Bom!");
            col.SendMessageUpwards("Damage", dmg);
            Destroy(gameObject);
        }
    }
}
