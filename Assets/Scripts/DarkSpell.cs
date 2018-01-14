using UnityEngine;
using System.Collections;

public class DarkSpell : MonoBehaviour
{
    private float speed = 2f;
    private int dmg = 30;
	private int time =0;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        move();
	}

    public void move()
    {
        Vector3 move = new Vector3(-1f, 0f, 0f);
        transform.position += move * speed * Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Minion" || col.gameObject.tag == "Castle")
        {
            Debug.Log("Kesentuh DarkSpell!");
            col.SendMessageUpwards("Damage", dmg);
            Destroy(gameObject);
        }
    }
}
