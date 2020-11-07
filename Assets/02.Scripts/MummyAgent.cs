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
    //에이젼트 초기화
    public override void Initialize()
    {

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
        
    }
}
