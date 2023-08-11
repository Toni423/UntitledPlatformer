using UnityEngine;

public abstract class Interactable : MonoBehaviour {

    [SerializeField] private ScriptableInteractableManager interactableManager;
    private Texture2D hoverSprite;

    private void Awake() {
        hoverSprite = interactableManager.getInteractableHoverSprite();
    }

    protected abstract void onInteract();

    private void OnMouseDown() {
        onInteract();
    }


    private void OnMouseEnter() {
        Cursor.SetCursor(hoverSprite, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit() {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
