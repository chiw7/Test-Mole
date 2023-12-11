using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] float showMonsterIntervalSeconds = 2;
    [SerializeField] float countDownShowMonsterSeconds;
    int MAX_MONSTERS_ON_SCREEN = 3;

    public List<Monster> monsters;
    Text score;
    int scoreNumber = 0;
    public void HideMonster(GameObject monster)
    {
        monster.SetActive(false);
    }
    void ShowMonster(GameObject monster)
    {
        monster.SetActive(true);
    }

    public void AddScore()
    {
        scoreNumber += 10;
        score.text = scoreNumber.ToString();
    }


    void Start()
    {
        InitScore();
        InitMonsterList();
        HideAllMonsters();
        //ShowRandomMonster();
        ResetShowMonsterSeconds();
    }

    private void ResetShowMonsterSeconds()
    {
        countDownShowMonsterSeconds = showMonsterIntervalSeconds;
    }

    void HideAllMonsters()
    {
        foreach (var m in monsters)
        {
            HideMonster(m.gameObject);
        }
    }
    List<Monster> HiddenMonsters
    {
        get { 
            var result = new List<Monster>();//建立暫時性清單
            foreach (var m in monsters)
            {
                if (!m.IsActive)
                {
                    result.Add(m);
                }
            }
            return result;
        }
    }

    int MonsterCountOnScreen
    {
        get
        {
            int result = 0;
            foreach (var m in monsters)
            {
                if (m.IsActive)
                {
                    result += 1;
                }
            }
            return result;
        }
    }


    void ShowRandomMonster()
    {
        int r = Random.Range(0, HiddenMonsters.Count);
        Monster m = HiddenMonsters[r];
        ShowMonster(m.gameObject);
    }

    private void InitMonsterList()
    {
        monsters = GameObject.FindObjectsOfType<Monster>().ToList();
    }

    private void InitScore()
    {
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        score.text = "";
    }

    void FixedUpdate()
    {
        TryCountDownToShowMonster();
    }
    bool CountDownShowMonsterTimeUp => countDownShowMonsterSeconds <= 0;
    bool MonstersOnScreenAreFull => MonsterCountOnScreen >= MAX_MONSTERS_ON_SCREEN;
    private void TryCountDownToShowMonster()
    {
        countDownShowMonsterSeconds -= Time.fixedDeltaTime;
        if (CountDownShowMonsterTimeUp)
        {
            ResetShowMonsterSeconds();
            if (!MonstersOnScreenAreFull)
            {
                ShowRandomMonster();
            }
        }
    }
}
