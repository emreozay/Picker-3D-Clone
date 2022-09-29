using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Level level;
    private List<GameObject> collectableObjects;

    void Start()
    {
        Instantiate(level.levelBase);
        
        Instantiate(level.levelBase, OffsetBetweenBases(), Quaternion.identity);
        
        GetCollectableObjects();
    }

    private void GetCollectableObjects()
    {
        collectableObjects = new List<GameObject>();

        collectableObjects = Resources.LoadAll<GameObject>("CollectableObjects").ToList();

        foreach (var item in collectableObjects)
        {
            if (item.name.ToLowerInvariant() == (level.collectableObjectType.ToString() + level.collectableObjectShape.ToString()).ToLowerInvariant())
            {
                Instantiate(item);
            }
        }
    }

    private Vector3 OffsetBetweenBases()
    {
        int childCount = level.levelBase.transform.GetChild(0).childCount + 1;

        Vector3 offset = level.levelBase.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().bounds.size * childCount;
        offset = new Vector3(0, 0, offset.z);
        
        return offset;
    }
}

public enum CollectableObjectType
{
    CUBE,
    SPHERE,
    CAPSULE
}

public enum CollectableObjectShape
{
    ARROW,
    TRIANGLE,
    RECTANGLE
}