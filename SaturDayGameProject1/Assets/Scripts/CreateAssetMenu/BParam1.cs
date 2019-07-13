using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Create Bparam1", fileName = "Bparam1")]
public class BParam1 : ScriptableObject
{

    public int myIndex = 1;

    [SerializeField]
    public int[,] blockDPos = new int[4, 4]
    {
        {0,1,0,0},
        {1,1,1,0},
        {0,0,0,0},
        {0,0,0,0}
    };

    [SerializeField]
    public int[,] blockLPos = new int[4, 4] 
    {
        {0,1,0,0},
        {1,1,0,0},
        {0,1,0,0},
        {0,0,0,0}
    };

    [SerializeField]
    public int[,] blockRPos = new int[4, 4]
    {
        {0,1,0,0},
        {0,1,1,0},
        {0,1,0,0},
        {0,0,0,0}
    };

    [SerializeField]
    public int[,] blockUPos = new int[4, 4]
    {
        {0,0,0,0},
        {1,1,1,0},
        {0,1,0,0},
        {0,0,0,0}
    };

}
