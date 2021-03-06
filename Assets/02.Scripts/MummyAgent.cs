﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

//주변환경을 관측, 전달 (Observation)
//정책에 따라서 행동 (Action)
//행동에 따른 보상 (Reward)
public class MummyAgent : Agent
{
    private Transform tr;
    private Rigidbody rb;
    private StageManager stageManager;

    public float moveSpeed = 1.5f;
    public float turnSpeed = 200.0f;
    public Text rewardText;

    private Renderer floorRd;
    private Material originMt;
    public Material goodMt;
    public Material badMt;

    //에이젼트 초기화
    public override void Initialize()
    {
        MaxStep = 5000;
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        stageManager = tr.parent.Find("StageManager").GetComponent<StageManager>();
        floorRd = tr.parent.Find("Floor").GetComponent<MeshRenderer>();
        originMt = floorRd.material;
    }

    //학습을 시작할때마다 호출되는 메소드(스테이지-환경을 초기화)
    public override void OnEpisodeBegin()
    {
        //스테이지 초기화
        stageManager.InitStage();
        //물리력을 모두 초기화
        rb.velocity = rb.angularVelocity = Vector3.zero;
        //에이젼트의 위치를 불규칙하게 변경
        tr.localPosition = new Vector3(Random.Range(-24.0f, 24.0f)
                                        , 0.05f
                                        , Random.Range(-24.0f, 24.0f));
        tr.localRotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360));
        //Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    //주변환경을 관측
    public override void CollectObservations(VectorSensor sensor)
    {

    }

    //정책에 따른 행동(전달받은 명령에 따라서 행동)
    public override void OnActionReceived(float[] vectorAction)
    {
        //Debug.Log($"[0]={vectorAction[0]}, [1]={vectorAction[1]}");
        Vector3 dir = Vector3.zero;
        Vector3 rot = Vector3.zero;

        //좌우회전 처리 vectorAction[1]
        switch ((int)vectorAction[1])
        {
            case 0: break;
            case 1: rot = Vector3.up * -1.0f; break;
            case 2: rot = Vector3.up * +1.0f; break;
        }
        //전진/후진처리 vectorAction[0]
        switch ((int)vectorAction[0])
        {
            case 0: break;
            case 1: dir =  Vector3.forward; break;
            case 2: dir = -Vector3.forward; break;
        }

        tr.Rotate(rot * Time.fixedDeltaTime * turnSpeed);
        rb.AddRelativeForce(dir * moveSpeed, ForceMode.VelocityChange);

        AddReward(-1/(float)MaxStep);
        rewardText.text = GetCumulativeReward().ToString("##0.0000");
    }

    //개발자가 테스트용, 모방학습(Immetation Learing)
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0.0f;   //Non-key , W, S (0, 1, 2)
        actionsOut[1] = 0.0f;   //Non-key , A, D (0, 1, 2)

        //전진/후진
        if (Input.GetKey(KeyCode.W))
        {
            actionsOut[0] = 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            actionsOut[0] = 2.0f;
        }

        //좌우 회전
        if (Input.GetKey(KeyCode.A))
        {
            actionsOut[1] = 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            actionsOut[1] = 2.0f;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("GOOD_ITEM"))
        {
            Destroy(coll.gameObject);
            rb.velocity = rb.angularVelocity = Vector3.zero;
            AddReward(+1.0f);
            StartCoroutine(ChangeMaterial(goodMt));
        }

        if (coll.gameObject.CompareTag("BAD_ITEM"))
        {
            Destroy(coll.gameObject);
            rb.velocity = rb.angularVelocity = Vector3.zero;
            AddReward(-1.0f);
            EndEpisode();
            StartCoroutine(ChangeMaterial(badMt));
        }

        if (coll.gameObject.CompareTag("WALL"))
        {
            //rb.velocity = rb.angularVelocity = Vector3.zero;
            AddReward(-0.1f);
        }
    }

    IEnumerator ChangeMaterial(Material changeMt)
    {
        floorRd.material = changeMt;
        yield return new WaitForSeconds(0.2f);
        floorRd.material = originMt;
    }
}
