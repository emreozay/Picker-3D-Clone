using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject baseLevel;
    [SerializeField] private Transform collectibleObjectParent;
    private Level level;
    private List<GameObject> collectableObjectPrefabs;
    private List<GameObject> containers;
    private List<Level> levels;

    private Vector3 basePosition;

    void Start()
    {
        GetCollectableObjects();
        GetLevels();

        //SetContainerText();
        //Instantiate(levelBase);

        //InstantiateCollectibleObjects();
    }

    private void SetContainerText()
    {
        containers = new List<GameObject>();

        for (int i = 0; i < 3; i++)
        {
            Transform tempContainer = baseLevel.transform.GetChild(i).Find("Container");

            if (tempContainer != null)
                containers.Add(tempContainer.gameObject);
        }

        for (int i = 0; i < containers.Count; i++)
        {
            TextMeshPro textMesh = containers[i].GetComponentInChildren<TextMeshPro>();
            
            if(textMesh != null)
            {
                if (i == 0)
                    textMesh.text = "0 / " + level.firstStage.objectAmount;
                else if (i == 1)
                    textMesh.text = "0 / " + level.secondStage.objectAmount;
                else if (i == 2)
                    textMesh.text = "0 / " + level.finalStage.objectAmount;
            }
        }
    }

    private void InstantiateCollectibleObjects()
    {
        for (int i = 0; i < level.firstStage.collectableObject.Length; i++)
        {
            string type = level.firstStage.collectableObject[i].type.ToString()[0] + level.firstStage.collectableObject[i].type.ToString().Substring(1).ToLowerInvariant();
            string shape = level.firstStage.collectableObject[i].shape.ToString()[0] + level.firstStage.collectableObject[i].shape.ToString().Substring(1).ToLowerInvariant();

            string objectName = type + shape;

            GameObject temp = collectableObjectPrefabs.Where(obj => obj.name == objectName).SingleOrDefault();
            Instantiate(temp, basePosition + level.firstStage.collectableObject[i].position, Quaternion.identity, collectibleObjectParent);
        }

        for (int i = 0; i < level.secondStage.collectableObject.Length; i++)
        {
            string type = level.secondStage.collectableObject[i].type.ToString()[0] + level.secondStage.collectableObject[i].type.ToString().Substring(1).ToLowerInvariant();
            string shape = level.secondStage.collectableObject[i].shape.ToString()[0] + level.secondStage.collectableObject[i].shape.ToString().Substring(1).ToLowerInvariant();

            string objectName = type + shape;

            GameObject temp = collectableObjectPrefabs.Where(obj => obj.name == objectName).SingleOrDefault();
            Instantiate(temp, basePosition + level.secondStage.collectableObject[i].position, Quaternion.identity, collectibleObjectParent);
        }

        for (int i = 0; i < level.finalStage.collectableObject.Length; i++)
        {
            string type = level.finalStage.collectableObject[i].type.ToString()[0] + level.finalStage.collectableObject[i].type.ToString().Substring(1).ToLowerInvariant();
            string shape = level.finalStage.collectableObject[i].shape.ToString()[0] + level.finalStage.collectableObject[i].shape.ToString().Substring(1).ToLowerInvariant();

            string objectName = type + shape;

            GameObject temp = collectableObjectPrefabs.Where(obj => obj.name == objectName).SingleOrDefault();
            Instantiate(temp, basePosition + level.finalStage.collectableObject[i].position, Quaternion.identity, collectibleObjectParent);
        }
    }

    private void GetCollectableObjects()
    {
        collectableObjectPrefabs = new List<GameObject>();

        collectableObjectPrefabs = Resources.LoadAll<GameObject>("CollectableObjects").ToList();
    }

    private void GetLevels()
    {
        levels = new List<Level>();

        levels = Resources.LoadAll<Level>("Levels").ToList();

        for (int i = 0; i < levels.Count; i++)
        {
            level = levels[i];

            basePosition = new Vector3(0, 0, 158.4f * i);
            
            
            SetContainerText();

            Instantiate(baseLevel, basePosition, Quaternion.identity);
            InstantiateCollectibleObjects();
        }
    }
}

[System.Serializable]
public class CollectableObject
{
    public CollectableObjectType type;
    public CollectableObjectShape shape;
    public Vector3 position;
}

[System.Serializable]
public class LevelStage
{
    public int objectAmount;
    public CollectableObject[] collectableObject;
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