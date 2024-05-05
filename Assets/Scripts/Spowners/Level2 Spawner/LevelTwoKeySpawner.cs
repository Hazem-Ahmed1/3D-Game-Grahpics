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
