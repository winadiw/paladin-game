using UnityEngine;
using System.Collections;

public class PlayerCastle : MonoBehaviour
{
    private int currentHealth;
    private int maxHealth = 500;
    private int minionCount = 0;
    private float summonCooldown = 0f;
    private bool summon = true;
    /*
    public GameObject Minion_Dragon;
    public GameObject Minion_Golem;
    public GameObject Minion_Log;
    public GameObject Minion_Skeleton;
    public GameObject Minion_Snake;
    public GameObject Minion_Watermelon;
    public GameObject Minion_WeirdMushroom;
    public Transform dragonDoor;
    public Transform golemDoor;
    public Transform logDoor;
    public Transform skeletonDoor;
    public Transform snakeDoor;
    public Transform watermelonDoor;
    public Transform mushroomDoor;
    */

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
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (summon)
        {
            summonMinion();
        }

        percentOfHp = (float)currentHealth / (float)maxHealth;
        objectScreen = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.0f, 0.9f, 0.0f));

        left = objectScreen.x - (healthBarWidth / 1) - 50;
        top = objectScreen.y + (healthBarHeight / 2) - 400;

    }

    public void summonMinion()
    {
        /*
        if (summonCooldown <= 0)
        {
            
            int spawn = Random.Range(1, 8);
            if (spawn == 1) Instantiate(Minion_Dragon, dragonDoor.position, dragonDoor.rotation);
            else if (spawn == 2) Instantiate(Minion_Golem, golemDoor.position, golemDoor.rotation);
            else if (spawn == 3) Instantiate(Minion_Log, logDoor.position, logDoor.rotation);
            else if (spawn == 4) Instantiate(Minion_Skeleton, skeletonDoor.position, skeletonDoor.rotation);
            else if (spawn == 5) Instantiate(Minion_Snake, snakeDoor.position, snakeDoor.rotation);
            else if (spawn == 6) Instantiate(Minion_Watermelon, watermelonDoor.position, watermelonDoor.rotation);
            else if (spawn == 7) Instantiate(Minion_WeirdMushroom, mushroomDoor.position, mushroomDoor.rotation);

        ++minionCount;

            if (minionCount == 2)
            {
                summonCooldown = 30f;
                minionCount = 0;
            }
            else
            {
                summonCooldown = 5f;
            }

        }
        else
            summonCooldown -= 0.1f;
        */
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Lose();
            Destroy(gameObject);
        }
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(left, top, (300 * percentOfHp), 15), HpBarTexture);
    }

    public void Lose()
    {
        MenuManager.menuID = 1; //Dead Panel
        gameManager.GetComponent<GameManager>().Reset();
        Application.LoadLevel("Main Menu");
    }
}
