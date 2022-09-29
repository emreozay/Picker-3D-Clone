using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Levels/New Level")]

public class Level : ScriptableObject
{
    public GameObject levelBase;
    public GameObject collectableObject;

    public CollectableObjectType collectableObjectType;
    public CollectableObjectShape collectableObjectShape;
}
