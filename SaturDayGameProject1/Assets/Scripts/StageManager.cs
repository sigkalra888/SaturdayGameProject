using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private BParam1 bparam1;
    [SerializeField]
    private Bparam baseParameter;

    private bool fallStart = false;

    [SerializeField]
    private int downCount = 1;
    private int SideCount = 3;

    private float time = 0;
    [SerializeField]
    private float tmpTime;

    //現在のブロックの配列データ
    private int[,] blockPos = new int[4,4];

    private struct TMPBPos
    {
        public int y;
        public int x;
    }

    private TMPBPos[] tmpBPos = new TMPBPos[4];

    //tmpBPosにデータを保存する時にカウント
    private int posSetCount = 0;

    private enum SideMoveLimit
    {
        NoLimit,
        Lstop,
        Rstop,
        Stop
    }

    private SideMoveLimit smLimit = SideMoveLimit.NoLimit;

    public Text[] fieldText = new Text[210];

    //ステージの配列データ
    private int[,] stage = new int[21, 10]
    {
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0}
    };
    
    void Update()
    {
        if (canvas.GetComponent<CreateField>().complete)
        {
            SpwanBlock();
            canvas.GetComponent<CreateField>().complete = false;
        }
        PrintStage();
        if (!fallStart) { return; }

        time += Time.deltaTime;

        SideMove();
        
        if (time >= tmpTime)
        {
            DataSetStage();
            Fall();
            time = 0;
        }
        
    }

    private void SpwanBlock()
    {
        int num = 1;//Random.Range(0,8);
        BlockPosSet(num);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (baseParameter.blockDPos[i, j] == 1)
                {
                    tmpBPos[posSetCount].y = i + downCount;
                    tmpBPos[posSetCount].x = j + SideCount;
                    stage[i + downCount, j + SideCount] = baseParameter.myIndex;
                    posSetCount++;
                }
                
            }
        }
        posSetCount = 0;
        fallStart = true;
    }
    private void Fall()
    {
        if (fallStart)
        {
            downCount++;

            BlockMove();
        }
    }

    private void SideMove()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(smLimit == SideMoveLimit.NoLimit || smLimit == SideMoveLimit.Rstop)
            {
                SideCount--;

                DataSetStage();

                BlockMove();
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (smLimit == SideMoveLimit.NoLimit || smLimit == SideMoveLimit.Lstop)            {
                SideCount++;

                DataSetStage();

                BlockMove();
            }
        }
    }

    private void BlockMove()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (baseParameter.blockDPos[i, j] == 1)
                {
                    if (i + downCount == 21)
                    {
                        fallStart = false;
                        break;
                    }
                    else if (j + SideCount == 0)
                    {
                        smLimit = SideMoveLimit.Lstop;
                    }
                    else if (j + SideCount == 9)
                    {
                        smLimit = SideMoveLimit.Rstop;
                    }
                    tmpBPos[posSetCount].y = i + downCount;
                    tmpBPos[posSetCount].x = j + SideCount;
                    posSetCount++;
                    stage[i + downCount, j + SideCount] = baseParameter.myIndex;
                }
            }
        }
        posSetCount = 0;
    }

    private void DataSetStage()
    {
        if (downCount < 19)
        {
            for (int i = 0; i < 4; i++)
            {
                stage[tmpBPos[i].y, tmpBPos[i].x] = 0;
            }
        }
    }

    private void BlockPosSet(int i)
    {
        switch (i)
        {
            case 1:
                baseParameter.myIndex = bparam1.myIndex;
                baseParameter.blockDPos = bparam1.blockDPos;
                baseParameter.blockLPos = bparam1.blockLPos;
                baseParameter.blockRPos = bparam1.blockRPos;
                baseParameter.blockUPos = bparam1.blockUPos;
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

    private void PrintStage()
    {
        for (int i = 0; i < 21; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                fieldText[i * 10 + j].text = stage[i,j].ToString();
            }
        }
    }
}
