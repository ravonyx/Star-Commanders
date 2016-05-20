using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public CanvasGroup pauseMenu;
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.value = AudioListener.volume;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.alpha == 0)
        {
            Debug.Log("activate pause");
            pauseMenu.alpha = 1;
            UnityEngine.Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.alpha == 1)
        {
            Debug.Log("desactivate pause");
            pauseMenu.alpha = 0;
            UnityEngine.Cursor.visible = false;
        }
    }
}
