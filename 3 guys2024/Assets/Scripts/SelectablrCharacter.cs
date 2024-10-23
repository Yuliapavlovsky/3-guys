using UnityEngine;

public class SelectablrCharacter : MonoBehaviour {
    Color normalColor; //Color normalColor
    [SerializeField] Color selectedColor;
    [SerializeField] Color highlightColor;
    InputManager im;
    Renderer rend; //
    public void OnSelect() {
        rend.material.color = selectedColor;  // color is in material in mesh
    }
    private void OnMouseEnter() {              //mouse over
        if (!im.IsSelected(this)) {
            rend.material.color = highlightColor;
        }
    }
    private void OnMouseExit() {               // pull mouse avay
        if (!im.IsSelected(this)) {            // if cube is not selected we dont want it to returen to normal color
            rend.material.color = normalColor; //arent selected cubes returned to normal color
        }
    }

    public void OnDeselect() {
        rend.material.color = normalColor;
    }

    private void Awake() {
        im = FindAnyObjectByType<InputManager>(); //from the script InputManager
        rend = GetComponentInChildren<Renderer>();// 
        normalColor = rend.material.color;        // at the beginning start with normal color // normalColor= rend.material.color

    }
    private void OnMouseDown() {
        print("Hello, I'm " + name);              //works with left mouse click 
        im.NewSelection(this);
    }



}