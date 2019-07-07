using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateFeild : MonoBehaviour
{
    [SerializeField]
    private Text feild;

    [SerializeField]
    private GameObject feildData;

    void Start()
    {
        //フィールドの作成
        for (int i = 0; i < 21; i++)
        {
            FeildGenerate(i);
        }
    }

    private void FeildGenerate(int y)
    {
        for (int i = 0; i < 10; i++)
        {
            Text feildPre = Instantiate(feild, Vector3.zero, Quaternion.identity);
            feildPre.transform.SetParent(feildData.transform);
            feildPre.transform.localPosition = new Vector3(-100 + i * 20, 180 - y * 18, 0);
        }
    }
}
