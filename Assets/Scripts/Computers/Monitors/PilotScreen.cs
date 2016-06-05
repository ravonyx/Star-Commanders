using UnityEngine;
using System.Collections;

public class PilotScreen : MonoBehaviour 
{
    [SerializeField]
    Camera _spaceshipCamera;

    [SerializeField]
    Renderer _screen;

	void Start () 
    {
        RenderTexture renderCamera = new RenderTexture(1024, 728, 0);
        renderCamera.antiAliasing = 8;
        _spaceshipCamera.targetTexture = renderCamera;
        _screen.material.mainTexture = renderCamera;
        _screen.material.shader = Shader.Find("Legacy Shaders/Diffuse");
        _screen.material.name = "ScreenShip";
	}
}
