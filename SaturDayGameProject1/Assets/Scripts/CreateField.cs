using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateField : MonoBehaviour
{
    [HideInInspector]
    public bool complete = false;

    [SerializeField]
    private GameObject stageManager;

    [SerializeField]
    private Text field;

    private int fieldCount = 0;

    [SerializeField]
    private GameObject fieldData;

    void Start()
    {
        //フィールドの作成
        for (int i = 0; i < 21; i++)
        {
            FeildGenerate(i);
        }
        complete = true;
    }

    private void FeildGenerate(int y)
    {
        for (int i = 0; i < 10; i++)
        {
            Text fieldPre = Instantiate(field, Vector3.zero, Quaternion.identity);
            fieldPre.transform.SetParent(fieldData.transform);
            fieldPre.transform.localPosition = new Vector3(-100 + i * 20, 180 - y * 18, 0);
            stageManager.GetComponent<StageManager>().fieldText[fieldCount] = fieldPre;
            fieldCount++;
        }
    }
}
