using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    
    // Start is called before the first frame update
    public void ResetLevel()
    {
        // SceneManager.GetActiveScene().name
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
