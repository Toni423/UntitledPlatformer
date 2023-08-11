using UnityEngine;

public class Bookshelf : Interactable {

    [SerializeField] private SriptableDialogueManager dialogueManager;
    [SerializeField] private string[] text;
    
    protected override void onInteract() {
        dialogueManager.setText(text);
        dialogueManager.startDialogue();
    }
}
