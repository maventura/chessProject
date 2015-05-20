 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game 
{
	public int turn = 0;
	public List<int[,] > movHistory = new List< int[,] >();
    public Piece piece = new Piece();
    public Piece[] pieces= new Piece[32];
    public Board board = new Board();
    public List<Entity> updateables = new List<Entity>();
	public bool pieceSelected = false;
	public static bool turnAlign = false;
	public int pieceId = 0;
	public string[,] types = new string[2, 8] { { "rock", "knight", "bishop", "king", "queen", "bishop", "knight", "rock" }, { "pawn","pawn","pawn","pawn","pawn","pawn","pawn","pawn" } };
	//public DvPrimitiveObject[,] models = new DvPrimitiveObject(PrimitiveType.Capsule);

	private float cameraTransition;
	
	public void StartGame()
    {
		board.Init(0, -250, 0, 10, 500, 10, Color.cyan, PrimitiveType.Cube);
		spawnPieces();
        updateables.Add(piece);
        updateables.Add(board);
    }

    public void Update()
	{	turn = movHistory.Count - 1;
		updateCamera ();
		captureKeys ();
		//el count es como el length
		for (int i = 0; i < updateables.Count; i++) {
			updateables [i].Update ();
		}

		board.updateSelector ();
	}

	public void moveSelectedPiece(int pieceId)
	{
		int[,] m = new int[8,8];
		copySquareMatrix (m, board.pieceMatrix);
		movHistory.Add (m);
		turnAlign = !turnAlign;
		board.pieceMatrix [pieces [pieceId].col, pieces [pieceId].row] = -1;
		board.pieceMatrix [board.selecCol, board.selecRow] = pieceId;
		pieces [pieceId].model.x = board.selecCol * 10;
		pieces [pieceId].model.z = board.selecRow * 10;
		pieces [pieceId].row = board.selecRow;
		pieces [pieceId].col = board.selecCol;

		if(pieces[pieceId].align == false)
		{
			pieces[pieceId].model.color = Color.white;
		}else{
			pieces[pieceId].model.color = Color.yellow;
		}
	}
	public void spawnPieces()
	{
		int idxw = 0;
		int idxb = 16;

		for(int r = 0; r < 2 ; ++r)
		{    
			for(int c = 0; c < 8 ; ++c)
			{

				//BLANCAS
				pieces[idxw] = new Piece();
				pieces[idxw].type = types[r,c];
				pieces[idxw].row = r;
				pieces[idxw].col = 7-c;
				pieces[idxw].align = false;

				if((c == 3 || c == 4) && (r == 0 || r == 7))
				{
					pieces[idxw].Init(70-c*10, 5, r*10, 5, 15, 5, Color.white, typeToModel(pieces[idxw].type));
				}else{
					pieces[idxw].Init(70-c*10, 5, r*10, 5, 5, 5, Color.white, typeToModel(pieces[idxw].type));
				}
				board.pieceMatrix[7-c,r] = idxw;


				//NEGRAS
				pieces[idxb] = new Piece();
				pieces[idxb].type = types[r,c];
				pieces[idxb].col = 7-c;
				pieces[idxb].row = 7-r;
				pieces[idxb].align = true;
				if((c == 3 || c == 4) && (r == 0 || r == 7))
				{
					pieces[idxb].Init(70-c*10, 5,70-r*10 , 5, 15, 5, Color.yellow, typeToModel(pieces[idxw].type));
				}else{
					pieces[idxb].Init(70-c*10, 5,70-r*10 , 5, 5, 5, Color.yellow, typeToModel(pieces[idxw].type));
				}
				board.pieceMatrix[7-c,7-r] = idxb;	

				idxw++;
				idxb++;

			}
		}
	


	}
	public void FinishGame()
	{
	}



	
	public bool validMov(string type, int x1, int y1, int x2, int y2)
	{
		bool canMove = false;
		switch (type)
		{
		case "pawn":
			canMove = pawnMov(x1, y1, x2, y2);
			break;
		case "rock":
			canMove = rockMov(x1, y1, x2, y2);
			break;
		case "bishop":
			canMove = diagMov(x1, y1, x2, y2);
			break;
		case "king":
			canMove = kingMov(x1, y1, x2, y2);
			break;
		case "queen":
			canMove = diagMov(x1, y1, x2, y2) || rockMov(x1, y1, x2, y2);
			break;
		case "knight":
			canMove = knightMov(x1, y1, x2, y2);
			break;
		default:
			return canMove;
		}
		return canMove;
	}
	
	public bool rockMov(int x1, int y1, int x2, int y2)
	{

		int step;
			if(x1 == x2)
			{
				step = sign(y2 - y1);

				int i = y1 + step;
				while(i != y2)
				{
					if(board.pieceMatrix[x1,i] != -1)
					{
						return false;
					}
						i += step;
				}
				return true;
			}
			else if(y1 == y2)
			{
				step = sign(x2 - x1);
				int i = x1 + step;
				while(i != x2)
				{
				if(board.pieceMatrix[i,y1] != -1)
				{
					return false;
				}
					i += step;
				}
				return true;
			}

		return false;
	}

	public bool diagMov(int x1, int y1, int x2, int y2)
	{
		if (x1 == x2 || y1 == y2) 
		{
			return false;
		}
		int stepX = sign (x2 - x1);
		int stepY = sign (y2 - y1);

		x1 += stepX;
		y1 += stepY;

		while ((x1 != x2) && (y1 != y2)) {
			if(board.pieceMatrix[x1,y1] != -1){
				return false;
			}
			x1 += stepX;
			y1 += stepY;
		}
		if ((x1  == x2) && (y1 == y2)) 
		{
			return true;
		} else {
			return false;
		}
	}

	public bool knightMov(int x1, int y1, int x2, int y2)
	{
		bool res = false;
		res = res || (x1+1==x2)&&(y1+2==y2);
		res = res || (x1+1==x2)&&(y1-2==y2);
		res = res || (x1+2==x2)&&(y1+1==y2);
		res = res || (x1+2==x2)&&(y1-1==y2);
		res = res || (x1-1==x2)&&(y1+2==y2);
		res = res || (x1-1==x2)&&(y1-2==y2);
		res = res || (x1-2==x2)&&(y1+1==y2);
		res = res || (x1-2==x2)&&(y1-1==y2);

		return res;
	}

	public bool pawnMov(int x1, int y1, int x2, int y2)
	{
		bool align = pieces [board.pieceMatrix [x1, y1]].align;
		int adv;
		if (align == false) {
			adv = 1;
		} else {
			adv = -1;
		}
		bool canMov;
		canMov = (x2 == x1 && y2 == y1 + adv) && (board.pieceMatrix [x2,y2] == -1);
		canMov = canMov || ((x2 == x1 + 1 || x2 == x1 - 1) && board.pieceMatrix [x2, y2] != -1) && (y1 + adv == y2);
		canMov = canMov || (align == false && y1 == 1 && y2 == 3 && x1 == x2) || (align == true && y1 == 6 && y2 == 4 && x1 == x2);
		return canMov;
	}

	public bool kingMov(int x1, int y1, int x2, int y2)
	{
		
		return (x2 <= x1+1) && (x2 >= x1-1) && (y2 <= y1+1) && (y2 >= y1-1);
	}


	int sign( int a)
	{
		if (a > 0) {
			return 1;
		} else if(a < 0)
		{
			return -1;
		}
		return 0;
	}

	void updateCamera(){
		//100*(Mathf.Cos (((cameraTransition + 20) / 140) * Mathf.PI));
		if (turnAlign == false) {
			if (cameraTransition> 0) {
				cameraTransition -= 1;
			}
		} else if (cameraTransition< 30){
			cameraTransition += 1;
		}
		CameraController.SetCameraPosition (40+100*(Mathf.Cos ((cameraTransition / 30) * Mathf.PI - Mathf.PI/2)), 80, 40 + 100*(Mathf.Sin ((cameraTransition / 30) * Mathf.PI - Mathf.PI/2)));
		CameraController.LookCameraToTarget (board.model);
	}

	void captureKeys(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel (0);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			
			
			if (pieceSelected) {
				int landingPiece = board.pieceMatrix [board.selecCol, board.selecRow];

				if (landingPiece != -1) {
					int x1 = pieces[pieceId].col;
					int y1 = pieces[pieceId].row;
					int x2 = pieces[landingPiece].col;
					int y2 = pieces[landingPiece].row;
					if ((pieces [landingPiece].align != pieces [pieceId].align) &&
					    validMov(pieces[pieceId].type, x1, y1, x2, y2)) { //todo: meter align en validMoves
						moveSelectedPiece (pieceId);
						pieceSelected = false;
						pieces [landingPiece].Destroy ();
					}
				} else {
					int x1 = pieces [pieceId].col;
					int y1 = pieces [pieceId].row;
					int x2 = board.selecCol;
					int y2 = board.selecRow;
					
					if (validMov (pieces [pieceId].type, x1, y1, x2, y2)) { //todo: meter align en validMoves
						moveSelectedPiece (pieceId);
						pieceSelected = false;
					}
				}
				pieceSelected = false;
				if(pieces[pieceId].align == false)
				{
					pieces[pieceId].model.color = Color.white;
				}else{
					pieces[pieceId].model.color = Color.yellow;
				}
				
			} else {
				pieceId = board.pieceMatrix [board.selecCol, board.selecRow];
				if(pieces[pieceId].align == turnAlign)
				{
					if (pieceId != -1) {
						pieceSelected = true;
						pieces [pieceId].model.color = Color.red;
					}
				}
			}
		}


		if (Input.GetKeyDown (KeyCode.Z)) {
			if (turn >= 0)
			{
				eliminateModels();
				turnAlign = !turnAlign;
				copySquareMatrix(board.pieceMatrix, movHistory[movHistory.Count - 1]);
				loadPiecesfromMatrix();
				Debug.Log (movHistory);
				movHistory.RemoveAt(movHistory.Count-1);
			}
		}





	}


	void eliminateModels(){
		for (int i = 0; i < pieces.Length; i++) {
			if(pieces[i].model != null) {
				pieces[i].Destroy();
			}
		}
	}


	void copySquareMatrix(int[,] a, int[,] b)
	{
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				a[i,j] = b[i,j];
			}
		}
	}


	void loadPiecesfromMatrix()
	{
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if(board.pieceMatrix[i,j] != -1)
				{
					pieces[board.pieceMatrix[i,j]].col = i;
					pieces[board.pieceMatrix[i,j]].row = j;
					Color c;
					if(pieces[board.pieceMatrix[i,j]].align) //0 if white, 1 if black;
					{
						c = Color.yellow;
					}else{
						c = Color.white;
					}
					int h;
					string t = pieces[board.pieceMatrix[i,j]].type;
					if(t == "queen" || t == "king") //0 if white, 1 if black;
					{
						h = 15;
					}else{
						h = 5;
					}
					pieces[board.pieceMatrix[i,j]].Init(10*i, 5, 10*j, 5, h, 5, c, typeToModel(t));
					 //pieces[idxw].Init(70-c*10, 5, r*10, 5, 5, 5, Color.white, models[r,c]);

				}
			}
		}

	}


	PrimitiveType	typeToModel(string t){

		switch (t)
		{
		
		case "pawn":
		case "queen":
			return PrimitiveType.Sphere;
		case "rock":
			return PrimitiveType.Cylinder;
		case "bishop":
			return PrimitiveType.Capsule;
		case "king":
		case "knight":
			return PrimitiveType.Cube;
		default:
			Debug.Log ("me diste mal el string");
				return PrimitiveType.Capsule;
		}

		


	}
}

