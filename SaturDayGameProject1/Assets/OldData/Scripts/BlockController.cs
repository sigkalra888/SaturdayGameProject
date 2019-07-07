using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public BlockManager blockManager;
    private float time;
    [SerializeField]
    public GameObject[] blocks;
    public bool fallState = false;
    public bool kidou = false;
    public int myIndex;
    private int myState = 0;
    [HideInInspector]
    public bool firstTime = false;
    public int[,] blockPosNow;
    public int[,] blockPosDef;
    public int[,] blockPosR;
    public int[,] blockPosL;
    public int[,] blockPosU;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            blockManager.posManager[blockManager.count].tetoBlockBasePos[(int)blocks[i].transform.localPosition.y, 
                                                                         (int)blocks[i].transform.localPosition.x] = 1;
        }
        blockManager.count++;
    }

    // Update is called once per frame
    void Update()
    {
        if (!kidou) { return; }
        if (!firstTime)
        {
            blockManager.x = (int)gameObject.transform.localPosition.x;
            blockManager.y = (int)gameObject.transform.localPosition.y;
            for (int i = 0; i < 4; i++)
            {
                blockManager.tPos[i].x = blockManager.x + (int)blocks[i].transform.localPosition.x;
                blockManager.tPos[i].y = ((blockManager.y + (int)blocks[i].transform.localPosition.y) - 20) * -1;
                blockManager.BlockSetForStage(i);
            }
            blockPosNow = new int[4, 4];
            blockPosNow = blockPosDef;
            blockManager.TextPrint();
            CanFall();
            firstTime = true;
        }
        Fall();
    }

    //移動処理
    private void Fall()
    {
        if (fallState)
        {
            time += Time.deltaTime;
            if (time >= blockManager.tmpTime)
            {
                if (CanFall() == false)
                {
                    blockManager.PosUpdateF();
                }
                else
                {
                    blockManager.tmpTime = time + blockManager.intarval;

                    this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x,
                                                                          this.gameObject.transform.localPosition.y + 1,
                                                                          this.gameObject.transform.localPosition.z);
                }

                for (int i = 0; i < 4; ++i)
                {
                    blockManager.BlockSetForStage(i);
                }

                blockManager.TextPrint();
            }    
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (blockManager.PosUpdateS() == (int)BlockManager.SideMoveStop.Move || blockManager.PosUpdateS() == (int)BlockManager.SideMoveStop.Rstop)
            {
                this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x - 1,
                                                                  this.gameObject.transform.localPosition.y,
                                                                  this.gameObject.transform.localPosition.z);
                for (int i = 0; i < 4; ++i)
                {
                    blockManager.BlockSetForStage(i);
                }
            }

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (blockManager.PosUpdateS() == (int)BlockManager.SideMoveStop.Move || blockManager.PosUpdateS() == (int)BlockManager.SideMoveStop.Lstop)
            {
                this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x + 1,
                                                                  this.gameObject.transform.localPosition.y,
                                                                  this.gameObject.transform.localPosition.z);
                for (int i = 0; i < 4; ++i)
                {
                    blockManager.BlockSetForStage(i);
                }
            }

        }

        RotatedPosSet();
        
    }

    private bool CanFall()
    {
        for (int i = 0; i < 4; i++)
        {
            int y =  (int)blocks[i].transform.localPosition.y;
            int x = (int)blocks[i].transform.localPosition.x;
            if (blockManager.y + y == 0)
            {
                return false;
            }
            else if (blockManager.stage[((y + blockManager.y + 1) - 20) * -1, x + blockManager.x] == 9)
            {
                return false;
            }
            Debug.Log(((y + blockManager.y + 1) - 20) * -1 + ":" + (x + blockManager.x));
            Debug.Log(blockManager.stage[((y + blockManager.y + 1) - 20) * -1, x + blockManager.x]);
        }

        return true;
    }

    private void RotatedPosSet()
    {
        switch (myIndex)
        {
            case 1:
                BlockRotation1(myState);
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

    private void BlockRotation1(int x)
    {
        switch (x)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    blockPosNow = blockPosL;
                    myState = 3;
                    blocks[0].transform.localPosition = new Vector3(0, -1, 0);
                    blocks[2].transform.localPosition = new Vector3(0, 1, 0);
                    blocks[3].transform.localPosition = new Vector3(-1, 0, 0);
                    blockManager.RotationAdjust();
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    blockPosNow = blockPosR;
                    myState = 1;
                    blocks[0].transform.localPosition = new Vector3(0, 1, 0);
                    blocks[2].transform.localPosition = new Vector3(0, -1, 0);
                    blocks[3].transform.localPosition = new Vector3(1, 0, 0);
                    blockManager.RotationAdjust();
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    blockPosNow = blockPosDef;
                    myState = 0;
                    blocks[0].transform.localPosition = new Vector3(-1, 0, 0);
                    blocks[2].transform.localPosition = new Vector3(1, 0, 0);
                    blocks[3].transform.localPosition = new Vector3(0, 1, 0);
                    blockManager.RotationAdjust();
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    blockPosNow = blockPosU;
                    myState = 2;
                    blocks[0].transform.localPosition = new Vector3(1, 0, 0);
                    blocks[2].transform.localPosition = new Vector3(-1, 0, 0);
                    blocks[3].transform.localPosition = new Vector3(0, -1, 0);
                    blockManager.RotationAdjust();
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    blockPosNow = blockPosR;
                    myState = 1;
                    blocks[0].transform.localPosition = new Vector3(0, 1, 0);
                    blocks[2].transform.localPosition = new Vector3(0, -1, 0);
                    blocks[3].transform.localPosition = new Vector3(1, 0, 0);
                    blockManager.RotationAdjust();
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    blockPosNow = blockPosL;
                    myState = 3;
                    blocks[0].transform.localPosition = new Vector3(0, -1, 0);
                    blocks[2].transform.localPosition = new Vector3(0, 1, 0);
                    blocks[3].transform.localPosition = new Vector3(-1, 0, 0);
                    blockManager.RotationAdjust();
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    blockPosNow = blockPosU;
                    myState = 2;
                    blocks[0].transform.localPosition = new Vector3(1, 0, 0);
                    blocks[2].transform.localPosition = new Vector3(-1, 0, 0);
                    blocks[3].transform.localPosition = new Vector3(0, -1, 0);
                    blockManager.RotationAdjust();
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    blockPosNow = blockPosDef;
                    myState = 0;
                    blocks[0].transform.localPosition = new Vector3(-1, 0, 0);
                    blocks[2].transform.localPosition = new Vector3(1, 0, 0);
                    blocks[3].transform.localPosition = new Vector3(0, 1, 0);
                    blockManager.RotationAdjust();
                }
                break;
        }
    }
}
