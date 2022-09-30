using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Levels/New Level")]

public class Level : ScriptableObject
{
    public GameObject levelBase;

    [Header("First stage")]
    public CollectableObject[] collectableObjects1;

    [Header("Second stage")]
    public CollectableObject[] collectableObjects2;

    [Header("Final stage")]
    public CollectableObject[] collectableObjects3;
}