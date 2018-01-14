using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillCoolDown : MonoBehaviour
{
    public List<Skill> skills;

    public GameObject Player;
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

    void Start()
    {
        foreach (Skill s in skills)
        {
            if (s.currentCoolDown < s.cooldown)
            {
                s.currentCoolDown = s.cooldown;
                s.skillIcon.fillAmount = s.currentCoolDown / (s.cooldown);
            }
        }
    }
    void Update()
    {
        if (GameManager.paused == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (skills[3].currentCoolDown >= skills[3].cooldown)
                {
                    //Monster 1 Dragon
                    Instantiate(Minion_Dragon, dragonDoor.position, dragonDoor.rotation);
                    skills[3].currentCoolDown = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                if (skills[4].currentCoolDown >= skills[4].cooldown)
                {
                    //Monster 2 Golem
                    Instantiate(Minion_Golem, golemDoor.position, golemDoor.rotation);
                    skills[4].currentCoolDown = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                if (skills[5].currentCoolDown >= skills[5].cooldown)
                {
                    //Monster 3 Log
                    Instantiate(Minion_Log, logDoor.position, logDoor.rotation);
                    skills[5].currentCoolDown = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                if (skills[6].currentCoolDown >= skills[6].cooldown)
                {
                    //Monster 4 Skeleton
                    Instantiate(Minion_Skeleton, skeletonDoor.position, skeletonDoor.rotation);
                    skills[6].currentCoolDown = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                if (skills[7].currentCoolDown >= skills[7].cooldown)
                {
                    //Monster 5 Snake
                    Instantiate(Minion_Snake, snakeDoor.position, snakeDoor.rotation);
                    skills[7].currentCoolDown = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                if (skills[8].currentCoolDown >= skills[8].cooldown)
                {
                    //Monster 6 Watermelon
                    Instantiate(Minion_Watermelon, watermelonDoor.position, watermelonDoor.rotation);
                    skills[8].currentCoolDown = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                if (skills[9].currentCoolDown >= skills[9].cooldown)
                {
                    //Monster 7 Mushroom
                    Instantiate(Minion_WeirdMushroom, mushroomDoor.position, mushroomDoor.rotation);
                    skills[9].currentCoolDown = 0;
                }
            }
            foreach (Skill s in skills)
            {
                if (s.currentCoolDown < s.cooldown)
                {
                    s.currentCoolDown += Time.deltaTime;
                    s.skillIcon.fillAmount = s.currentCoolDown / s.cooldown;
                }
            }
        }
    }
    /*
    void Update()
    {
        foreach(Skill s in skills)
        {
            if(s.currentCoolDown < s.cooldown)
            {
                s.currentCoolDown += Time.deltaTime;
                s.skillIcon.fillAmount = s.currentCoolDown / s.cooldown;
            }
        }
    }
    */

    public float getCurrentCooldown(int i)
    {
        return skills[i].currentCoolDown;
    }
    public float getCooldown(int i)
    {
        return skills[i].cooldown;
    }
    public void setCurrentCooldownZero(int i)
    {
        skills[i].currentCoolDown = 0;
    }

}
[System.Serializable]
public class Skill
{
    public float cooldown;
    public Image skillIcon;
    [HideInInspector]
    public float currentCoolDown;
}
