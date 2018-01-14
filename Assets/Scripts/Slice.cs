using UnityEngine;
using System.Collections;

public class Slice : MonoBehaviour {

	private float speed = 2f;
    public int currentDamage;
    public int singleDamage;
    private int doubleDamage;

    Vector3 tempPos;
	// Use this for initialization
    void Start()
    {
        tempPos = transform.position;
        currentDamage = singleDamage;
        doubleDamage = singleDamage * 2;
    }
    // Update is called once per frame
    void Update () {

		move();
	}

	public void move()
	{
		Vector3 move = new Vector3(1f,0f,0f);
		transform.position += move * speed * Time.deltaTime;
		//Debug.Log(transform.position.x + "  " + tempPos.x);
		if(Mathf.Abs(tempPos.x) - Mathf.Abs(transform.position.x) >= 0.5f)
			Destroy(gameObject);
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyCastle")
            col.SendMessageUpwards("Damage", currentDamage);
	}

    public void enableDoubleDamage()
    {
        currentDamage = doubleDamage;
    }
    public void disableDoubleDamage()
    {
        currentDamage = singleDamage;
    }
}
