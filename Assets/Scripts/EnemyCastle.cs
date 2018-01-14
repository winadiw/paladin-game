using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyCastle : MonoBehaviour
{
    public AudioClip winSound;
    private int currentHealth;
    private int maxHealth = 1000;
    private int minionCount = 0;
    private float summonCooldown = 0f;
    private bool summon = true;
	public GameObject Enemy_Dragon;
    public GameObject Enemy_Golem;
    public GameObject Enemy_Log;
    public GameObject Enemy_Skeleton;
    public GameObject Enemy_Snake;
    public GameObject Enemy_Watermelon;
    public GameObject Enemy_WeirdMushroom;
    public Transform dragonDoor;
    public Transform golemDoor;
    public Transform logDoor;
    public Transform skeletonDoor;
    public Transform snakeDoor;
    public Transform watermelonDoor;
    public Transform mushroomDoor;

    public Texture2D HpBarTexture;
    public Texture2D ManaBarTexture;
    float hpBarLength;
    float percentOfHp;
    float manaBarLength;
    float percentOfMana;
    private float left;
    private float top;
    private Vector2 objectScreen;
    private int healthBarWidth = 100;
    private float healthBarHeight = 0.1f;

    public GameObject gameManager;

    void Start()
    {
        currentHealth = maxHealth + GameManager.currentLevel*50;
    }

    // Update is called once per frame
    void Update()
    {
        if(summon && GameManager.paused == false)
        {
            summonMinion();
        }

        percentOfHp = (float)currentHealth / (float)maxHealth;
        objectScreen = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.0f, 0.9f, 0.0f));

        left = objectScreen.x - (healthBarWidth / 1) - 50;
        top = objectScreen.y + (healthBarHeight / 2) - 400;

        if (currentHealth <= 0)
        {
            StartCoroutine(nextLevel());
        }

    }

    public void summonMinion()
    {
        if (summonCooldown <= 0)
        {
            int spawn = Random.Range(1, 8);
            if (spawn==1) Instantiate(Enemy_Dragon, dragonDoor.position, dragonDoor.rotation);
            else if (spawn == 2) Instantiate(Enemy_Golem, golemDoor.position, golemDoor.rotation);
            else if (spawn == 3) Instantiate(Enemy_Log, logDoor.position, logDoor.rotation);
            else if (spawn == 4) Instantiate(Enemy_Skeleton, skeletonDoor.position, skeletonDoor.rotation);
            else if (spawn == 5) Instantiate(Enemy_Snake, snakeDoor.position, snakeDoor.rotation);
            else if (spawn == 6) Instantiate(Enemy_Watermelon, watermelonDoor.position, watermelonDoor.rotation);
            else if (spawn == 7) Instantiate(Enemy_WeirdMushroom, mushroomDoor.position, mushroomDoor.rotation);

            ++minionCount;

            if (minionCount == 2+GameManager.currentLevel)
            {
                summonCooldown = 20f;
                minionCount = 0;
            }
            else
            {
                summonCooldown = 12-GameManager.currentLevel;
            }

        }
        else
            summonCooldown -= 0.1f;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        //Debug.Log(currentHealth);
        
        if(currentHealth <= 0 )
        {
            StartCoroutine(nextLevel());
        }
    }

    void OnGUI()
    {
        if(currentHealth>0)
            GUI.DrawTexture(new Rect(left, top, (300 * percentOfHp), 15), HpBarTexture);
    }

    IEnumerator nextLevel()
    {
        Debug.Log("masuk nextLevel");
       
        MenuManager.menuID = 2; //victory Panel
        MenuManager.lastLevel = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetFloat("lastStageTime", GameManager.currentTime);

        switch(GameManager.currentLevel)
        {
            case 1:
                if (PlayerPrefs.GetFloat("highScore1") == 0)
                    PlayerPrefs.SetFloat("highScore1", PlayerPrefs.GetFloat("lastStageTime"));
                break;
            case 2:
                if (PlayerPrefs.GetFloat("highScore2") == 0)
                    PlayerPrefs.SetFloat("highScore2", PlayerPrefs.GetFloat("lastStageTime"));
                break;
            case 3:
                if (PlayerPrefs.GetFloat("highScore3") == 0)
                    PlayerPrefs.SetFloat("highScore3", PlayerPrefs.GetFloat("lastStageTime"));
                break;
            case 4:
                if (PlayerPrefs.GetFloat("highScore4") == 0)
                    PlayerPrefs.SetFloat("highScore4", PlayerPrefs.GetFloat("lastStageTime"));
                break;
            case 5:
                if (PlayerPrefs.GetFloat("highScore5") == 0)
                    PlayerPrefs.SetFloat("highScore5", PlayerPrefs.GetFloat("lastStageTime"));
                break;

        }
        yield return new WaitForSeconds(3f);
        gameManager.GetComponent<GameManager>().Reset();
        Destroy(gameObject);
        Application.LoadLevel("Main Menu");
        
    }
}
