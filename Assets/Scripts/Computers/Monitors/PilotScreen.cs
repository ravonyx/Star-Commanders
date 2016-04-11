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
        _spaceshipCamera.targetTexture = renderCamera;
        _screen.material.mainTexture = renderCamera;
        _screen.material.shader = Shader.Find("Legacy Shaders/Self-Illumin/Bumped Diffuse");
	}
}
