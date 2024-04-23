using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{

    public Transform tornadoCenter;
    public float pullforce;
    public float refreshRate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "OJB")
        {
            StartCoroutine(pullObject(other, true));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "OJB")
        {
            StartCoroutine(pullObject(other, false));
        }
    }
    IEnumerator pullObject(Collider x, bool shouldPull)
    {
        if (shouldPull)
        {
            Vector3 ForeDir = tornadoCenter.position - x.transform.position;
            x.GetComponent<Rigidbody>().AddForce(ForeDir.normalized * pullforce * Time.deltaTime);
            yield return refreshRate;
            StartCoroutine(pullObject(x, shouldPull));

        }
    }

    void Start()
    {
        StartCoroutine(DestroyAfterDelay(5f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
