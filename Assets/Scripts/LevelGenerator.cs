using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Level level;
    private List<GameObject> collectableObjectPrefabs;

    void Start()
    {
        Instantiate(level.levelBase);
        
        Instantiate(level.levelBase, OffsetBetweenBases(), Quaternion.identity);
        Instantiate(level.levelBase, new Vector3(0, 0, OffsetBetweenBases().z * 2), Quaternion.identity);

        GetCollectableObjects();
        InstantiateCollectibleObjects();
    }

    private void InstantiateCollectibleObjects()
    {
        for (int i = 0; i < level.collectableObjects1.Length; i++)
        {
            string type = level.collectableObjects1[i].type.ToString()[0] + level.collectableObjects1[i].type.ToString().Substring(1).ToLowerInvariant();
            string shape = level.collectableObjects1[i].shape.ToString()[0] + level.collectableObjects1[i].shape.ToString().Substring(1).ToLowerInvariant();

            string objectName = type + shape;

            GameObject temp = collectableObjectPrefabs.Where(obj => obj.name == objectName).SingleOrDefault();
            Instantiate(temp, level.collectableObjects1[i].position, Quaternion.identity);
        }

        for (int i = 0; i < level.collectableObjects2.Length; i++)
        {
            string type = level.collectableObjects2[i].type.ToString()[0] + level.collectableObjects2[i].type.ToString().Substring(1).ToLowerInvariant();
            string shape = level.collectableObjects2[i].shape.ToString()[0] + level.collectableObjects2[i].shape.ToString().Substring(1).ToLowerInvariant();

            string objectName = type + shape;

            GameObject temp = collectableObjectPrefabs.Where(obj => obj.name == objectName).SingleOrDefault();
            Instantiate(temp, level.collectableObjects2[i].position, Quaternion.identity);
        }

        for (int i = 0; i < level.collectableObjects3.Length; i++)
        {
            string type = level.collectableObjects3[i].type.ToString()[0] + level.collectableObjects3[i].type.ToString().Substring(1).ToLowerInvariant();
            string shape = level.collectableObjects3[i].shape.ToString()[0] + level.collectableObjects3[i].shape.ToString().Substring(1).ToLowerInvariant();

            string objectName = type + shape;

            GameObject temp = collectableObjectPrefabs.Where(obj => obj.name == objectName).SingleOrDefault();
            Instantiate(temp, level.collectableObjects3[i].position, Quaternion.identity);
        }
    }

    private void GetCollectableObjects()
    {
        collectableObjectPrefabs = new List<GameObject>();

        collectableObjectPrefabs = Resources.LoadAll<GameObject>("CollectableObjects").ToList();
    }

    private Vector3 OffsetBetweenBases()
    {
        int childCount = level.levelBase.transform.GetChild(0).childCount + 1;

        Vector3 offset = level.levelBase.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().bounds.size * childCount;
        offset = new Vector3(0, 0, offset.z);
        
        return offset;
    }
}

[System.Serializable]
public class CollectableObject
{
    public CollectableObjectType type;
    public CollectableObjectShape shape;
    public Vector3 position;
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