dusing UnityEngine;
using System.Collections;

public class Minion_Watermelon : MonoBehaviour
{
    public AudioClip explosionSound;
    //SoundManager.instance.PlaySingle(explosionSound);
    private string enemyName = "Watermelon";
    private int currentHealth;
    private int maxHealth = 50;
    private float speed = 0.5f;
	private float attackCooldown = 0f;
	private bool bombState = false;
	public Transform sightStart, sightEnd;
	public bool spottedEnemy;
	public bool spottedCastle;
	public GameObject explosion;
	public Transform explosionSpawn;
	public Animator anim;

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

            if (bombState == false)
            {
                percentOfHp = (float)currentHealth / (float)maxHealth;
                objectScreen = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.0f, 0.9f, 0.0f));

                left = objectScreen.x - (healthBarWidth / 1);
                top = objectScreen.y + (healthBarHeight / 2);
            }

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
        if (!bombState)
        {
            anim.SetBool("Bomb", true);
            bombState = true;
            SoundManager.instance.PlaySingle(explosionSound);
            attackCooldown = 12.0f;
        }
        if (bombState && attackCooldown <= 0)
        {
            GameObject clone = (GameObject)Instantiate(explosion, explosionSpawn.position, explosionSpawn.rotation);
            Destroy(gameObject);
            Destroy(clone, 1.0f);
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

		if (spottedEnemy || spottedCastle) {
			EnemyAttack ();
		} else if (bombState) 
		{
			EnemyAttack ();
		}
		else
		{
			if(!bombState)EnemyMove();
		}
	}

    void OnGUI()
    {
        if(bombState==false)
            GUI.DrawTexture(new Rect(left, top, (50 * percentOfHp), 5), HpBarTexture);
    }
}

