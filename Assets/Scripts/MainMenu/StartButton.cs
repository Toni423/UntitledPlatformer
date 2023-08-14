using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void clickStart() {
        SceneManager.LoadScene("Level1");
    }
}
