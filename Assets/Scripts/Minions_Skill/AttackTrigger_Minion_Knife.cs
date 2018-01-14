using UnityEngine;
using System.Collections;

public class AttackTrigger_Minion_Knife : MonoBehaviour {

    private int dmg = 50;

    void Start()
    {
       
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyCastle")
        {
            col.SendMessageUpwards("Damage", dmg);
            Debug.Log("HIT");
        }   
    }

}
