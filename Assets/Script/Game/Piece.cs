using UnityEngine;
using System.Collections;

public class Piece:Entity
{
	public int jumpSrt = 7;
	public int rotationSrt = 180;
	public int col;
	public int row;
	public string type;
	public bool align; //white = 0, black = 1.
	
	public Piece ()
	{
	}
	
	public override void Init (int x, int y, int z, float width, float height, float depth, Color color, PrimitiveType type)
	{
		//el base hace que llame al init del entity
		base.Init (x, y, z, width, height, depth, color, type);
		model.physicsActive = false;
		model.FreezeRigidBody (true, true, true, true, true, true);
		model.reciveLighting = true;
		
	}
	
	public override void Move ()
	{
		
	}
	
	public override void Update ()
	{
		base.Update ();
	}
	
	public void Jump ()
	{
		
	}
	
}
