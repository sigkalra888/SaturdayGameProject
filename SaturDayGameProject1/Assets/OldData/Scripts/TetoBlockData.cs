using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockParameter", fileName = "BParam")]
public class TetoBlockData : ScriptableObject
{
    [System.Serializable]
    public struct Pos
    {
        public int y;
        public int x;
    }

    public Pos[] posDef = new Pos[4];
    public Pos[] posRight = new Pos[4];
    public Pos[] posLeft = new Pos[4];
    public Pos[] posUnder = new Pos[4];

    public int myIndex;
}
