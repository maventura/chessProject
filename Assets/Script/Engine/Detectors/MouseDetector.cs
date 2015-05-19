using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public delegate void MouseEventHandler();


public class MouseDetector : MonoBehaviour 
{

    protected MouseEventHandler [] _mouseClickFunction = new MouseEventHandler[6];

    public void AddEventListener(MouseEvent id, MouseEventHandler function)
    {
        _mouseClickFunction[(int)id] = function; 
    }

    public void RemoveEventListener(MouseEvent id)
    {
        _mouseClickFunction[(int)id] = null;
    }

    void OnMouseDown()
    {
        if (_mouseClickFunction[(int)MouseEvent.MOUSE_DOWN] != null) _mouseClickFunction[(int)MouseEvent.MOUSE_DOWN]();
    }

    void OnMouseUpAsButton()
    {
        if (_mouseClickFunction[(int)MouseEvent.MOUSE_CLICK] != null) _mouseClickFunction[(int)MouseEvent.MOUSE_CLICK]();
    }

    void OnMouseEnter()
    {
        if (_mouseClickFunction[(int)MouseEvent.MOUSE_ENTER] != null) _mouseClickFunction[(int)MouseEvent.MOUSE_ENTER]();
    }

    void OnMouseExit()
    {
        if (_mouseClickFunction[(int)MouseEvent.MOUSE_EXIT] != null)_mouseClickFunction[(int)MouseEvent.MOUSE_EXIT]();
    }

    void OnMouseOver()
    {
        if (_mouseClickFunction[(int)MouseEvent.MOUSE_OVER] != null) _mouseClickFunction[(int)MouseEvent.MOUSE_OVER]();
    }

}


public enum MouseEvent:int
{
    MOUSE_CLICK = 0,
    MOUSE_OVER,
    MOUSE_DOWN,
    MOUSE_ENTER,
    MOUSE_EXIT
}