using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPause()
    {
        if (!SceneManager.GetSceneByName("PauseMenu").isLoaded)
        {
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            SceneManager.UnloadSceneAsync("PauseMenu");
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
