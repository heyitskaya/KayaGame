/*
* Author: Isaiah Mann
* Description: Used to navigate the main menu
*/

using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	static bool FIRST_LOAD = true;
	public GameObject NewGameButton;
	public GameObject NewGameConfirmationPanel;

	void Start () {
		if (FIRST_LOAD) {
			FIRST_LOAD = false;
			EventController.Event("menuMusicStart");
		}
	}

	public void LaunchGame () {

		SceneController.LoadMainGame();

	}

	public void LaunchTutorial () {
		SceneController.LoadTutorialScene();
	}
		
	public void LoadOptionsMenu () {

		SceneController.LoadOptionsMenu();

	} 

	public void LoadDevelopScenesList () {

		SceneController.LoadDevelopSceneListScene();

	}

<<<<<<< HEAD
	public void ShowConfirmationPanel(){
		NewGameConfirmationPanel.SetActive (true);
	}

	public void HideConfirmationPanel(){

		NewGameConfirmationPanel.SetActive (false);
	}

	public void StartNewGame(){
		//reset the data and start a new game
		//1. reset data
		new SaveLoad().ClearSave(); 
		//2. start a new game
		LaunchGame();

	}


=======
	public void LoadCreditsMenu () {
		SceneController.LoadCredits();
	}

>>>>>>> d0705662f2e4a66613c6be9472f7c670e2a20c7d
}