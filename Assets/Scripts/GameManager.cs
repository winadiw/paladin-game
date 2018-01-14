using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    private int mana;
    public int maximumMana;
    public int minimumMana;
    public static float currentMana;
    public int startingMana;
    public float regenRate;
    private float regenElapsed;
    public float regenDelay;
    private float elapsed;
    public static float currentTime;
    public static bool paused = false;
    public static int currentLevel = 1;

    public GameObject pauseText;

    //Variable untuk buat texture di atas
    public Texture2D HpBarTexture;
    public Texture2D ManaBarTexture;
    float hpBarLength;
    float percentOfHp;
    float manaBarLength;
    float percentOfMana;

    public void Reset()
    {
        regenRate = 3.0f;
        startingMana = 75;
        maximumMana = 300;
        minimumMana = 0;
        elapsed = 0.0f;
        regenDelay = 0.0f;
        currentTime = 0;
    }
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log(currentLevel);
        currentMana = startingMana;
    }

    void Update()
    {
        if (elapsed > regenDelay)
        {
            currentMana = Mathf.Min(currentMana + regenRate * Time.deltaTime, maximumMana);
        }

        elapsed += Time.deltaTime;

        currentTime += Time.deltaTime;
        //Debug.Log(currentTime);

        percentOfHp = Player.currentHealth / Player.maxHealth;
        hpBarLength = percentOfHp * 100;

        percentOfMana = currentMana / maximumMana;
        manaBarLength = percentOfMana * 100;

        //pauseText.text = "PAUSED";
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused == false)
            {
                Time.timeScale = 0;
                GameManager.paused = true;
                pauseText.GetComponent<Text>().enabled = true;
                PauseManager.pauseID = 1;
            }
            else
            {
                pauseText.GetComponent<Text>().enabled = false;
                GameManager.paused = false;
                PauseManager.pauseID = 0;
                Time.timeScale = 1;
            }
        }
        
    }

    void ConsumeMana(int amount)
    {
        currentMana = Mathf.Max(currentMana - amount, minimumMana);
        elapsed = 0;
    }

    void AddMana(int amount)
    {
        currentMana = Mathf.Min(currentMana + amount, maximumMana);
    }

    void SetMana(int amount)
    {
        currentMana = Mathf.Clamp(mana, minimumMana, maximumMana);
    }

    float GetMana()
    {
        return currentMana;
    }

    void OnGUI()
    {
          if (Player.currentHealth > 0)
          {
            GUI.DrawTexture(new Rect((Screen.width / 2) - 100, 10, hpBarLength,25), HpBarTexture);
          }
          if (currentMana > 0)
          {
            GUI.DrawTexture(new Rect((Screen.width / 2) - 100, 35, manaBarLength, 25), ManaBarTexture);
          }
    }

    public static void useSkill(float mana)
    {
        currentMana -= mana;
    }
}

