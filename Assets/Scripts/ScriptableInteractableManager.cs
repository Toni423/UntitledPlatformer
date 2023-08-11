using UnityEngine;

[CreateAssetMenu(fileName = "InteractableManager", menuName = "ScriptableObjects/ScriptableInteractableManager", order = 1)]
public class ScriptableInteractableManager : ScriptableObject {
    [SerializeField] private Texture2D interactHoverSprite;

    public Texture2D getInteractableHoverSprite() {
        return interactHoverSprite;
    }
}
