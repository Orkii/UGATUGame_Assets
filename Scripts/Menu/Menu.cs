using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    //public List<Scene> scenes;
    public void ChangeScenes(int numberOfScenes)
    {
        SceneManager.LoadScene(numberOfScenes);

    }
}
