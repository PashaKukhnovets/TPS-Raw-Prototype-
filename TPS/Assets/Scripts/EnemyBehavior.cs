using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public List<Transform> locations;
    
    [SerializeField] private Transform playerPoint;
    [SerializeField] private Transform patrolRoute;

    private int locationIndex = 0;
    private NavMeshAgent agent;

    private int _lives = 3;
    public int EnemyLives
    {
        get {
            return _lives;
        }

        set {
            _lives = value;
        }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    private void Update()
    {
        isAliveToMove();
        ReactToHit();
        if (agent.remainingDistance < 0.2f && !agent.pathPending) {
            MoveToNextPatrolLocation();
        }
    }

    private IEnumerator Die() {
        this.transform.Rotate(-75, 0, 0);
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            agent.destination = playerPoint.position;
            Debug.Log("Player detected - attack!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Player out of range, resume patrol");
        }
    }

    void InitializePatrolRoute() {
        foreach (Transform child in patrolRoute) {
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation() {
        if (locations.Count == 0)
            return;

        agent.destination = locations[locationIndex].position;

        locationIndex = (locationIndex + 1) % locations.Count;
    }

    void ReactToHit() {
        if (_lives <= 0)
        {
            StartCoroutine(Die());
        }
    }

    void isAliveToMove() {
        if (EnemyLives <= 0) {
            agent.destination = this.gameObject.transform.position;
        }
    }
}
