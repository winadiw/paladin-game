﻿using UnityEngine;
using System.Collections;

public class Minion_Log : MonoBehaviour
{
    public AudioClip darkSound;
    //SoundManager.instance.PlaySingle(darkSound);
    private string enemyName = "Haunted Log";
    private int currentHealth;
    private int maxHealth = 100;
    private float speed = 0.5f;
	private float attackCooldown = 0f;
	public Transform sightStart, sightEnd;
	public bool spottedEnemy;
	public bool spottedCastle;
	public Rigidbody2D darkSpell;
    public Transform logHead;

    public Texture2D HpBarTexture;
    public Texture2D ManaBarTexture;
    float hpBarLength;
    float percentOfHp;
    float manaBarLength;
    float percentOfMana;
    private float left;
    private float top;
    private Vector2 objectScreen;
    private int healthBarWidth = 25;
    private float healthBarHeight = 0.1f;
    //public Transform other;
    // Use this for initialization
    void Start()
	{
        currentHealth = maxHealth;
	}

    // Update is called once per frame
    void Update()
    {
        if (GameManager.paused == false)
        {
            Raycasting();

            percentOfHp = (float)currentHealth / (float)maxHealth;
            objectScreen = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.0f, 0.9f, 0.0f));

            left = objectScreen.x - (healthBarWidth / 1);
            top = objectScreen.y + (healthBarHeight / 2);

            attackCooldown -= 0.1f;
        }
    }
    public void EnemyMove()
	{
		Vector3 move = new Vector3(1f, 0f, 0f);
		transform.position += move * speed * Time.deltaTime;
	}

	public void EnemyAttack()
	{
		if (attackCooldown <= 0)
		{
			Instantiate(darkSpell, logHead.position, logHead.rotation);
			attackCooldown = 20f;
            SoundManager.instance.PlaySingle(darkSound);
        }
	}

	public void Damage(int damage)
	{
		currentHealth -= damage;
		Debug.Log("Enemy: " + currentHealth);

		if (currentHealth <= 0)
			Destroy(gameObject);
	}

	public void Raycasting()
	{
		Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
        spottedEnemy = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Enemy"));
        spottedCastle = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("EnemyCastle"));

        if (spottedEnemy || spottedCastle)
		{
			EnemyAttack();
		}
		else
		{
			EnemyMove();
		}
	}

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(left, top, (50 * percentOfHp), 5), HpBarTexture);
    }
}

