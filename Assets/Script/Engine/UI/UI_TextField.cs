using System;
using System.Collections;
using UnityEngine;

public class UI_TextField : UI_Object
{

	private string _text="";
    private int _size = 20;
    private string _color = "000000";
    private string _fontName = "Arial";
    private Font _font;
    private bool _bold = false;
    private TextMesh _mesh;
    private MeshRenderer _meshRenderer;
    private TextAlignment _aling;
    private Material _fontMaterial;

	public UI_TextField(string fontName = "Arial" , int size = 20 , string color = "000000"  )
	{
        //this.bold = bold;
       // this.fontName = fontName;
      



        model = new GameObject("TextField");
        Init();
        model.layer = DV_Layers.UI_OBJECT;
        z = UI_Camera.cameraDepth + 10;

        _mesh = model.AddComponent<TextMesh>();
        _meshRenderer = model.GetComponent<MeshRenderer>();



        this._fontName = fontName;
        this._font = UI_Tools.GetFont(_fontName, DV_AssetsPath.FONTS_PATH);
        _mesh.font = _font;

        this.size = size;
        this.color = color;
        this._fontMaterial = UI_Tools.GetMaterial(_font.name, _font.material.mainTexture, ShaderPath.TEXT_SHADER);

        this._meshRenderer.material = _fontMaterial;
        
    }


    #region PROPIEDADES

    /// <summary>
    /// Texto del textField
    /// </summary>
    public string text
    {
        get
        {
            return _text;
        }
        set
        {
            _text = value;
            _mesh.text = value;
        }
    }

    /// <summary>
    /// Nombre de la fuente, respetar el nombre de la carpeta Resources
    /// </summary>
    public string font
    {
        set
        {
            _fontName = value;
            _font = UI_Tools.GetFont(_fontName, DV_AssetsPath.FONTS_PATH);
           // Debug.Log(_font.name);

            _mesh.font = _font;
            _fontMaterial.mainTexture = _font.material.mainTexture;
        }
        get
        {
            return _fontName;
        }
    }

    /// <summary>
    /// Tamaño de la fuente
    /// </summary>
    public int size
    {
        get
        {
            return _size;
        }
        set
        {
            _size = value;
            _mesh.fontSize = _size;
            _mesh.characterSize = _size;
        }
    }

    /// <summary>
    /// Color en Hexa de la fuente
    /// </summary>
    public string color
    {
        get
        {
            return _color;
        }
        set
        {
            _color = value;
            Debug.Log(_mesh);
            _mesh.color = UI_Tools.HexToColor(_color);
        }
    }

    /// <summary>
    /// Negrita
    /// </summary>
    public bool bold
    {
        get
        {
            return _bold;
        }
        set
        {
            _bold = value;
            if (_mesh) _mesh.fontStyle = FontStyle.Bold;
            else _mesh.fontStyle = FontStyle.Normal;
        }
    }

    /// <summary>
    /// Alinenacion del textfield
    /// </summary>
    public TextAlignment aling
    {
        get
        {
            return _aling;
        }
        set
        {
            _aling = value;
            _mesh.alignment = _aling;
        }
    }
   
    #endregion

}

