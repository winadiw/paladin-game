using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
	public static float maxHealth=750;
    public static float currentHealth;
	private bool isDead = false;
    public bool shielded = false;
	private float speed = 1.5f;
    private float shieldTimer = 0f;
    public float damageTimer = 0f;
    private	Animator animator;

    public AudioClip swordSound;
    public AudioClip skillSound;
    public AudioClip rageSound;
    public AudioClip auraSound;
    public AudioClip hurtSound;

    public float shieldMana = 15f;
    public float sliceMana = 25f;
    public float damageMana = 10f;

    public SpriteRenderer shieldAura;
    public SpriteRenderer damageAura;

    public Collider2D attackTrigger;
	public GameObject slice;
	public Transform skillSpawn;
	AnimatorStateInfo currentState;
	float playbackTime;
    public Vector3 theScale;

    public SkillCoolDown skill;
    public AttackTrigger attack;
    public Slice sliceScript;

    public GameObject gameManager;
    // Use this for initialization
    void Start() 
	{
        currentHealth = maxHealth+(GameManager.currentLevel*100);
        attackTrigger.enabled = false;
		animator = GetComponent<Animator> ();
        Physics2D.IgnoreLayerCollision(10, 8);
        Physics2D.IgnoreLayerCollision(10, 13);
    }

	// Update is called once per frame
	void Update () 
	{
		currentState = animator.GetCurrentAnimatorStateInfo(0);
		playbackTime = currentState.normalizedTime % 1;

		if(isDead == false)
			PlayerMove ();
        
		//Debug.Log(transform.position);
		if (currentState.IsName("PlayerAttack") && playbackTime > 0.3) 
		{
			attackTrigger.enabled = true;
		} 
		else
			attackTrigger.enabled = false;

		if(shieldTimer <= 0f)
		{
			shieldAura.enabled = false;
			shielded = false;
		}
		else
			shieldTimer -= 0.1f;

        if (damageTimer <= 0f)
        {
            damageAura.enabled = false;
            attack.disableDoubleDamage();
            sliceScript.disableDoubleDamage();
        }
        else
            damageTimer -= 0.1f;
	}

    public void PlayerMove()
    {
        theScale = transform.localScale;
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        if (GameManager.paused == false)
        { 
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"))
                {
                    if (theScale.x < 0)
                    {
                        theScale.x *= -1;
                        transform.localScale = theScale;
                    }
                    transform.position += move * speed * Time.deltaTime;
                    animator.SetTrigger("PlayerMove");
                }

            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"))
                {
                    if (theScale.x > 0)
                    {
                        theScale.x *= -1;
                        transform.localScale = theScale;
                    }
                    transform.position += move * speed * Time.deltaTime;
                    animator.SetTrigger("PlayerMove");
                }
            }

            else if (Input.GetKeyDown(KeyCode.Z))
            {
                if (theScale.x >= 0)
                {
                    animator.SetTrigger("PlayerAttack");
                    SoundManager.instance.PlaySingle(swordSound);
                }
            }

            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (skill.getCurrentCooldown(0) >= skill.getCooldown(0))
                {
                    if (GameManager.currentMana >= damageMana)
                    {
                        damageAura.enabled = true;
                        Debug.Log(damageAura.enabled);
                        damageTimer = 30f;
                        attack.enableDoubleDamage();
                        sliceScript.enableDoubleDamage();
                        GameManager.useSkill(damageMana);
                        skill.setCurrentCooldownZero(0);
                        SoundManager.instance.PlaySingle(rageSound);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //skill aura
                if (skill.getCurrentCooldown(1) >= skill.getCooldown(1))
                {
                    if (GameManager.currentMana >= shieldMana)
                    {
                        SoundManager.instance.PlaySingle(skillSound);
                        shieldAura.enabled = true;
                        shielded = true;
                        shieldTimer = 50f;
                        GameManager.useSkill(shieldMana);
                        skill.setCurrentCooldownZero(1);
                        SoundManager.instance.PlaySingle(auraSound);
                    }
                    else Debug.Log("Not Enough Mana for SHIELD!");
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                //skill slice
                if (skill.getCurrentCooldown(2) >= skill.getCooldown(2))
                {
                    if (GameManager.currentMana >= sliceMana && theScale.x >= 0)
                    {
                        SoundManager.instance.PlaySingle(skillSound);
                        Instantiate(slice, skillSpawn.position, skillSpawn.rotation);
                        GameManager.useSkill(sliceMana);
                        skill.setCurrentCooldownZero(2);
                    }
                    else Debug.Log("Not Enough Mana for SLICE!");
                }
            }
            else
            {
                animator.SetTrigger("PlayerIdle");
            }
        }
	}

	public void Damage(int damage)
	{
		if(shielded == false)
		{
            if(currentHealth>0)
            {
                currentHealth -= damage;
                SoundManager.instance.PlaySingle(hurtSound);
				animator.SetTrigger("PlayerAttacked");
			}

			else if(currentHealth <= 0)
			{
                StartCoroutine(playerDead());
			}
		}
	}

    IEnumerator playerDead()
    {
        isDead = true;
        animator.SetTrigger("PlayerDie");
        yield return new WaitForSeconds(3f);
        //SoundManager.instance.destroyMusic();
        MenuManager.menuID = 1; //dead Panel
        gameManager.GetComponent<GameManager>().Reset();
        Application.LoadLevel("Main Menu");
        
    }
}

