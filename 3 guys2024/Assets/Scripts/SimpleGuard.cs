using UnityEngine;
using UnityEngine.AI;

public class SimpleGuard : MonoBehaviour {
    public Transform[] waypoints;
    public int nextWaypoint = -1;
    public float threshold = 1.0f;
    NavMeshAgent agent;

    void StartTowardsNextWayPoint() {
        nextWaypoint++;                                       //it start to count from -1 so the first waypoint is [0]

        if (nextWaypoint >= waypoints.Length) {               //without this loop it goes to [4] and its an error
            nextWaypoint = 0;
        }
        agent.destination = waypoints[nextWaypoint].position; //agent start to go to the waypoint
    }


    void Update() {
        float dist = Vector3.Distance(transform.position,                      //checking position between vectors
                                       waypoints[nextWaypoint].position);
        if (dist < threshold) {
            StartTowardsNextWayPoint();
        }
    }

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();

    }

    void Start() {
        StartTowardsNextWayPoint();
    }
}

