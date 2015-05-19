using UnityEngine;
using System.Collections;

public abstract class BaseObject 
{
    protected internal GameObject model;
    protected Transform _transform;
    protected Renderer _renderer;
    protected bool _collisionable = true;
    protected bool _visible = true;

    protected virtual void Init()
    {
        _transform = model.GetComponent<Transform>();
        _renderer = model.GetComponent<Renderer>();
    }

    #region PROPIEDADES

    /// <summary>
    /// Posicion en X del Objeto
    /// </summary>s
    public float x
    {
        get
        {
            if (model == null) return 0;
            return _transform.position.x;
        }
        set
        {
            if (model == null) return;
            Vector3 pos = _transform.position;
            pos.x = value;
            _transform.position = pos;
        }
    }


    /// <summary>
    /// Posicion en Y del Objeto
    /// </summary>
    public float y
    {
        get
        {
            if (model == null) return 0;
            return _transform.position.y;
        }
        set
        {
            if (model == null) return;
            Vector3 pos = _transform.position;
            pos.y = value;
            _transform.position = pos;
        }
    }

    /// <summary>
    /// Posicion en Z del objeto
    /// </summary>
    public float z
    {
        get
        {
            if (model == null) return 0;
            return _transform.position.z;
        }
        set
        {
            if (model == null) return;
            Vector3 pos = _transform.position;
            pos.z = value;
            _transform.position = pos;
        }
    }


    /// <summary>
    /// Ancho del Objeto
    /// </summary>
    public float width
    {
        get
        {
            if (model == null) return 0;
            return _transform.localScale.x;
        }
        set
        {
            if (model == null) return;
            Vector3 scale = _transform.localScale;
            scale.x = value;
            _transform.localScale = scale;
        }
    }

    /// <summary>
    /// Alto del Objeto
    /// </summary>
    public float height
    {
        get
        {
            if (model == null) return 0;
            return _transform.localScale.y;
        }
        set
        {
            if (model == null) return;
            Vector3 scale = _transform.localScale;
            scale.y = value;
            _transform.localScale = scale;
        }
    }

    /// <summary>
    /// Profundidad del Objeto
    /// </summary>
    public float depth
    {
        get
        {
            if (model == null) return 0;
            return _transform.localScale.z;
        }
        set
        {
            if (model == null) return;
            Vector3 scale = _transform.localScale;
            scale.z = value;
            _transform.localScale = scale;
        }
    }

    /// <summary>
    /// Activa la colision del objeto
    /// </summary>
    public bool collisionable
    {
        get
        {
            return _collisionable;
        }
        set
        {

            if (model == null) return;
            _collisionable = value;
            if (_collisionable) _transform.GetComponent<Collider>().enabled = true;
            else _transform.GetComponent<Collider>().enabled = false;
        }
    }

    /// <summary>
    /// Renderizado de objeto
    /// </summary>
    public bool visible
    {
        get
        {
            return _visible;
        }
        set
        {
            if (model == null) return;
            _visible = value;
            if (_visible) _renderer.enabled = true;
            else _renderer.enabled = false;
        }
    }


    /// <summary>
    /// Rotacion en Y del objeto
    /// </summary>
    public float rotationY
    {
        get
        {
            if (model == null) return 0;
            return _transform.rotation.eulerAngles.y;
        }
        set
        {
            if (model == null) return;
            Vector3 localAngles = _transform.rotation.eulerAngles;
            localAngles.y = value;
            _transform.eulerAngles = localAngles;
        }
    }


    /// <summary>
    /// Rotacion en X del objeto (Momentaneamente fuera de servicio :D)
    /// </summary>
    public float rotationX
    {
        get
        {
            if (model == null) return 0;
            return _transform.rotation.eulerAngles.x;
        }
        set
        {
            if (model == null) return;
            Vector3 localAngles = _transform.rotation.eulerAngles;
            localAngles.x = value;
            //_transform.Rotate(value, 0, 0);

            _transform.rotation = Quaternion.Euler(localAngles);
            // _transform.eulerAngles = localAngles;
        }
    }


    /// <summary>
    /// Rotacion en Z del objeto
    /// </summary>
    public float rotationZ
    {
        get
        {
            if (model == null) return 0;
            return _transform.rotation.eulerAngles.z;
        }
        set
        {
            if (model == null) return;
            Vector3 localAngles = _transform.rotation.eulerAngles;
            localAngles.z = value;
            _transform.eulerAngles = localAngles;
        }
    }


    /// <summary>
    /// Nombre del objeto
    /// </summary>
    public string name
    {
        get
        {
            if (model == null) return "null";
            return model.name;
        }
        set
        {
            if (model == null) return ;
            model.name = value;
        }
    }


    #endregion

}
