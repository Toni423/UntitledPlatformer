using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private float textSpeed;
    [SerializeField] private SriptableDialogueManager dialogueManager;

    private Coroutine currentWrite;
    private string[] text;
    private int nextLine = 0;


    private void Awake() {
        dialogueManager.setDialogueWindow(this);
        gameObject.SetActive(false);
    }


    private void OnEnable() {
        nextLine = 0;
        textField.text = "";
        text = dialogueManager.getText();
    }
    


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (currentWrite != null) {
                StopCoroutine(currentWrite);
            }
            textField.text = "";
            
            if (nextLine < text.Length) {
                currentWrite = StartCoroutine(writeLine(nextLine));
                nextLine++;
            }
            else {
                gameObject.SetActive(false);
            }
        }
    }



    private IEnumerator writeLine(int lineNum) {
        
        
        if (lineNum >= text.Length) {
            yield break;
        }

        foreach (char c in text[lineNum]) {
            textField.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        
    }


    
}
