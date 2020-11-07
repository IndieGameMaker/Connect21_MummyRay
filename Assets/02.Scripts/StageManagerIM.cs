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

    private GameObject prevHint;

    void Start()
    {
        pos = transform.position + new Vector3(0, 0.55f, 0);        
    }

    public void InitStage()
    {
        if (prevHint != null) Destroy(prevHint);

        //불규칙한 힌트 생성
        int idx = Random.Range(0, hintPrefabs.Length); //0, 1, 2
        prevHint = Instantiate(hintPrefabs[idx], pos, Quaternion.identity, this.transform.parent);
        hintColor = (HINT_COLOR)idx;
    }

}
