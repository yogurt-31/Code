using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct MapPos
{
    public Vector3 centerPos;
    public Vector3 rotationPos;
    public Vector3 createSize;
}

[Serializable]
public struct PrefabSetting
{
    public GameObject createPrefab;
    public float minSize, maxSize;
    public float createPercent;
}
public class DebugMapCreate : MonoBehaviour
{
    [SerializeField] private List<PrefabSetting> createSettings;
    [SerializeField] private List<MapPos> createPos;
    [SerializeField] private int createCount = 50;
    float sum = 0;

    private void Start()
    {
        foreach(PrefabSetting setting in createSettings)
        {
            sum += setting.createPercent;
        }

        for (int i = 0; i < createSettings.Count; ++i)
        {
            SelectPrefab(createSettings[i]);
        }
    }

    private void SelectPrefab(PrefabSetting prefabSetting)
    {
        int prefabCount = (int)((prefabSetting.createPercent / sum) * this.createCount);

        for (int i = 0; i < prefabCount; ++i)
        {
            int createPosition = Random.Range(0, createPos.Count);

            GameObject createObject = Instantiate(prefabSetting.createPrefab, transform);

            createObject.transform.position
                = PositionSetting(createPosition);
            createObject.transform.rotation
                = RotationSetting();
            createObject.transform.localScale
                = SizeSetting(prefabSetting.minSize, prefabSetting.maxSize);
        }
    }

    private Vector3 PositionSetting(int createPosition)
    {
        float createXPos = createPos[createPosition].createSize.x * 0.5f;
        float createZPos = createPos[createPosition].createSize.z * 0.5f;
        Vector3 centerPos = createPos[createPosition].centerPos;

        float xPos = Random.Range(-createXPos, createXPos);
        float zPos = Random.Range(-createZPos, createZPos);

        xPos = float.Parse(xPos.ToString("N1"));
        zPos = float.Parse(zPos.ToString("N1"));

        return new Vector3(xPos, 0, zPos) + centerPos;

    }
    private Quaternion RotationSetting()
    {
        return Quaternion.Euler(-90, Random.Range(0, 360), 0);
    }

    private Vector3 SizeSetting(float minSize, float maxSize)
    {
        float createSize = Random.Range(minSize, maxSize);

        createSize = float.Parse(createSize.ToString("N1"));

        return new Vector3(createSize, createSize, createSize);
    }

    private void OnDrawGizmos()
    {
        if (createPos == null) return;
        for (int i = 0; i < createPos.Count; ++i)
        {
            Gizmos.matrix = Matrix4x4.TRS(createPos[i].centerPos, Quaternion.Euler(createPos[i].rotationPos), Vector3.one);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, createPos[i].createSize);
            Gizmos.matrix = Matrix4x4.identity;
        }
    }
}
