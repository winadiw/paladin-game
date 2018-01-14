using UnityEngine;
using System.Collections;

public class GolemHit_Minion : MonoBehaviour
{
    private int dmg = 50;
    // Use this for initialization
    void Start () {
        Physics2D.IgnoreLayerCollision(12, 10);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyCastle")
        {
            Debug.Log("Kesentuh Claw!");
            col.SendMessageUpwards("Damage", dmg);
            Destroy(gameObject);
        }

    }
}
