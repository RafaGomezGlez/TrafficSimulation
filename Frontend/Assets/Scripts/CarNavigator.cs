using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarNavigator : MonoBehaviour
{
    [SerializeField] private Transform movePositionTransform;

    Requesting positionScript;
    private NavMeshAgent navMeshAgent;
    Vector3 targetPos;
    [SerializeField] public GameObject movementController;
    

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        movementController = GameObject.FindWithTag("GameController");
    }

    private void Start() {
        
    }

    private void Update() {
    }
}
