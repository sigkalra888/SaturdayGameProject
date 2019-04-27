using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileGenerator : MonoBehaviour {

    [MenuItem("Tile/Generato")]
    static void TileGenerato()
    {
        int x;
        int y;
        GameObject tilepre = Resources.Load<GameObject>("Prefabs/Tile");
        GameObject tilespre = Resources.Load<GameObject>("Prefabs/Tiles");
        for (x = 0; x < 10; x++)
        {
            for (y = 0; y < 21; y++)
            {
                GameObject tiles = Instantiate(tilepre, new Vector3(x - 5, y - 2, 1), Quaternion.identity);
            }
        }
    }


}
