using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GuargSensor : MonoBehaviour {
    [SerializeField] List<Transform> visiblePlayer;
    List<Transform> playerCharacters = new List<Transform>();

    [SerializeField] float sightDistance;
    [SerializeField] LayerMask sightBlockers; //not hitting collaiders, only walls

    [SerializeField] float maxangle;

    public List<Transform> GetVisiblePlayers() {          //we didnt make the list public so others scripts cant modify it,
        return visiblePlayer;                             //instead we make list as function and return that list
    }


    void UpdateVisiblePlayers() {
        visiblePlayer.Clear();                               //by defolt agent doesnt see anybody
        foreach (var player in playerCharacters) {           //going through all player characters, 
            Vector3 pos = player.position;

            if (Vector3.Distance(pos, transform.position) > sightDistance) { //TODO too far
                continue;                                   //continue; skip the next, go back to the start of the loop for another player
            }

            RaycastHit hit;                                 //TODO is blocked by a wall/obstacle
            Ray ray = new Ray(transform.position + Vector3.up * 0.3f, //startin position from where drawing line, (at the ground a bit higher?)
                               pos - transform.position);
            if (Physics.Raycast(ray,
                                out hit,
                                Vector3.Distance(transform.position, pos),
                                sightBlockers)) {
                Debug.DrawLine(transform.position + Vector3.up * 0.3f,
                               hit.point,
                               Color.red);                         //if a wall hit it draws a red line
                continue;
            }
            else {
                Debug.DrawLine(transform.position + Vector3.up * 0.3f,
                               pos + Vector3.up * 0.3f,
                               Color.white);                       //if player visible draws a white line
                //no continue; if theres continue then it imposible to run next code, mistake
            }

            var dir = pos - transform.position;                    //vector from us to the target
            if (Vector3.Angle(transform.forward, dir) > maxangle) {//TODO not within the cone of vision
                continue;
            }


            visiblePlayer.Add(player);
        }
    }

    private void OnDrawGizmos() {                           //not visible by main camera
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightDistance);
    }

    void FindPlayerCharacter() {
        var players = FindObjectsByType<SelectablrCharacter>(FindObjectsSortMode.None);
        foreach (var player in players) {
            playerCharacters.Add(player.transform);
        }
    }


    void Start() {
        FindPlayerCharacter();
    }
    void VisualizeAngles() {
        var middle = transform.forward * sightDistance;   //transform.forward is normalized vector 1, dightDistance is max distance
        var rotLeft = Quaternion.AngleAxis(-maxangle, Vector3.up); //rotation around y 
        var rotRight = Quaternion.AngleAxis(maxangle, Vector3.up);
        var p = transform.position;
        var left = rotLeft * middle;
        var right = rotRight * middle;
        Debug.DrawLine(p, p + left, Color.white);
        Debug.DrawLine(p, p + right, Color.white);
    }

    void Update() {
        UpdateVisiblePlayers();
        VisualizeAngles();
    }
}


