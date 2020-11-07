using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class MummyIM : Agent
{
    private Transform tr;
    private Rigidbody rb;

    public float moveSpeed = 1.0f;
    public float turnSpeed = 200.0f;
    public StageManagerIM stageManager;

    public override void Initialize()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        MaxStep = 1000;
    }

    public override void OnEpisodeBegin()
    {
        stageManager.InitStage();
        //에이젼트의 초기위치값을 설정
        tr.localPosition = new Vector3(Random.Range(-4.0f, 4.0f), 0.05f, -3.5f);
        tr.localRotation = Quaternion.identity;

        rb.velocity = rb.angularVelocity = Vector3.zero;
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
        if (coll.gameObject.CompareTag("RED"))
        {
            if (stageManager.hintColor == StageManagerIM.HINT_COLOR.RED)
            {
                SetReward(+1.0f);
            }
            else
            {
                SetReward(-1.0f);
            }
            EndEpisode();
        }

        if (coll.gameObject.CompareTag("GREEN"))
        {
            if (stageManager.hintColor == StageManagerIM.HINT_COLOR.GREEN)
            {
                SetReward(+1.0f);
            }
            else
            {
                SetReward(-1.0f);
            }
            EndEpisode();
        }  

        if (coll.gameObject.CompareTag("BLUE"))
        {
            if (stageManager.hintColor == StageManagerIM.HINT_COLOR.BLUE)
            {
                SetReward(+1.0f);
            }
            else
            {
                SetReward(-1.0f);
            }
            EndEpisode();
        }

        if (coll.gameObject.CompareTag("WALL"))
        {
            SetReward(-1/(float)MaxStep);
        }              
    }


}
