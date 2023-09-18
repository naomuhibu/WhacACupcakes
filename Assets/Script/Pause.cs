using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    bool isOnPause;
    public void PauseTheGame()
    {
        if (isOnPause)
        {
            Time.timeScale = 1;
            isOnPause = false;
            this.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            Time.timeScale = 0;
            isOnPause = true;
            this.gameObject.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }


    }
}
