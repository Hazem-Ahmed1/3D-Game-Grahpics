using UnityEngine;

public class Maketornado : MonoBehaviour
{
    public GameObject tornado;
    public void OnTriggerEnter(Collider other)
    {
        Instantiate(tornado , this.transform.position , Quaternion.identity);
        Destroy(this.gameObject);
    }
}

