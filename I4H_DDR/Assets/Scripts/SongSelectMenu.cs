using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongSelectMenu : MonoBehaviour
{
    public void GoToConfiguration() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Moved to configuration scene");
    }
}
