// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;

// public class Tornado : MonoBehaviour
// {
//     // public Transform PlayerTransform;
//     public float currentMovementSpeed = 50.0f;
//     public Transform tornadoCenter;
//     public float pullforce;
//     public float refreshRate;
//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("OJB"))
//         {
//             StartCoroutine(pullObject(other, true));
//         }
//     }
//     private void OnTriggerExit(Collider other)
//     {
//         if (other.tag == "OJB")
//         {
//             StartCoroutine(pullObject(other, false));
//         }
//     }
//     IEnumerator pullObject(Collider x, bool shouldPull)
//     {
//         Debug.Log("Bedo");
//         if (shouldPull)
//         {
//             Debug.Log("Bedo");
//             Vector3 ForeDir = tornadoCenter.position - x.transform.position;
//             x.attachedRigidbody.AddForce(ForeDir.normalized * pullforce * Time.deltaTime);
//             yield return refreshRate;
//             StartCoroutine(pullObject(x, shouldPull));
//         }
//     }

//     // IEnumerator PullPlayer(Collider x, bool shouldPull)
//     // {
//     //     if (shouldPull)
//     //     {
//     //         // Generate a random direction vector
//     //         Vector3 randomDir = new Vector3(Random.Range(-10f, 10f), 0 , Random.Range(-10f, 10f));
//     //         // Normalize the random direction vector to ensure consistent speed
//     //         // randomDir = randomDir.normalized;
//     //         // Move the character controller in the random direction
//     //         // if (characterController != null && characterController.enabled)
//     //         // {

//     //             // characterController.GetComponent<Transform>()(randomDir * currentMovementSpeed * Time.deltaTime);
//     //             PlayerTransform.position =randomDir;
//     //         // }
//     //         yield return refreshRate;
//     //         StartCoroutine(PullPlayer(x, shouldPull));
//     //     }
//     // }

//     void Start()
//     {
//         // characterController = GetComponent<CharacterController>();
//         StartCoroutine(DestroyAfterDelay(5f));
//     }

//     IEnumerator DestroyAfterDelay(float delay)
//     {
//         yield return new WaitForSeconds(delay);
//         Destroy(gameObject);
//     }
// }