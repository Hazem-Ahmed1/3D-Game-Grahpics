using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoItemsSpawner : MonoBehaviour
{
    private List<Vector3> itempositions = new List<Vector3>();
    public GameObject[] itemPrefabs = new GameObject[4];
    void Start()
    {
        itempositions.Add(new Vector3(78.3600006f,5.88000011f,61f));
        itempositions.Add(new Vector3(129.5f,5.88000011f,48.5f));
        itempositions.Add(new Vector3(70.1319656f,5.88000011f,83.0999985f));
        itempositions.Add(new Vector3(84.8000031f,5.88000011f,6.70000076f));
        itempositions.Add(new Vector3(103.900002f,5.88000011f,86.5f));
        itempositions.Add(new Vector3(31.2000008f,5.88000011f,29.0200005f));
        itempositions.Add(new Vector3(10.9000015f,5.88000011f,69.4000015f));
        itempositions.Add(new Vector3(110.900002f,5.88000011f,113.900002f));
        itempositions.Add(new Vector3(114.699997f,5.88000011f,9.10000038f));
        itempositions.Add(new Vector3(82.4000015f,5.88000011f,17.2000008f));
        itempositions.Add(new Vector3(52.5999985f,5.88000011f,28.6000004f));
        itempositions.Add(new Vector3(19.3999996f,5.88000011f,40.2999992f));
        itempositions.Add(new Vector3(60.4000015f,5.88000011f,106.699997f));
        itempositions.Add(new Vector3(135.199997f,5.88000011f,50.0999985f));
        itempositions.Add(new Vector3(65.6999969f,5.88000011f,42.5f));
        itempositions.Add(new Vector3(66.1999969f,5.88000011f,99.5999985f));



        // itempositions.Add(new Vector3(133.529999f, 0.219999999f + 0.35f, 60.7999992f));
        // itempositions.Add(new Vector3(137.869995f, 0.219999999f + 0.35f, 60.7999992f));
        // itempositions.Add(new Vector3(142, 0.219999999f + 0.35f, 60.7999992f));
        // itempositions.Add(new Vector3(174.929993f, 0.219999999f + 0.35f, 43.75f));
        // itempositions.Add(new Vector3(174.929993f, 0.219999999f + 0.35f, 52.6800003f));
        // itempositions.Add(new Vector3(158.289993f, 0.219999999f + 0.35f, 51.7099991f));
        // itempositions.Add(new Vector3(158.289993f, 0.219999999f + 0.35f, 59.9099998f));
        // itempositions.Add(new Vector3(204, 1.07000005f + 0.35f, 39.9700012f));
        // itempositions.Add(new Vector3(204, 1.07000005f + 0.35f, 20.0300007f));
        // itempositions.Add(new Vector3(227.5f, 1.07000005f + 0.35f, 37.2000008f));
        // itempositions.Add(new Vector3(262.299988f, 1.07000005f + 0.35f, 57.9000015f));
        // itempositions.Add(new Vector3(277.200012f, 1.07000005f + 0.35f, 91));
        // itempositions.Add(new Vector3(238.899994f, 1.07000005f + 0.35f, 113.199997f));
        // itempositions.Add(new Vector3(217.600006f, 1.07000005f + 0.35f, 131.300003f));
        // itempositions.Add(new Vector3(161.800003f, 0.899999976f + 0.35f, 131.300003f));
        // itempositions.Add(new Vector3(143.690002f, 0.899999976f + 0.35f, 131.300003f));
        // itempositions.Add(new Vector3(143.690002f, 0.899999976f + 0.35f, 113.510002f));
        // itempositions.Add(new Vector3(158.600006f, 0.899999976f + 0.35f, 113.510002f));
        // itempositions.Add(new Vector3(109.760002f, 0.899999976f + 0.35f, 113.510002f));
        // itempositions.Add(new Vector3(109.760002f, 0.899999976f + 0.35f, 148.399994f));
        // itempositions.Add(new Vector3(144.199997f, 0.899999976f + 0.35f, 155));
        // itempositions.Add(new Vector3(150.300003f, 0.899999976f + 0.35f, 133.5f));
        // Change Places
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
            for (int i = 0; i < 6; i++)
            {
                if (itempositions.Count > 0)
                {
                    int randomIndex = Random.Range(0, itempositions.Count);
                    Vector3 spawnPosition = itempositions[randomIndex];
                    GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
                    itempositions.RemoveAt(randomIndex);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
