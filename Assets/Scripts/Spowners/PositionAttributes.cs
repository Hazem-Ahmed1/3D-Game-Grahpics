using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Position Attributes", menuName = "Positions")]
public class PositionAttributes : ScriptableObject
{
    public List<Vector3> KeyPositions = new List<Vector3>();
    public List<Vector3> ItemsPositions = new List<Vector3>();
    public List<Vector3> OpenTreasure = new List<Vector3>();
    public List<Vector3> CloseTreasure = new List<Vector3>();

}
