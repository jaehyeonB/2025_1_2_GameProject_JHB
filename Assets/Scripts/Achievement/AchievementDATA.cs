using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement" , menuName = "Achievement/Achievement Data")]
public class AchievementDATA : MonoBehaviour
{
    public string achievementName;
    public string description;
    public AchievementType achievementType;
    public int requiredAmount;
    public int rewardCoins;
    public bool isUnlocked;
    public Sprite icon;
}
