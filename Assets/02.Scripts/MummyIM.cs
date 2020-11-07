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
        
    }

    public override void OnActionReceived(float[] vectorAction)
    {

    }

    public override void Heuristic(float[] actionsOut)
    {

    }



}
