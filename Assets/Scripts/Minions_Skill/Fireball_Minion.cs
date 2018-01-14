using UnityEngine;
using System.Collections;

public class Fireball_Minion : MonoBehaviour {

	private float speed = 2f;
	private int dmg = 5;

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreLayerCollision(12, 10);
    }
	
	// Update is called once per frame
	void Update () {
	
		move();
	}

	public void move()
	{
		Vector3 move = new Vector3(1f,0f,0f);
		transform.position += move * speed * Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyCastle")
        {
            Debug.Log("Kesentuh Fireball!");
			col.SendMessageUpwards("Damage", dmg);
			Destroy(gameObject);
		}

	}
}
