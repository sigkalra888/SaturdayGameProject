using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayNum : MonoBehaviour
{
    [SerializeField]
    private Text NumText;
    [SerializeField]
    private GameObject bManager;
    private float x = 340;
    private float y = 40;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 21; j++)
            {
                Text numPre = Instantiate(NumText, new Vector3(NumText.rectTransform.position.x, NumText.rectTransform.position.y, NumText.rectTransform.position.z), Quaternion.identity);
                numPre.rectTransform.position = new Vector3(x - i * 20,y + j * 17, 0);
                numPre.transform.parent = gameObject.transform;
                bManager.GetComponent<BlockManager>().text[i + j] = numPre;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
