
using UnityEngine;

public static class LevelProgress
{
    public const int MaxLevels = 3;

    private const string ProgressKey = "HighestUnlockedLevel";

    public static int HighestUnlockedLevel
    {
        get
        {
            return Mathf.Clamp(
                PlayerPrefs.GetInt(ProgressKey, 1),
                1,
                MaxLevels
            );
        }
    }

    public static bool IsUnlocked(int level)
    {
        return level >= 1 &&
               level <= MaxLevels &&
               level <= HighestUnlockedLevel;
    }

    public static void UnlockNextLevel(int completedLevel)
    {
        if (completedLevel < 1 || completedLevel > MaxLevels)
            return;

        int nextLevel = Mathf.Min(
            completedLevel + 1,
            MaxLevels
        );

        if (nextLevel > HighestUnlockedLevel)
        {
            PlayerPrefs.SetInt(ProgressKey, nextLevel);
            PlayerPrefs.Save();
        }
    }
}
