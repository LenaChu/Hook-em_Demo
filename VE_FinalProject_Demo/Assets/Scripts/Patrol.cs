using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Patrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    private NavMeshAgent agent;
    public ThirdPersonCharacter character;
    public Transform[] moveSpots;
    private int randomSpot;

    private Vector3 lastPos;
    private GameObject player;
    private bool bKeepGoing;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");

        agent.updateRotation = false;
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        bKeepGoing = true;
        lastPos = agent.velocity;
    }


    void keepMoving()
    {
        agent.SetDestination(moveSpots[randomSpot].position);

        if (waitTime <= 0)
        {
            randomSpot = Random.Range(0, moveSpots.Length);
            waitTime = startWaitTime;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }


        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
    }

    void Update()
    {
        float diff = Vector3.Distance(player.transform.position, agent.transform.position);

        if (diff < 1.8f)
        {
            Debug.Log("Update() STOP diff=" + diff);
            agent.velocity = Vector3.zero;
        }
        else
        {
            Debug.Log("Update() GO diff=" + diff);
            agent.velocity = lastPos;
            keepMoving();
        }
    }
}
