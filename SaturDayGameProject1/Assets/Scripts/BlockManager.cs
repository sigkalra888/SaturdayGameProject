using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tetoBlock;
    [SerializeField]
    private List<GameObject> stockBlocks;
    [SerializeField]
    private GameObject block;

    public float tmpTime;
    private float tmpTime2;
    public float intarval;
    private float intarval2 = 0.1f;

    private int x, y;
    private int adjustX = 5;
    private int adjustY = 2;

    public struct TmpPos
    {
        public int x;
        public int y;
    }

    TmpPos[] tPos = new TmpPos[4];

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
    
    public Text[] text;

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
    private int high = 17;
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

        if (time >= tmpTime2)
        {
            TextPrint();
            tmpTime2 = time + intarval2;
        }

        //prefabのX,Y座標
        if(block == null) { return; }
        x = (int)block.transform.localPosition.x;
        y = (int)block.transform.localPosition.y;
        Debug.Log(BlockHighP((int)block.GetComponent<BlockController>().blocks[3].transform.localPosition.y) + ":" + ((int)block.GetComponent<BlockController>().blocks[3].transform.localPosition.x + x + 5));
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
        blockPre.GetComponent<BlockController>().kidou = true;
        block = blockPre;
        state = State.fall;
        x = (int)block.transform.localPosition.x;
        y = (int)block.transform.localPosition.y;
        for (int i = 0; i < 4; i++)
        {
            tPos[i].x = ((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x + x) + adjustX;
            tPos[i].y = BlockHighP((int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y);
            BlockSetForStage(i);
        }

    }

    private void StockBlockGenerator()
    {
        if (time >= 2 && stockBlocks.Count != 5)
        {
            int tetoNum = 0;//Random.Range(0, tetoBlock.Length);
            GameObject blockPre = Instantiate(tetoBlock[tetoNum], new Vector3(7, high - stockBlocks.Count * 2, 0.45f), Quaternion.identity);
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
            //prefabの中にあるCubeのX座標   以降mx
            int mx = (int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x;

            if (mx + x - 1 <= -6)
            {
                return (int)SideMoveStop.Lstop;
            }
            else if(mx + x + 1 >= 5)
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
            //prefabの中にあるCubeのY座標   以降my
            int my = (int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y;

            if (my + y -1 == -3)
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
        }
        return true;
    }

    public void RotationAdjust()
    {
        for (int i = 0; i < 4; i++)
        {
            int mx = (int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x;

            if (mx + x == 5)
            {
                block.transform.localPosition = new Vector3(block.transform.localPosition.x - 1, block.transform.localPosition.y, block.transform.localPosition.z);
            }
            else if (mx + x == -6)
            {
                block.transform.localPosition = new Vector3(block.transform.localPosition.x + 1, block.transform.localPosition.y, block.transform.localPosition.z);
            }
        }
    }

    private void FinalBlockSetForStage(int i)
    {
        int mx = (int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x;
        int my = (int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y;

        stage[BlockHighP(my) - 1, (mx + x) + adjustX] = 0;
        stage[BlockHighP(my), (mx + x) + adjustX] = 9;
        tmpTime = 0;
    }

    public void BlockSetForStage(int i)
    {
        int mx = (int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.x;
        int my = (int)block.GetComponent<BlockController>().blocks[i].transform.localPosition.y;

        if (stage[BlockHighP(my), (mx + x) + adjustX] != 9)
        {
            stage[tPos[i].y, tPos[i].x] = 0;
            Debug.Log("stage[" + BlockHighP(my) + "," + ((mx + x) + adjustX) + "]");
            stage[BlockHighP(my), (mx + x) + adjustX] = block.GetComponent<BlockController>().myIndex;
            tPos[i].x = (mx + x) + adjustX;
            tPos[i].y = BlockHighP(my);
        }
    }

    private int BlockHighP(int my)
    {
        int _y = (my + y + adjustY) - 20;
        return _y * -1;
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
        block.GetComponent<BlockController>().kidou = true;
        stockBlocks.RemoveAt(0);
        for (int i = 0; i < stockBlocks.Count; i++)
        {
            stockBlocks[i].transform.localPosition = new Vector3(stockBlocks[i].transform.localPosition.x, stockBlocks[i].transform.localPosition.y + 2, stockBlocks[i].transform.localPosition.z);
        }
        state = State.fall;
        x = (int)block.transform.localPosition.x;
        y = (int)block.transform.localPosition.y;
        for (int i = 0; i < 4; i++)
        {
            BlockSetForStage(i);
        }
        TextPrint();
    }

    private void TextPrint()
    {
        for (int i = 0; i < 21; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                text[i * 10 + j].text = stage[i, j].ToString();
            }
        }
    }


}
