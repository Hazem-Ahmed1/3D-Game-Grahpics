using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keySpowner : MonoBehaviour
{
    private List<Vector3> vectors = new List<Vector3>();
    public GameObject key;
    void Start()
    {
        vectors.Add(new Vector3(115.5f, 0.76f, 57.2f));
        vectors.Add(new Vector3(115.5f, 0.76f, 88.05f));
        vectors.Add(new Vector3(133.3f, 0.76f, 145.1f));
        vectors.Add(new Vector3(169.8f, 0.76f, 136.4f));
        vectors.Add(new Vector3(223.6f, 2.03f, 136.4f));
        vectors.Add(new Vector3(249.36f, 2.03f, 136.4f));
        vectors.Add(new Vector3(229.4f, 2.03f, 60.9f));
        vectors.Add(new Vector3(280.9f, 2.03f, 60.9f));
        vectors.Add(new Vector3(178.1f, 0.86f, 77.6f));

        Vector3 randomVector = GetRandomVector();

        Instantiate(key,randomVector,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 GetRandomVector()
    {
        int randomIndex = Random.Range(0, vectors.Count);
        return vectors[randomIndex];
    }
}
