using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManagerIM : MonoBehaviour
{
    public enum HINT_COLOR
    {
        RED   = 0,
        GREEN = 1,
        BLUE  = 2
    }

    //현재 생성된 힌트 색상
    public HINT_COLOR hintColor = HINT_COLOR.RED;

    //힌트 객체를 저장할 배열
    public GameObject[] hintPrefabs;
    private Vector3 pos;

    void Start()
    {
        pos = transform.position + new Vector3(0, 0.55f, 0);        
    }

    public void InitStage()
    {
        var prevHint = transform.Find("HINT_RED");
        if (prevHint != null) Destroy(prevHint.gameObject);

        prevHint = transform.Find("HINT_GREEN");
        if (prevHint != null) Destroy(prevHint.gameObject);

        prevHint = transform.Find("HINT_BLUE");
        if (prevHint != null) Destroy(prevHint.gameObject);

        //불규칙한 힌트 생성
        int idx = Random.Range(0, hintPrefabs.Length); //0, 1, 2

    }

}
