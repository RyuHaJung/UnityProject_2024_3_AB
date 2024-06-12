using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    public List<Achievement> achievements;
    
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddProgress(string achievementName, int amount)
    {
        Achievement achievement = achievements.Find(a => a.name == achievementName);
        if (achievement != null) { achievement.AddProgress(amount); };
    }
}
