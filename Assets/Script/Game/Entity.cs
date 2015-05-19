using UnityEngine;
using System.Collections;

public class Entity 
{
    public DvPrimitiveObject model;
    public float velocity;

    public Entity()
    {

    }

    public virtual void Init(int x , int y, int z, float width, float height , float depth , Color color, PrimitiveType type)
    {
        model = new DvPrimitiveObject(type);
        model.x = x;
        model.y = y;
        model.z = z;
        model.width = width;
        model.height = height;
        model.depth = depth;
        model.color = color;
        model.reciveLighting = false;
    }

    public virtual void Move()
    {    
    }

    public virtual void Update()
    {
        Move();
    }

    public void Destroy()
    {
        model.Destroy();
    }
}
