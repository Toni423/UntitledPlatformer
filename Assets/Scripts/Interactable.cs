using UnityEngine;

public abstract class Interactable : MonoBehaviour {

    protected abstract void onInteract();

    private void OnMouseDown() {
        onInteract();
    }
}
