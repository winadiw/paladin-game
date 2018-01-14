using UnityEngine;
using System.Collections;

public class MushroomCollider : MonoBehaviour
{
    private int dmg = 50;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Something Spotted from Mushroom");
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Minion" || col.gameObject.tag == "Castle")
        {
            Debug.Log("Kesentuh Mushroom!");
            col.SendMessageUpwards("Damage", dmg);
            Destroy(gameObject);
        }

    }
}
