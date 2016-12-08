using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour {

    public struct Move
    {
        bool unique;
        bool special;
        int power;
        int manaUsage;
    }

    public enum Type : int { FYS, Arch, AL, Bus, Eng, Sci };

    public int hp, atk, def, spec, lck, mana;
    public Type type;

    public Move[] moveset = new Move[4];

    [SerializeField]
    private bool randomize;
    [SerializeField]
    private int randomLevel = 1;

    // enum for Majors, Dorms?

	void Start ()
    {
        if(randomize)
            GenerateRandom(randomLevel);
	}
	
	void Update ()
    {
	
	}
    
    void GenerateRandom(int level)
    {
        type = (Type)Random.Range(0, 6);

        //int hpMax, hpMin;

        if(type == Type.FYS)
        {
            
        }

        hp = Random.Range(5 * level, 10 * level);
        atk = Random.Range(5 * level, 10 * level);
        def = Random.Range(5 * level, 10 * level);
        spec = Random.Range(5 * level, 10 * level);
        lck = Random.Range(5 * level, 10 * level);
        mana = Random.Range(5 * level, 10 * level);
    }
}
