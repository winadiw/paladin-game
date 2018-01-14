using UnityEngine;
using System.Collections;

public class Minion_Knife : MonoBehaviour
{
    private string enemyName = "Minion_Knife";
    private int currentHealth;
    private int maxHealth = 2500;
    private float speed = 0.7f;
    private float attackCooldown = 0f;
    public Transform sightStart, sightEnd;
    public bool spottedEnemy;
    public bool spottedCastle;

    private Animator animator;
    AnimatorStateInfo currentState;
    public Transform objectFront;

    public Rigidbody2D slice;

    private bool isPlaying;
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

    float playbackTime;
    
    //public Transform other;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentState = animator.GetCurrentAnimatorStateInfo(0);
        playbackTime = currentState.normalizedTime % 1;
        
        Raycasting();

        percentOfHp = (float)currentHealth / (float)maxHealth;
        objectScreen = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.0f, 0.9f, 0.0f));

        left = objectScreen.x - (healthBarWidth / 1);
        top = objectScreen.y + (healthBarHeight / 2);

        attackCooldown -= 0.1f;
    }

    public void EnemyMove()
    {
        Vector3 move = new Vector3(1f, 0f, 0f);
        transform.position += move * speed * Time.deltaTime;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy: " + currentHealth);

        if (currentHealth <= 0)
            Destroy(gameObject);
    }

    public void EnemyAttack()
    {
        if (attackCooldown <= 0)
        {
            Debug.Log("Masuk Enemy Attack!");
            animator.SetTrigger("isAttacking");
            Instantiate(slice, objectFront.position, objectFront.rotation);
            animator.SetTrigger("isWalking");
           
            attackCooldown = 20f;
        }
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
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }
}
