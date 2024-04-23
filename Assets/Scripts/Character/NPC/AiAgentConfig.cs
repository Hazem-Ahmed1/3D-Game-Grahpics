using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    [SerializeField] public float maxTime = 1.0f;
    [SerializeField] public float maxDistance = 1.0f;
}
