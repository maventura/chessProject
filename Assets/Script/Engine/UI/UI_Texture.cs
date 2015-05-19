using UnityEngine;
using System.Collections;

public class UI_Texture : UI_Object 
{
    protected Material mat;
    protected Texture texture;


    public UI_Texture(string textureName , string name = "")
    {
        model = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Init();
        model.layer = DV_Layers.UI_OBJECT;
        z = UI_Camera.cameraDepth + 10;

        texture = UI_Tools.GetGUITexture(textureName, DV_AssetsPath.TEXTURE_PATH);
        mat = UI_Tools.GetMaterial(name + " Material", texture);

        _renderer.material = mat;

    }
}
