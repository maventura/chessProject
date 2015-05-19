using UnityEngine;
using System.Collections;

public class Dv3dObject : DvBaseObject
{
    public Dv3dObject(string prefabName)
    {
        model = GameObject.Instantiate((GameObject)Resources.Load(DV_AssetsPath.PREFAB_PATH + prefabName, typeof(GameObject)));
        if (model == null) GameObject.CreatePrimitive(PrimitiveType.Cube);
        Init();
        _transform.position = new Vector3(0, 0, 0);
        model.AddComponent<MeshCollider>().convex = true;

    }


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

            Shader _unlitTransparentShader = Shader.Find(ShaderPath.UNLIT_TRANSPARENT_CUTOUT);
            Shader _TransparentShader = Shader.Find(ShaderPath.STANDARD);


            if (_reciveLighting) _renderer.material.shader = _TransparentShader;
            else _renderer.material.shader = _unlitTransparentShader;
        }
    }


}