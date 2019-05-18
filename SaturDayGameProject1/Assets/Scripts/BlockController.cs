using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private float time;
    private float tmpTime = 0.7f;
    private float intarval = 0.7f;
    [SerializeField]
    private GameObject[] blocks;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //移動処理
    private void Fall()
    {
        time += Time.deltaTime;
        if (time >= tmpTime)
        {
            tmpTime = time + intarval;
            this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x,
                                                                  this.gameObject.transform.localPosition.y - 1,
                                                                  this.gameObject.transform.localPosition.z);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x - 1,
                                                                  this.gameObject.transform.localPosition.y,
                                                                  this.gameObject.transform.localPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x + 1,
                                                                  this.gameObject.transform.localPosition.y,
                                                                  this.gameObject.transform.localPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            this.gameObject.transform.localEulerAngles = new Vector3(0, 0, this.gameObject.transform.localEulerAngles.z - 90);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            this.gameObject.transform.localEulerAngles = new Vector3(0, 0, this.gameObject.transform.localEulerAngles.z + 90);
        }
    }
}
