using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public BlockManager blockManager;
    private float time;
    private float tmpTime = 0.7f;
    private float intarval = 0.7f;
    [SerializeField]
    public GameObject[] blocks;
    public bool fallState = false;
    public bool sideMoveState = false;
    public bool rotatinState = false;
    public bool kidou = false;
    public int myIndex;
    private int myState = 0;
    public int[,] blockPosNow;
    public int[,] blockPosDef;
    public int[,] blockPosR;
    public int[,] blockPosL;
    public int[,] blockPosU;

    void Start()
    {
        blockPosNow = new int[4, 4];
        blockPosNow = blockPosDef;
    }

    // Update is called once per frame
    void Update()
    {
        if (!kidou) { return; }
        Fall();
    }

    //移動処理
    private void Fall()
    {
        if (fallState)
        {
            time += Time.deltaTime;
            if (time >= tmpTime)
            {
                if (blockManager.PosUpdateF())
                {
                    tmpTime = time + intarval;
                    this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x,
                                                                          this.gameObject.transform.localPosition.y - 1,
                                                                          this.gameObject.transform.localPosition.z);
                }
            }    
        }

        if (sideMoveState)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (blockManager.PosUpdateS() == (int)BlockManager.SideMoveStop.Move || blockManager.PosUpdateS() == (int)BlockManager.SideMoveStop.Rstop)
                {
                    this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x - 1,
                                                                      this.gameObject.transform.localPosition.y,
                                                                      this.gameObject.transform.localPosition.z);
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (blockManager.PosUpdateS() == (int)BlockManager.SideMoveStop.Move || blockManager.PosUpdateS() == (int)BlockManager.SideMoveStop.Lstop)
                {
                    this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x + 1,
                                                                      this.gameObject.transform.localPosition.y,
                                                                      this.gameObject.transform.localPosition.z);
                }
                
            }
        }

        if (rotatinState)
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
