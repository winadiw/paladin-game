using UnityEngine;
using System.Collections;

public class Enemy_Golem : MonoBehaviour
{
    public AudioClip slashSound;
    //SoundManager.instance.PlaySingle(slashSound);
    private string enemyName = "Golem";
    private int currentHealth;
    private int maxHealth = 350;
    private float speed = 0.3f;
	private float attackCooldown = 0f;
	public Transform sightStart, sightEnd;
    public bool spottedPlayer;
    public bool spottedCastle;
    public bool spottedMinion;
    public Rigidbody2D golemHit;
	public Transform golemFront;

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
		Vector3 move = new Vector3(-1f, 0f, 0f);
		transform.position += move * speed * Time.deltaTime;
	}

	public void EnemyAttack()
	{
		if (attackCooldown <= 0)
		{
			Instantiate(golemHit, golemFront.position, golemFront.rotation);
			attackCooldown = 10f;
            SoundManager.instance.PlaySingle(slashSound);
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
        spottedPlayer = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));
        spottedCastle = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Castle"));
        spottedMinion = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Minion"));

        if (spottedPlayer || spottedCastle || spottedMinion )
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

