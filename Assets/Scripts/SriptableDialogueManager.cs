using UnityEngine;

[CreateAssetMenu(fileName = "DialogueManager", menuName = "ScriptableObjects/ScriptableDialogueManager", order = 1)]
public class SriptableDialogueManager : ScriptableObject {
    private string[] text;
    private float textSpeed;
    private Dialogue dialogue;

    public void setText(string[] newText) {
        text = newText;
    }

    public string[] getText() {
        return text;
    }

    public void startDialogue() {
        dialogue.gameObject.SetActive(true);
    }

    public void setDialogueWindow(Dialogue newDialogue) {
        dialogue = newDialogue;
    }


}
