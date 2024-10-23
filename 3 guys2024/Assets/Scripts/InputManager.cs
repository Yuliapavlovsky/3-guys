using UnityEngine;
using UnityEngine.AI;

public class InputManager : MonoBehaviour {
    
    public SelectablrCharacter currentSelection;
    [SerializeField] LayerMask onlyGround;
    public bool IsSelected(SelectablrCharacter character) {
        return character == currentSelection; //selected character 
    }

    public void NewSelection(SelectablrCharacter character) {
        if (character == currentSelection) { //if selected the same character do nothing
            return;                          //stops execution
        }

        if (currentSelection != null) {      //check if theres previous selection, its iiligal if its null
            currentSelection.OnDeselect();   //deselect the character
        }
        character.OnSelect();                //select the character
        currentSelection = character;

    }


    void Update() {
        Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
        if (currentSelection != null &&
            Input.GetKeyDown(KeyCode.Mouse1)) {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, onlyGround)) {
                print("Yes there's something under the mouse");
                var position = hit.point;
                currentSelection.GetComponent<NavMeshAgent>().destination = position;                             // currentSelection.transform.position = position; 
                                              //if we find something hit our chara gonna move there
            }
        }

    }
}
