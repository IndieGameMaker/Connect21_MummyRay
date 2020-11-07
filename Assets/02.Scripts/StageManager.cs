using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    public GameObject goodItem;
    public GameObject badItem;

    public int goodCount = 30;
    public int badCount  = 30;

    public List<GameObject> goodList = new List<GameObject>();
    public List<GameObject> badList  = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InitStage();
    }

    public void InitStage()
    {
        //기존의 Item을 삭제
        foreach(var obj in goodList)
        {
            Destroy(obj);
        }
        foreach(var obj in badList)
        {
            Destroy(obj);
        }

        //List 변수 초기화
        goodList.Clear();
        badList.Clear();

        //GoodItem 생성
        for (int i=0; i<goodCount; i++)
        {
            Vector3 pos = new Vector3( Random.Range(-24.0f, 24.0f)
                                     , 0.05f
                                     , Random.Range(-24.0f, 24.0f) );
            Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360), 0);
            goodList.Add(Instantiate<GameObject>(goodItem, transform.position + pos, rot, this.transform));
        }

        //BadItem 생성
        for (int i=0; i<badCount; i++)
        {
            Vector3 pos = new Vector3( Random.Range(-24.0f, 24.0f)
                                     , 0.05f
                                     , Random.Range(-24.0f, 24.0f) );
            Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360), 0);
            badList.Add(Instantiate<GameObject>(badItem, transform.position + pos, rot, this.transform));
        }        
    }
}
