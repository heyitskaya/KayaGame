using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NoahNavPlane : MonoBehaviour, IPointerClickHandler {

	NavMeshAgent Player;

	public bool flipped = false; //when sadie is facing right flipped is false
	public bool startMoving = false;
	private SpeechBubble speechBubble;

	// Use this for initialization
	void Start () {
		Player = GameManager.PlayerCharacter.GetComponent<NavMeshAgent>();
		speechBubble = Player.GetComponentInChildren<SpeechBubble>();
		//Player = GameObject.Find ("Sadie").GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Player.remainingDistance <= Player.stoppingDistance) {
			if (startMoving ) {
				startMoving = false;
				Debug.Log ("Stopped moving");
				//check x then flip

				if (Player.transform.position.x < 4.5f && Player.transform.position.x > 0f && startMoving==false) { //and we have stopped moving
					Debug.Log("flip1");
					if (flipped) { //if sadie is facing left
						Flip (); //flip it sadie is facing right
					}

				} 
				if (Player.transform.position.x < 11.333f && Player.transform.position.x > 8f && startMoving == false) {
					Debug.Log ("flip2");
					if (!flipped) { //if sadie is facing right
						Flip (); //flip it sadie is now facing left
					}
				}
			}
		}
	}

	void OnMouseUp(){
		//Debug.Log ("Mouse UP");
	}
	void Flip(){ //Flip player character
		flipped = !flipped;
		Player.transform.localScale = new Vector3 (Player.transform.localScale.x * -1, Player.transform.localScale.y, Player.transform.localScale.z);
		speechBubble.transform.localScale = new Vector3(speechBubble.transform.localScale.x * -1, speechBubble.transform.localScale.y, speechBubble.transform.localScale.z);
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if (!UIManager._instance.paused){
			startMoving = true;
			#if DEBUG
			Debug.Log("Clicked on Nav Floor.");
			#endif
			NavMeshPath path = new NavMeshPath ();
			Vector3 destination = eventData.pointerCurrentRaycast.worldPosition;
			Player.GetComponent<NavMeshAgent> ().CalculatePath (destination, path);
			//if(path.status == NavMeshPathStatus.PathComplete){
			if (destination.x > Player.transform.position.x) { //destination is to right of Sadie   sadie   destination
				Debug.Log("We should be in here");
				if (flipped) { // if sadie is facing left
					Flip (); //sadie is now facing right
				}
			} else { //when destination is on Sadie's left
				if (!flipped) { //sadie is facing right
					Flip (); //sadie is now facing left
				}
			}

			Player.SetDestination (destination);
			//}

		}
	}
}
