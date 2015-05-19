using UnityEngine;
using System.Collections;


public sealed class DvPrimitiveObject : DvBaseObject
{

    public DvPrimitiveObject(PrimitiveType objectType)
    {
        model = GameObject.CreatePrimitive(objectType);
        Init();
    }


}
