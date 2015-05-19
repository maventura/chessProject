using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour 
{
    public Game game = new Game();

	void Start ()
    {
        game.StartGame();
    }

	void Update () 
    {
        game.Update();
    }

}
