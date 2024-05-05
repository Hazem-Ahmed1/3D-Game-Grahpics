using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoSpawnerTreasure : MonoBehaviour
{
    public GameObject openTreasure;
    public GameObject closeTreasure;
    public GameObject flag;
    private List<Vector3> open = new List<Vector3>();
    private List<Vector3> close = new List<Vector3>();


    void Start()
    {

        close.Add(new Vector3(101f,7.03800011f,31.1100006f));
        close.Add(new Vector3(11.8699999f,7.08099985f,19.9099998f));
        close.Add(new Vector3(36.7579994f,6.61999989f,92.1920013f));

        // close.Add(new Vector3(111.330002f,1.62f,35.5099983f));
        // close.Add(new Vector3(111.330002f,1.62f,67.0199966f));
        // close.Add(new Vector3(162.830002f,1.62f,94.8700027f));
        // close.Add(new Vector3(182.940002f,1.62f,94.8700027f));
        // close.Add(new Vector3(189.539993f,1.62f,66.2300034f));
        // close.Add(new Vector3(161.690002f,1.62f,42.7599983f));
        // close.Add(new Vector3(178.759995f,1.62f,37.3199997f));
        // close.Add(new Vector3(184.860001f,1.62f,10.6400003f));
        // close.Add(new Vector3(168.100006f,1.62f,10.6400003f));

        open.Add(new Vector3(55.3400002f,6.48099995f,96.8700027f));
        open.Add(new Vector3(49.2089996f,6.61999989f,46.6699982f));
        open.Add(new Vector3(51.1860008f,6.44000006f,6.84299994f));
        // open.Add(new Vector3(254.759995f,1.38f,13.29f));
        // open.Add(new Vector3(273.929993f,1.38f,51.0499992f));
        // open.Add(new Vector3(280.049988f,1.38f,89.25f));
        // open.Add(new Vector3(244.669998f,1.38f,125.699997f));
        // open.Add(new Vector3(133.679993f,1.38f,139.130005f));
        // open.Add(new Vector3(121.68f,1.38f,118.169998f));
        // open.Add(new Vector3(187,1.38f,125.699997f));

        SetupTreasureAndFlag();

    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 GetRandomOpen()
    {
        int randomIndex = Random.Range(0, open.Count);
        return open[randomIndex];
    }

    Vector3 GetRandomClose()
    {
        int randomIndex = Random.Range(0, open.Count);
        return close[randomIndex];
    }

    public void SetupTreasureAndFlag()
    {
        int chooseGoal = Random.Range(-2, 2);

        if (chooseGoal >= 0)
        {
            Vector3 randomVector = GetRandomClose();
            GameObject instantiatedTreasure = Instantiate(closeTreasure, randomVector, Quaternion.identity);
            float heightAboveTreasure = 20.0f;
            flag.transform.position = new Vector3(instantiatedTreasure.transform.position.x,
                                          instantiatedTreasure.transform.position.y + heightAboveTreasure,
                                          instantiatedTreasure.transform.position.z);
            flag.SetActive(true);
        }
        else if (chooseGoal < 0)
        {
            Vector3 randomVector = GetRandomOpen();
            GameObject instantiatedTreasure = Instantiate(openTreasure, randomVector, Quaternion.identity);
            float heightAboveTreasure = 20.0f;
            flag.transform.position = new Vector3(instantiatedTreasure.transform.position.x,
                                          instantiatedTreasure.transform.position.y + heightAboveTreasure,
                                          instantiatedTreasure.transform.position.z);
            flag.SetActive(true);
        }
    }
}
