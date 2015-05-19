using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{

    public UI_Texture hud;
    public UI_TextField texto;
    public UI_Button playButton;
	// Use this for initialization
	void Start ()
    {
		Application.LoadLevel(1);

		/*
        hud = new UI_Texture("Life Hud");
        hud.width = 400;
        hud.height = 55;
        hud.z += 1;
        hud.y = 200;
        */


        texto = new UI_TextField("Comic Sans MS", 15, "ff0000");
        texto.text = "MENU PRINCIPAL";

        playButton = new UI_Button("button_normal", "button_over", "button_down");
        playButton.width = 100;
        playButton.height = 50;
        playButton.x = 200;
        playButton.y = 200;


        playButton.AddMouseEventListener(MouseEvent.MOUSE_CLICK, onClickPlay);
	}

    private void onClickPlay()
    {
       // lo mando a gameplay
       // Application.LoadLevel(1);
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
