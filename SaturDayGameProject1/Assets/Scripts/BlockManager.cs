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

    private int[,] stage = new int[21, 10]
    {
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    };

    private float time;

    //ストックブロック生成の為の変数
    private int y = 17;
    private int count = 0;

    void Start()
    {
        Invoke("FirstBlockGenerator", 2f);
    }
    
    void Update()
    {
        time += Time.deltaTime;
        StockBlockGenerator();
    }

    private void FirstBlockGenerator()
    {
        int tetoNum = 0;//Random.Range(0, tetoBlock.Length);
        GameObject blockPre = Instantiate(tetoBlock[tetoNum], new Vector3(-1, 16, 0.45f), Quaternion.identity);
        BlockPosSet(blockPre, tetoNum);
        blockPre.GetComponent<BlockController>().blockManager = this.gameObject.GetComponent<BlockManager>();
        blockPre.GetComponent<BlockController>().fallstate = true;
        block = blockPre;
       
    }

    private void StockBlockGenerator()
    {
        
        if (time >= 2 && stockBlocks.Count != 5)
        {
            int tetoNum = 0;//Random.Range(0, tetoBlock.Length);
            GameObject blockPre = Instantiate(tetoBlock[tetoNum], new Vector3(7, y - count * 2, 0.45f), Quaternion.identity);
            BlockPosSet(blockPre, tetoNum);
            blockPre.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            stockBlocks.Add(blockPre);
            count++;
        }
    }

    private void BlockPosSet(GameObject pre, int x)
    {
        switch (x + 1)
        {
            case 1:
                pre.GetComponent<BlockController>().blockPosDef = new int[4, 4]
                {
                    {0, 1, 0, 0},
                    {1, 1, 1, 0},
                    {0, 0, 0, 0},
                    {0, 0, 0, 0}
                };
                pre.GetComponent<BlockController>().blockPosR = new int[4, 4]
                {
                    {0, 1, 0, 0},
                    {0, 1, 1, 0},
                    {0, 1, 0, 0},
                    {0, 0, 0, 0}
                };
                pre.GetComponent<BlockController>().blockPosL = new int[4, 4]
                {
                    {0, 1, 0, 0},
                    {1, 1, 0, 0},
                    {0, 1, 0, 0},
                    {0, 0, 0, 0}
                };
                pre.GetComponent<BlockController>().blockPosU = new int[4, 4]
                {
                    {0, 0, 0, 0},
                    {1, 1, 1, 0},
                    {0, 1, 0, 0},
                    {0, 0, 0, 0}
                };
                pre.GetComponent<BlockController>().myIndex = 1;
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
        }
    }

    public void PosUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            if ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y - 1 < 0 || (int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x + 1 > 10 || (int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x - 1< 0)
            {
                block.GetComponent<BlockController>().fallstate = false;
                break;
            }
            stage[(int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y,
                  (int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x] = block.GetComponent<BlockController>().myIndex;
        }
    }
}
