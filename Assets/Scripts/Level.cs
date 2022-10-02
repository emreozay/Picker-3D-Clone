using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Levels/New Level")]

public class Level : ScriptableObject
{
    public LevelStage firstStage;

    public LevelStage secondStage;

    public LevelStage finalStage;
}