using UnityEngine;
using System.Collections;

public class Board:Entity
{
    public int jumpSrt = 7;
    public int rotationSrt = 180;
	public string tileColor;
	public int selecCol = 0;
	public int selecRow = 0;
	//public DvPrimitiveObject Selector = new DvPrimitiveObject(PrimitiveType.Cube);

    public int[,] pieceMatrix = new int[8,8];
	float modelX;
	float modelZ;

//Init(int x , int y, int z, float width, float height , float depth , Color color, PrimitiveType type)

	public Board()
	{
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				pieceMatrix[i,j] = -1;
			}
		}
	}
	

	public override void Init(int x, int y, int z, float width, float height, float depth, Color color, PrimitiveType type)
    {
    	for(int c = 0; c < 8; ++c)
    	{
    		for(int r = 0; r < 8; ++r)
    		{
    			if((c+r)%2 == 0)
				{
					tileColor = "000000";
				}else{
					tileColor = "ffffff";
				}
    			//el base hace que llame al init del entity
				base.Init(x+ c*10, y, z + r*10, width, height, depth, UI_Tools.HexToColor(tileColor), type);
					base.model.reciveLighting = true;



				}
        	model.physicsActive = false;
        	model.FreezeRigidBody(true, true, true, true, true, true);
    		
    	}
		base.Init(0, 0, 0, width+0.5f, 0.5f, depth+0.2f, UI_Tools.HexToColor("00ff00"), type);
		base.model.reciveLighting = true;
		base.model.collisionable = false;
	}

	public override void Move()
	{

			if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (!Game.turnAlign) {
				selecRow += 1;
				if(selecRow > 7) selecRow = 7;
				//model.z = (selecRow)*10;
			}
			else {
				selecRow -= 1;
				if(selecRow < 0) selecRow = 0;
				//model.z = (selecRow)*10;
			}


		} 
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (!Game.turnAlign) {
				selecRow -= 1;
				if(selecRow < 0) selecRow = 0;
				//model.z = (selecRow)*10;
			}
			else {
				selecRow += 1;
				if(selecRow > 7) selecRow = 7;
				//model.z = (selecRow)*10;
			}

		} 
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (!Game.turnAlign) {
			selecCol -= 1;
			if(selecCol < 0) selecCol = 0;
			//model.x = (selecCol)*10;
			}
			else {
				selecCol += 1;
				if(selecCol > 7) selecCol = 7;
				//model.x = (selecCol)*10;
			}
		} 
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			if (!Game.turnAlign) {
			selecCol += 1;
			if(selecCol > 7) selecCol = 7;
			//model.x = (selecCol)*10;
			}
			else {
				selecCol -= 1;
				if(selecCol < 0) selecCol = 0;
				//model.x = (selecCol)*10;
			}
		}
	}
	
	public override void Update()
	{

	
		base.Update();

    }

	public void updateSelector(){
		model.x += (selecCol*10 - model.x)*0.3f;
		model.z += (selecRow*10 - model.z)*0.3f;
	}


}
