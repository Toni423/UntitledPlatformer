using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitDoor : MonoBehaviour
{
   
    private void OnTriggerStay2D(Collider2D other) {
        if (Input.GetKey(KeyCode.W) && other.gameObject.CompareTag("Player")) {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
