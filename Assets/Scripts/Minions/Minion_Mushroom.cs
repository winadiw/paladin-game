using UnityEngine;
using System.Collections;

public class Minion_Mushroom : MonoBehaviour
{
    public AudioClip explosionSound;
    //SoundManager.instance.PlaySingle(explosionSound);
    private string enemyName = "Mushroom";
    private int currentHealth;
    private int maxHealth = 50;
    private int dmg = 50;
	private float speed = 0.5f;
	private float attackCooldown = 0f;
	public Transform sightStart, sightEnd;
	public bool spottedEnemy;
	public bool spottedCastle;

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
        Physics2D.IgnoreLayerCollision(8, 13);
        Physics2D.IgnoreLayerCollision(13, 13);
        Physics2D.IgnoreLayerCollision(12, 13);
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
			//Instantiate(darkSpell, logFront.position, logFront.rotation);
			//Sreang tanpa animasi(kalau nyentuh aja)
			attackCooldown = 45f;
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

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyCastle")
        {
            Debug.Log("Kesentuh Mushroom!");
            SoundManager.instance.PlaySingle(explosionSound);
            col.SendMessageUpwards("Damage", dmg);
            Destroy(gameObject);
        }

    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(left, top, (50 * percentOfHp), 5), HpBarTexture);
    }
}

