using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpowner : MonoBehaviour
{
    private List<Vector3> positions = new List<Vector3>();
    public GameObject[] itemPrefabs = new GameObject[4];
    public float respawnTime = 30f;
    void Start()
    {
        positions.Add(new Vector3(130.449997f, 0.219999999f + 0.35f, 60.7999992f));
        positions.Add(new Vector3(133.529999f, 0.219999999f + 0.35f, 60.7999992f));
        positions.Add(new Vector3(137.869995f, 0.219999999f + 0.35f, 60.7999992f));
        positions.Add(new Vector3(142, 0.219999999f + 0.35f, 60.7999992f));
        positions.Add(new Vector3(174.929993f, 0.219999999f + 0.35f, 43.75f));
        positions.Add(new Vector3(174.929993f, 0.219999999f + 0.35f, 52.6800003f));
        positions.Add(new Vector3(158.289993f, 0.219999999f + 0.35f, 51.7099991f));
        positions.Add(new Vector3(158.289993f, 0.219999999f + 0.35f, 59.9099998f));
        positions.Add(new Vector3(204, 1.07000005f + 0.35f, 39.9700012f));
        positions.Add(new Vector3(204, 1.07000005f + 0.35f, 20.0300007f));
        positions.Add(new Vector3(227.5f, 1.07000005f + 0.35f, 37.2000008f));
        positions.Add(new Vector3(262.299988f, 1.07000005f + 0.35f, 57.9000015f));
        positions.Add(new Vector3(277.200012f, 1.07000005f + 0.35f, 91));
        positions.Add(new Vector3(238.899994f, 1.07000005f + 0.35f, 113.199997f));
        positions.Add(new Vector3(217.600006f, 1.07000005f + 0.35f, 131.300003f));
        positions.Add(new Vector3(161.800003f, 0.899999976f + 0.35f, 131.300003f));
        positions.Add(new Vector3(143.690002f, 0.899999976f + 0.35f, 131.300003f));
        positions.Add(new Vector3(143.690002f, 0.899999976f + 0.35f, 113.510002f));
        positions.Add(new Vector3(158.600006f, 0.899999976f + 0.35f, 113.510002f));
        positions.Add(new Vector3(109.760002f, 0.899999976f + 0.35f, 113.510002f));
        positions.Add(new Vector3(109.760002f, 0.899999976f + 0.35f, 148.399994f));
        positions.Add(new Vector3(144.199997f, 0.899999976f + 0.35f, 155));
        positions.Add(new Vector3(150.300003f, 0.899999976f + 0.35f, 133.5f));

        SpawnItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SpawnItems()
    {
        foreach (GameObject itemPrefab in itemPrefabs)
        {
            for(int i = 0;i < 6;i++)
            {
            int randomIndex = Random.Range(0, positions.Count);
            Vector3 spawnPosition = positions[randomIndex];
            GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
            positions.RemoveAt(randomIndex);
            }
        }
    }
}
