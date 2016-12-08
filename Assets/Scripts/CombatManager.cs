using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour {

    public static CombatManager instance;

    public List<CharacterStats> playerStats;
    public List<CharacterStats> enemyStats;

    void Start()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Action();
        }
    }

    void Action()
    {
        //enemyStats[0].hp -= playerStats[0].atk;
    }
}
