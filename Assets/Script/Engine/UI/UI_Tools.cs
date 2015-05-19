using UnityEngine;
using System.Collections;

public class UI_Tools
{

    /// <summary>
    /// Metodo que crea un mateial
    /// </summary>
    /// <param name="pName">Nombre del material</param>
    /// <param name="pTexture">Textura del material</param>
    /// <param name="pShader">Ruta del Shader del Material</param>
    /// <returns></returns>
    public static Material GetMaterial(string pName, Texture pTexture, string pShader = ShaderPath.UNLIT_TRANSPARENT)
    {
        Material mat = new Material(Shader.Find(pShader));
        mat.mainTexture = pTexture;
        mat.name = pName;
        return mat;

    }

    /// <summary>
    /// Metodo que carga una textura de la carpeta Resources
    /// </summary>
    /// <param name="pTextureName">"Nombre de la Textura a buscar"</param>
    /// <param name="pTexturePath">"Ruta de la textura a buscar"</param>
    /// <returns></returns>
    public static Texture GetGUITexture(string pTextureName, string pTexturePath)
    {
        Texture2D text = (Texture2D)Resources.Load(pTexturePath + pTextureName, typeof(Texture2D));
        //Debug.Log(text.name);
        return text;
    }



    #region CREACION DE TEXTFIELDS

    /// <summary>
    /// Metodo que carga una fuente de resources
    /// </summary>
    /// <param name="pFontName">nombre de la Fuente</param>
    /// <param name="pFontPath">ruta de la fuente</param>
    /// <returns></returns>
    public static Font GetFont(string pFontName, string pFontPath)
    {
        Font font = (Font)Resources.Load(pFontPath + pFontName, typeof(Font));
        return font;
    }


    /// <summary>
    /// Metodo qeu convierte un color en hexadecima en Color32
    /// </summary>
    /// <param name="hex">codigo hexadecimal del color</param>
    /// <returns></returns>
    public static Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4,    2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }

    #endregion

}
