using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoKeySpawner : MonoBehaviour
{
    private List<Vector3> KeyVector = new List<Vector3>();
    public GameObject key;
    public GameObject flag;
    void Start()
    {
        KeyVector.Add(new Vector3(73.8648529f,5.73000002f,131.869995f));
        KeyVector.Add(new Vector3(120.699997f,5.73000002f,43.9343262f));
        KeyVector.Add(new Vector3(123.599998f,5.73000002f,108.400002f));
        KeyVector.Add(new Vector3(73.8648529f,5.73000002f,8.30000019f));
        KeyVector.Add(new Vector3(9.60000038f,5.73000002f,67.0999985f));
        KeyVector.Add(new Vector3(20.2999992f,5.73000002f,99.0999985f));
        KeyVector.Add(new Vector3(171.050003f,5.73000002f,21.8700008f));
        KeyVector.Add(new Vector3(190.119995f,6.32999992f,109.300003f));
        KeyVector.Add(new Vector3(191.600006f,6.46000004f,40.2000008f));
        KeyVector.Add(new Vector3(113.099998f,6.03299999f,79.3000031f));
        KeyVector.Add(new Vector3(58.0999985f,6.05600023f,109.300003f));
        KeyVector.Add(new Vector3(161.199997f,6.01300001f,131.699997f));
        KeyVector.Add(new Vector3(142.399994f,5.96199989f,153.100006f));
        KeyVector.Add(new Vector3(69.1999969f,5.90199995f,121.699997f));
        KeyVector.Add(new Vector3(10.8000002f,6.14599991f,46.5f));
        KeyVector.Add(new Vector3(165.110001f,5.96099997f,15f));
        // KeyVector.Add(new Vector3(115.5f, 0.76f, 88.05f));
        // KeyVector.Add(new Vector3(133.3f, 0.76f, 145.1f));
        // KeyVector.Add(new Vector3(169.8f, 0.76f, 136.4f));
        // KeyVector.Add(new Vector3(223.6f, 2.03f, 136.4f));
        // KeyVector.Add(new Vector3(249.36f, 2.03f, 136.4f));
        // KeyVector.Add(new Vector3(229.4f, 2.03f, 60.9f));
        // KeyVector.Add(new Vector3(280.9f, 2.03f, 60.9f));
        // KeyVector.Add(new Vector3(178.1f, 0.86f, 77.6f));

        Vector3 randomVector = GetRandomVector();

        SetupKeyAndFlag();

    }
    public void SetupKeyAndFlag()
    {
        Vector3 randomVector = GetRandomVector();
        GameObject instantiatedTreasure = Instantiate(key, randomVector, Quaternion.identity);
        float heightAboveTreasure = 15.0f;
        flag.transform.position = new Vector3(instantiatedTreasure.transform.position.x,
                                        instantiatedTreasure.transform.position.y + heightAboveTreasure,
                                        instantiatedTreasure.transform.position.z);
        flag.SetActive(true);
    }

    Vector3 GetRandomVector()
    {
        int randomIndex = Random.Range(0, KeyVector.Count);
        return KeyVector[randomIndex];
    }
}
