using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create BaseParameter", fileName = "BaseParam")]
public class Bparam : ScriptableObject
{
    public int myIndex;
    [SerializeField]
    public int[,] blockDPos = new int[4, 4];
    [SerializeField]
    public int[,] blockLPos = new int[4, 4];
    [SerializeField]
    public int[,] blockRPos = new int[4, 4];
    [SerializeField]
    public int[,] blockUPos = new int[4, 4];
}
