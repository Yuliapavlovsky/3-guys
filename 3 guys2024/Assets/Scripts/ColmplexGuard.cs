using UnityEngine;
using UnityEngine.AI;
public enum GuardState { Patrol, Chase, Investigate };

public class ColmplexGuard : MonoBehaviour {
    public Transform[] waypoints;
    public int nextWaypoint = -1;
    public float threshold = 1.0f;

    NavMeshAgent agent;
    GuargSensor sensor;
    [SerializeField] float searchTime = 2f;
    float investigateTimer = 0f;

    [SerializeField] GuardState state;

    Vector3 lastKnownPlayerPos;

    void StartTowardsNextWayPoint() {
        nextWaypoint++;                                       //it start to count from -1 so the first waypoint is [0]

        if (nextWaypoint >= waypoints.Length) {               //without this loop it goes to [4] and its an error
            nextWaypoint = 0;
        }
        agent.destination = waypoints[nextWaypoint].position; //agent start to go to the waypoint
    }


    void Update() {
        var visiblePlayer = sensor.GetVisiblePlayers();
        if (visiblePlayer.Count > 0) {
            state = GuardState.Chase;
            //lastKnownPlayerPos = visiblePlayer[0].position;

            Vector3 closestPos = visiblePlayer[0].position;   //theres at least one player(Count > 0), so we start with the first
            foreach (var vp in visiblePlayer) {
                if (Vector3.Distance(transform.position, closestPos) > //so we compare position of first with another, and if its closer, it became the closest
                     Vector3.Distance(transform.position, vp.position)) {
                    closestPos = vp.position;
                }
            }
            lastKnownPlayerPos = closestPos;
            agent.destination = lastKnownPlayerPos;
        }

        if (state == GuardState.Chase) {
            if (visiblePlayer.Count == 0) {
                state = GuardState.Investigate;
                investigateTimer = searchTime;
            }
        }

        if (state == GuardState.Investigate) {
            investigateTimer -= Time.deltaTime;
            if (investigateTimer < 0f) {
                state = GuardState.Patrol;
                int closestWaypoint = 0;
                for (int i = 0; i < waypoints.Length; i++) {
                    if (Vector3.Distance(waypoints[i].position, transform.position) <
                        Vector3.Distance(waypoints[closestWaypoint].position, transform.position)) {
                        closestWaypoint = i;
                    }
                }
                nextWaypoint = closestWaypoint;
                agent.destination = waypoints[nextWaypoint].position;
            }
        }

        if (state == GuardState.Patrol) {
            float dist = Vector3.Distance(transform.position,                      //checking position between vectors
                                           waypoints[nextWaypoint].position);
            if (dist < threshold) {
                StartTowardsNextWayPoint();
            }

        }
    }

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        sensor = GetComponent<GuargSensor>();

    }

    void Start() {
        StartTowardsNextWayPoint();
    }
}

