using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum StoryType
    {
        MAIN,
        SUB,
        SERIAL
    }

    public enum EventType
    {
        NONE,
        GOTOBATTLE = 100,
        CheckSTR = 1000,
    }

    public enum ResultType
    {
        AddExperience,
        GoToNextStory,
        GoToRandoemStory
    }

    
}
[System.Serializable]
public class Stats
{
    public int hpPoint;
    public int spPoint;

    public int currentHpPoint;
    public int currentSpPoint;
    public int currentXpPoint;

    public int strength;
    public int dexterity;
    public int consitytion;
    public int Intelligence;
    public int wisdom;
    public int charisma;

}