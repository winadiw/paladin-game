using UnityEngine;
using System.Collections;

public class AttackTrigger : MonoBehaviour {

    public int currentDmg;
    public int singleDamage;
    private int doubleDamage;

    void Start()
    {
        currentDmg = singleDamage;
       doubleDamage = singleDamage * 2;
    }
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log ("HIT");
        if(col.gameObject.tag == "EnemyCastle" || col.gameObject.tag == "Enemy")
		    col.SendMessageUpwards("Damage", currentDmg);
	}

    public void enableDoubleDamage()
    {
        currentDmg = doubleDamage;
    }
    public void disableDoubleDamage()
    {
        currentDmg = singleDamage;
    }
   
}
