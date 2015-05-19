using UnityEngine;
using System.Collections;

public class UI_Camera : MonoBehaviour
{

    private static Camera _uiCamera;
    private static Transform _transform;
    private static bool _enabled = true;
    public static float screenWidth;
    public static float screenHeight;
    public static float cameraDepth = 5000;



	// Use this for initialization
	void Awake () 
    {
        _uiCamera = this.GetComponent<Camera>();
        _transform = this.GetComponent<Transform>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        _uiCamera.orthographicSize = screenHeight / 2;
        Vector3 camPosition = new Vector3();
        camPosition.x += screenWidth / 2;
        camPosition.y -= screenHeight / 2;
        camPosition.z = 5000;
       
        _transform.position = camPosition;
       
     
	}
	
    public static bool cameraEnabled
    {
        set
        {
            _enabled = value;
            if (_uiCamera != null) _uiCamera.enabled = value;
        }
        get
        {
            return _enabled;
        }
    }

    /*
    public static int screenWidth
    {

    }
     * 
     * */
}
