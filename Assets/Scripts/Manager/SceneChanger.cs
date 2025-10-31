using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void Load(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Load(3);
            //GameManager.Instance.Sound.GetSoundEffect();
        }
    }
}
