using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{

    public void onPlayClick()
    {
        SceneManager.LoadScene("Main");
    }

    public void onQuitClick()
    {
        Application.Quit();
    }

}
