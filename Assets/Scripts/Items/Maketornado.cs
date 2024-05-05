using UnityEngine;

public class Maketornado : MonoBehaviour
{
    public GameObject tornado;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OJB") || other.CompareTag("Player"))
        {

            Instantiate(tornado , this.transform.position , Quaternion.identity);
            // Destroy(tornado,5);
            // Destroy(this.gameObject);
        }
    }
}

