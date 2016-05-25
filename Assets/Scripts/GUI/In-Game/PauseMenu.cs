using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public CanvasGroup pauseMenu;
    public Slider volumeSlider;
    public Slider sensitivitySlider;
    public GameObject player;

    void Start()
    {
        volumeSlider.value = AudioListener.volume;
        sensitivitySlider.value = 100;
    }

    public void changeSensitivity()
    {
        player.GetComponent<CharacController>().sensitivity = sensitivitySlider.value;
        player.GetComponentInChildren<CameraController>().sensitivity = sensitivitySlider.value;
    }

    public void changeSound()
    {
        AudioListener.volume = volumeSlider.value;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.alpha == 0)
        {
            Debug.Log("activate pause");
            pauseMenu.alpha = 1;
            pauseMenu.blocksRaycasts = true;
            UnityEngine.Cursor.visible = true;
            pauseMenu.interactable = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.alpha == 1)
        {
            Debug.Log("desactivate pause");
            pauseMenu.alpha = 0;
            pauseMenu.blocksRaycasts = false;
            pauseMenu.interactable = false;
            UnityEngine.Cursor.visible = false;
        }
    }
}
