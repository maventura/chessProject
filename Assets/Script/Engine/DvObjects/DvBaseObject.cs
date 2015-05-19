using UnityEngine;
using System.Collections;

public abstract class DvBaseObject : BaseObject
{
   
    protected Rigidbody _rigidBody;
    protected CollisionDetector _collisionDetector;
    protected MouseDetector _mouseDetector;

   
    protected bool _reciveLighting = false;
    protected bool _physicActive = false;
    protected RigidbodyConstraints _physicContrains;


    public DvBaseObject()
    {

    }

    protected override void Init()
    {
        base.Init();


        _collisionDetector = model.AddComponent<CollisionDetector>();
        _mouseDetector = model.AddComponent<MouseDetector>();

        _collisionDetector.Init();
        _rigidBody = model.GetComponent<Rigidbody>();
        _physicContrains = RigidbodyConstraints.FreezeAll;
        reciveLighting = true;

    }


    #region PROPIEDADES

  

    /// <summary>
    /// Habilita que el sea afectado por la iluminacion, 
    /// Utilizar en objetos con texturas
    /// </summary>
    public bool reciveLighting
    {
        get
        {
            return _reciveLighting;
        }
        set
        {
            if (model == null) return;
            _reciveLighting = value;

            Shader _unlitTransparentShader = Shader.Find(ShaderPath.UNLIT_COLOR);
            Shader _TransparentShader = Shader.Find(ShaderPath.STANDARD);
     

            if (_reciveLighting) _renderer.material.shader = _TransparentShader;
            else _renderer.material.shader = _unlitTransparentShader;
        }
    }


    /// <summary>
    /// Color del objeto
    /// </summary>
    public Color color
    {
        get
        {
            if (model == null) return Color.magenta;
            return _renderer.material.color;
        }
        set
        {
            if (model == null) return;
            _renderer.material.color = value;
        }
    }

    /// <summary>
    /// Activa o desactiva la fisica del objeto
    /// </summary>
    public bool physicsActive
    {
        get
        {
            return _physicActive;
        }
        set
        {
            if (model == null) return;
            _physicActive = value;

            if (_physicActive) _rigidBody.constraints = RigidbodyConstraints.None;
            else _rigidBody.constraints = _physicContrains;
        }
    }

    
    #endregion


    #region METODOS

    /// <summary>
    /// Metodo que chekea la colision con otro DV object
    /// </summary>
    /// <param name="obj">Objeto con el cual pregunto si esta colisionando</param>
    /// <returns></returns>
    public bool HitTestObject (DvBaseObject obj)
    {

        if (model == null || obj == null) return false;
        if (!collisionable || !obj.collisionable) return false;

        if (_collisionDetector.colliders.Contains(obj.model.GetComponent<Collider>()))
        {
            return true;
        }
        
        return false;
    }


    /// <summary>
    /// Metodo que aplica una fuerza sobre el objeto tomando como direccion un vector director en los ejes x,y y z
    /// *Nota: es necesario tener la fisica del objeto activada previamente
    /// </summary>
    /// <param name="xDir">Direccion en X del objeto, valores entre 0 y 1</param>
    /// <param name="yDir">Direccion en Y del objeto, valores entre 0 y 1</param>
    /// <param name="zDir">Direccion en Z del objeto, valores entre 0 y 1</param>
    /// <param name="power"></param>
    public void addForce(float xDir = 0, float yDir = 0, float zDir = 0 , float power = 1)
    {
        if (model == null) return;
        if (!_physicActive) return;
        xDir = Mathf.Clamp(xDir, 0, 1);
        yDir = Mathf.Clamp(yDir, 0, 1);
        zDir = Mathf.Clamp(zDir, 0, 1);

        _rigidBody.AddForce(new Vector3(xDir, yDir, zDir) * power, ForceMode.VelocityChange);
    }


    /// <summary>
    /// Freeza los ejes de movimiento y rotacion de un objeto con fisica
    /// </summary>
    /// <param name="model"> modelo</param>
    /// <param name="posX"> freezar posicion en X </param>
    /// <param name="posY"> freezar posicion en Y </param>
    /// <param name="posZ"> freezar posicion en Z </param>
    /// <param name="rotX"> freezar Rotacion en X </param>
    /// <param name="rotY"> freezar Rotacion en Y </param>
    /// <param name="rotZ"> freezar Rotacion en Z </param>
    public void FreezeRigidBody( bool posX = false, bool posY = false, bool posZ = false, bool rotX = false, bool rotY = false, bool rotZ = false)
    {
        if (model == null) return;

         _rigidBody.constraints = RigidbodyConstraints.None;

        if (posX)_rigidBody.constraints =_rigidBody.constraints
                                              | RigidbodyConstraints.FreezePositionX;

        if (posY) _rigidBody.constraints = _rigidBody.constraints
                                      | RigidbodyConstraints.FreezePositionY;

        if (posZ) _rigidBody.constraints = _rigidBody.constraints
                                      | RigidbodyConstraints.FreezePositionZ;


        if (rotX) _rigidBody.constraints = _rigidBody.constraints
                                      | RigidbodyConstraints.FreezeRotationX;


        if (rotY) _rigidBody.constraints = _rigidBody.constraints
                                      | RigidbodyConstraints.FreezeRotationY;


        if (rotZ) _rigidBody.constraints = _rigidBody.constraints
                                      | RigidbodyConstraints.FreezeRotationZ;


        _physicContrains = _rigidBody.constraints;

    }

    /// <summary>
    /// Metodo que agrega eventos para detectar el mouse
    /// </summary>
    /// <param name="eventID">Tipo de evento de mouse a agregar</param>
    /// <param name="function">Funcion que se ejecute cuando sucede el evento.
    /// *Nota*:Solo se admite una funcion por evento  </param>
    public void AddMouseEventListener(MouseEvent eventID, MouseEventHandler function)
    {
        _mouseDetector.AddEventListener(eventID, function);
    }

    /// <summary>
    /// Metodo que remueve algun evento de mouse sobre le objeto
    /// </summary>
    /// <param name="eventID">Evento a remover</param>
    public void RemoveMouseEventListener(MouseEvent eventID)
    {
        _mouseDetector.RemoveEventListener(eventID);
    }

    /// <summary>
    /// Metodo que permite mover un objeto segun sus coordenadas locales
    /// </summary>
    /// <param name="direction">direccion hacia donde mover el objeto</param>
    /// <param name="velocity">velocidad a la cual mover el objeto.  *Nota: no esta estimado el delta time*</param>
    public void Move(MoveDirection direction, float velocity)
    {
        if (model == null) return;
        switch (direction)
        {
            case MoveDirection.FORWARD:
                _transform.position += _transform.forward * velocity;
                break;
            case MoveDirection.BACK:
                _transform.position -= _transform.forward * velocity;
                break;
            case MoveDirection.RIGHT:
                _transform.position += _transform.right * velocity;
                break;
            case MoveDirection.LEFT:
                _transform.position -= _transform.right * velocity;
                break;
            case MoveDirection.UP:
                _transform.position += _transform.up * velocity;
                break;
            case MoveDirection.DOWN:
                _transform.position -= _transform.up * velocity;
                break;
            default:
                break;
        }
    }

    #endregion



    /// <summary>
    /// Destruye el Objeto de la escena
    /// </summary>
    public void Destroy()
    {
        if (model != null)
        {
            GameObject.Destroy(model);
            model = null;
        }
    }

}

public enum MoveDirection
{
    FORWARD,
    BACK,
    RIGHT,
    LEFT,
    UP,
    DOWN

}
