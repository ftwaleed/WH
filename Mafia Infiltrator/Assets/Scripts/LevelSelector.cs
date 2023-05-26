using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public int level;
    public string subjectName;

    void Start()
    {
        
    }

    public void OpenScene(){
        SceneManager.LoadScene(subjectName + " Level "+level.ToString());
    }
}
