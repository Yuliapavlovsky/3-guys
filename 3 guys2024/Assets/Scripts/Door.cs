using UnityEngine;

public class Door : MonoBehaviour {
    public bool opening = false;
    [Range(0, 1)] public float howOpen = 0;         // in editor polzunok
    public float speed = 1;
    public float maxRotation = 90;
    Quaternion initialRot;

   public void Open() { 
        opening = true;
    }
    public void Close() { 
        opening=false;
    }

    private void Awake() {
            initialRot= transform.rotation;        // take original rotation that was in parent object
    }

    void Update() {
        howOpen += Time.deltaTime * (opening ? 1 : -1); // bool, if true  *1, if not *-1
        /*the same:
          if (opening) {howOpen += Time.deltaTime*speed; }
            else {howOpen += Time.deltaTime * -speed;}  */

        howOpen = Mathf.Clamp01(howOpen);          //Mathf.Clamp01(howOpen, 0, 1); function for restriction of value
        var rot = Quaternion.AngleAxis(howOpen * maxRotation, Vector3.up);  // need a conspect of quaternion
        transform.rotation = rot*initialRot;       // initial its where we started and from there we rotate to the rot
    }
}
