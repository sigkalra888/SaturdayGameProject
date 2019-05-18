using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tetoBlock;
    [SerializeField]
    private List<GameObject> stockBlocks;
    [SerializeField]
    private GameObject block;

    private float time;

    //ストックブロック生成の為の変数
    private int y = 17;
    private int count = 0;

    private bool blockfalling = true;

    void Start()
    {
        Invoke("FirstBlockGenerator", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        StockBlockGenerator();
    }

    private void FirstBlockGenerator()
    {
        int tetoNum = Random.Range(0, tetoBlock.Length);
        GameObject blockPre = Instantiate(tetoBlock[tetoNum], new Vector3(-1, 16, 0.45f), Quaternion.identity);
        block = blockPre;
        blockfalling = false;
       
    }

    private void StockBlockGenerator()
    {
        
        if (time >= 2 && stockBlocks.Count != 5)
        {
            int tetoNum = Random.Range(0, tetoBlock.Length);
            GameObject blockPre = Instantiate(tetoBlock[tetoNum], new Vector3(7, y - count * 2, 0.45f), Quaternion.identity);
            blockPre.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            stockBlocks.Add(blockPre);
            count++;
        }
    }
}
