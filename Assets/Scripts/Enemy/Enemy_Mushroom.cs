using UnityEngine;
using System.Collections;

public class Enemy_Mushroom : MonoBehaviour
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
    public bool spottedPlayer;
    public bool spottedCastle;
    public bool spottedMinion;

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
        Physics2D.IgnoreLayerCollision(9, 14);
        Physics2D.IgnoreLayerCollision(9, 9);
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

        spottedMinion = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Minion"));
        spottedPlayer = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));
        spottedCastle = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Castle"));

        if (spottedPlayer || spottedCastle || spottedMinion)
        {

        }
        else
        {
            EnemyMove();
        }
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Minion" || col.gameObject.tag == "Castle")
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

