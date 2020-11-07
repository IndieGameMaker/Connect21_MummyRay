using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //에이젼트 초기화
    public override void Initialize()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        stageManager = tr.parent.Find("StageManager").GetComponent<StageManager>();
    }

    //학습을 시작할때마다 호출되는 메소드(스테이지-환경을 초기화)
    public override void OnEpisodeBegin()
    {

    }

    //주변환경을 관측
    public override void CollectObservations(VectorSensor sensor)
    {

    }

    //정책에 따른 행동
    public override void OnActionReceived(float[] vectorAction)
    {

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
}
