using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    public bool gamePaused = false;
    public GameObject pausedMenu;

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Cancel"))
        {
            if (gamePaused == false)
            {
                Time.timeScale = 0;
                gamePaused = true;
                Cursor.visible = true;
                pausedMenu.SetActive(true);
            }
            else
            {
                pausedMenu.SetActive(false);
                Cursor.visible = false;
                gamePaused = false;
                Time.timeScale = 1;
            }
        }
	}

    public void UnpauseGame()
    {
        pausedMenu.SetActive(false);
        Cursor.visible = false;
        gamePaused = false;
        Time.timeScale = 1;
    }

}
