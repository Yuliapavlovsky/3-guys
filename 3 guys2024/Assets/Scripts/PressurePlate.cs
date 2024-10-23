using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour {
    int count = 0;
    public UnityEvent plateDown;
    public UnityEvent plateUp;
    public Transform graphics;
    public float shiftY = 0.4f;    

    void OnTriggerEnter(Collider c) {
        var character = c.GetComponent<SelectablrCharacter>();
        if (character != null) {
            count++;
            if (count == 1) {
                print("Plate down");
                plateDown.Invoke();
                graphics.position += Vector3.down * shiftY;
            }
        }
    }
    void OnTriggerExit(Collider c) {
        var character = c.GetComponent<SelectablrCharacter>();
        if (character != null) {
            count--;
            if (count == 0) {
                print("Plate up");
                plateUp.Invoke();
            }
        }
    }
}

