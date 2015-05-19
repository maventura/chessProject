using UnityEngine;
using System.Collections;

public abstract class UI_Object : BaseObject
{

    private float _realX;
    private float _realY;

    public UI_Object()
    {
       
    }


    /// <summary>
    /// Posicion en X del Objeto
    /// </summary>s
    public float x
    {
        get
        {
            return _transform.position.x - width / 2;
        }
        set
        {
            _realX = value;
            base.x = value + width/2;
        }
    }


    /// <summary>
    /// Posicion en Y del Objeto
    /// </summary>
    public float y
    {
        get
        {
            return -_transform.position.y + height/2;
        }
        set
        {
            _realY = value ;
            base.y = -value - height / 2;
        }
    }

    /// <summary>
    /// Ancho del Objeto
    /// </summary>
    public float width
    {
        get
        {
            return _transform.localScale.x;
        }
        set
        {
            base.width = value;
            x = _realX;
        }
    }

    /// <summary>
    /// Alto del Objeto
    /// </summary>
    public float height
    {
        get
        {
            return _transform.localScale.y;
        }
        set
        {
            base.height = value;
            y = _realY;
        }
    }


}
