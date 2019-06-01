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

    public enum State
    {
        fall,
        next
    }

    private State state;
    public enum SideMoveStop
    {
        Move,
        Rstop,
        Lstop,
        RLstop
    }

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
        if (state == State.next)
        {
            NextBlock();
        }
    }

    private void FirstBlockGenerator()
    {
        int tetoNum = 0;//Random.Range(0, tetoBlock.Length);
        GameObject blockPre = Instantiate(tetoBlock[tetoNum], new Vector3(-1, 16, 0.45f), Quaternion.identity);
        BlockPosSet(blockPre, tetoNum);
        blockPre.GetComponent<BlockController>().blockManager = this.gameObject.GetComponent<BlockManager>();
        blockPre.GetComponent<BlockController>().sideMoveState = true;
        blockPre.GetComponent<BlockController>().fallState = true;
        blockPre.GetComponent<BlockController>().rotatinState = true;
        block = blockPre;
        state = State.fall;
       
    }

    private void StockBlockGenerator()
    {
        
        if (time >= 2 && stockBlocks.Count != 5)
        {
            int tetoNum = 0;//Random.Range(0, tetoBlock.Length);
            GameObject blockPre = Instantiate(tetoBlock[tetoNum], new Vector3(7, y - stockBlocks.Count * 2, 0.45f), Quaternion.identity);
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

    public int PosUpdateS()
    {
        for (int i = 0; i < 4; i++)
        {
            if ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x - 1 <= -6)
            {
                return (int)SideMoveStop.Lstop;
            }
            else if((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x + 1 >= 5)
            {
                return (int)SideMoveStop.Rstop;
            }
            else {
                BlockSetForStage(i);
            }
        }
        return (int)SideMoveStop.Move;
    }

    public bool PosUpdateF()
    {
        for (int i = 0; i < 4; i++)
        {
            if ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y - 1 == -3)
            {
                block.GetComponent<BlockController>().fallState = false;
                block.GetComponent<BlockController>().sideMoveState = false;
                block.GetComponent<BlockController>().rotatinState = false;
                for (int j = 0; j < 4; j++)
                {
                    FinalBlockSetForStage(j);
                }
                state = State.next;
                return false;
            }
            else
            {
                if (BlockSetForStage(i) == false)
                {
                    block.GetComponent<BlockController>().fallState = false;
                    block.GetComponent<BlockController>().sideMoveState = false;
                    block.GetComponent<BlockController>().rotatinState = false;
                    state = State.next;
                    break;
                }
            }
        }
        TextPrint();
        return true;
    }

    public void RotationAdjust()
    {
        for (int i = 0; i < 4; i++)
        {
            if ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x == 5)
            {
                block.transform.localPosition = new Vector3(block.transform.localPosition.x - 1, block.transform.localPosition.y, block.transform.localPosition.z);
            }
            else if ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x == -6)
            {
                block.transform.localPosition = new Vector3(block.transform.localPosition.x + 1, block.transform.localPosition.y, block.transform.localPosition.z);
            }
        }
    }

    private void FinalBlockSetForStage(int i)
    {
        if ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x < 0)
        {
            if ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y < 0)
            {
                stage[((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y) * -1,
                ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x) * -1] = 9;
            }
            else
            {
                stage[(int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y,
                ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x) * -1] = 9;
            }
        }
        else
        {
            if ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y < 0)
            {
                stage[((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y) * -1,
                ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x)] = 9;
            }
            else
            {
                stage[(int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y,
                (int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x] = 9;
            }
        }
    }

    private bool BlockSetForStage(int i)
    {
        if ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x < 0)
        {
            if ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y < 0)
            {
                if (stage[((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y) * -1,
                    ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x) * -1] != 9)
                {
                    stage[((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y) * -1,
                    ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x) * -1] = block.GetComponent<BlockController>().myIndex;
                    return true;
                }
                return false;
            }
            else
            {
                if (stage[(int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y,
                ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x) * -1] != 9)
                {
                    stage[(int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y,
                    ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x) * -1] = block.GetComponent<BlockController>().myIndex;
                    return true;
                }
                return false;
            }
        }
        else
        {
            if ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y < 0)
            {
                if (stage[((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y) * -1,
                ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x)] != 9)
                {
                    stage[((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y) * -1,
                    ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x)] = block.GetComponent<BlockController>().myIndex;
                    return true;
                }
                return false;
            }
            else
            {
                if (stage[(int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y,
                (int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x] != 9)
                {
                    stage[(int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y + (int)block.transform.localPosition.y,
                    (int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + (int)block.transform.localPosition.x] = block.GetComponent<BlockController>().myIndex;
                    return true;
                }
                return false;
            }
        }
    }

    private void NextBlock()
    {
        block = stockBlocks[0];
        block.transform.localPosition = new Vector3(-1, 16, 0.45f);
        block.transform.localScale = new Vector3(1,1,1);
        block.GetComponent<BlockController>().blockManager = this.gameObject.GetComponent<BlockManager>();
        block.GetComponent<BlockController>().sideMoveState = true;
        block.GetComponent<BlockController>().fallState = true;
        block.GetComponent<BlockController>().rotatinState = true;
        stockBlocks.RemoveAt(0);
        for (int i = 0; i < stockBlocks.Count; i++)
        {
            stockBlocks[i].transform.localPosition = new Vector3(stockBlocks[i].transform.localPosition.x, stockBlocks[i].transform.localPosition.y + 2, stockBlocks[i].transform.localPosition.z);
        }
        state = State.fall;
    }

    private void TextPrint()
    {
        for (int i = 0; i < 21; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Debug.Log(i + "," + j + ":" + stage[i, j]);
            }
        }
    }
}
