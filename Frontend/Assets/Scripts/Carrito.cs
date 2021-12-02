using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Carrito : MonoBehaviour
{
    
    NavMeshAgent agent;
    public Transform objetivo;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();        
        agent.destination = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = objetivo.position;
    }
}
