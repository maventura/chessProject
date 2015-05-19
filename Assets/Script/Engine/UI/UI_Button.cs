using UnityEngine;
using System.Collections;

public class UI_Button : UI_Object
{
    protected Material mat;
    protected Texture [] texture;
    protected MouseDetector _mouseDetector;
    protected bool _pressed;


    public UI_Button(string normalTexture , string overTexture = "" , string downTexture = "")
    {
        model = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Init();
        model.layer = DV_Layers.UI_OBJECT;
        z = UI_Camera.cameraDepth + 10;

        _mouseDetector = model.AddComponent<MouseDetector>();

        texture = new Texture[3];

        texture[DV_ButtonStates.NORMAL] = UI_Tools.GetGUITexture(normalTexture, DV_AssetsPath.TEXTURE_PATH);

        AddMouseEventListener(MouseEvent.MOUSE_EXIT, onMouseExit);
       

        if (overTexture != "")
        {
            texture[DV_ButtonStates.OVER] = UI_Tools.GetGUITexture(overTexture, DV_AssetsPath.TEXTURE_PATH);
            AddMouseEventListener(MouseEvent.MOUSE_OVER, onMouseOver);


        }
        if (downTexture != "")
        {
            texture[DV_ButtonStates.DOWN] = UI_Tools.GetGUITexture(downTexture, DV_AssetsPath.TEXTURE_PATH);
            AddMouseEventListener(MouseEvent.MOUSE_DOWN, onMouseDown);
            AddMouseEventListener(MouseEvent.MOUSE_CLICK, onMouseUp);

        }




        mat = UI_Tools.GetMaterial(name + " Material", texture[DV_ButtonStates.NORMAL]);

        _renderer.material = mat;
    }

    private void onMouseExit()
    {
        _renderer.material.mainTexture = texture[DV_ButtonStates.NORMAL];
    }


    private void onMouseDown()
    {
        _renderer.material.mainTexture = texture[DV_ButtonStates.DOWN];
        _pressed = true;
    }

    private void onMouseUp()
    {
        _renderer.material.mainTexture = texture[DV_ButtonStates.NORMAL];
        _pressed = false;
    }


    private void onMouseOver()
    {
        if (!_pressed) _renderer.material.mainTexture = texture[DV_ButtonStates.OVER];
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


}



