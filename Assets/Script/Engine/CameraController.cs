using UnityEngine;
using System.Collections;

public class CameraController  
{

    private static Transform _camTransform;
    private static Camera _cam;

    /// <summary>
    /// Lleva la camara a una posicion
    /// </summary>
    /// <param name="x"> posicion en X</param>
    /// <param name="y"> posicion en y</param>
    /// <param name="z"> posicion en Z</param>
    public static void SetCameraPosition(float x, float y, float z)
    {
        if (_cam == null)  _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _cam.transform.position = new Vector3(x, y, z);
    }


    /// <summary>
    ///  Me da la posicion de la camara
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetCameraPosition()
    {
        if (_cam == null) _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (_camTransform == null) _camTransform = _cam.GetComponent<Transform>();
        return _camTransform.position;
    }


    /// <summary>
    /// La camara mira a un objetivo
    /// </summary>
    /// <param name="model"></param>
    public static void LookCameraToTarget(DvPrimitiveObject obj)
    {
        if (_cam == null) _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (_camTransform == null) _camTransform = _cam.GetComponent<Transform>();
        _camTransform.LookAt(obj.model.GetComponent<Transform>());
    }


    /// <summary>
    /// La camara mira a un objetivo
    /// </summary>
    /// <param name="model"></param>
    public static void FalowTarget(DvBaseObject obj ,float distance , float height)
    {
       
        if (_cam == null) _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (_camTransform == null) _camTransform = _cam.GetComponent<Transform>();
        if (obj == null || obj.model == null) return;
        Transform _target =  obj.model.GetComponent<Transform>();

        

        _camTransform.forward = _target.forward;

        _camTransform.position = _target.position;

        _camTransform.position -= _camTransform.forward * distance;

        _camTransform.position += _camTransform.up * height;


        //_camTransform.localPosition = position;

        _camTransform.LookAt(_target);
    }

    /// <summary>
    /// Rota La camara
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void CameraRotation(float x, float y, float z)
    {
        if (_cam == null) _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (_camTransform == null) _camTransform = _cam.GetComponent<Transform>();
        _camTransform.Rotate(x, y, z);
    }

    /// <summary>
    /// Mueve la camara
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void MoveCameraPosition(float x, float y, float z)
    {
        if (_cam == null) _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (_camTransform == null) _camTransform = _cam.GetComponent<Transform>();
        _camTransform.position += new Vector3(x, y, z);
    }

}
