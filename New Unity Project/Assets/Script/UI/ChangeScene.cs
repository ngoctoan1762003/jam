using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public float transitionTime;

    // Update is called once per frame
    void Update()
    {
        
        transitionTime -= Time.deltaTime;
        if(transitionTime < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
