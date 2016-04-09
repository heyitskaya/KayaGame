using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NoahNavPlane : MonoBehaviour, IPointerClickHandler {

	NavMeshAgent Player;

	public bool flipped = false; //when sadie is facing right flipped is false
	public bool startMoving = false;
	private SpeechBubble speechBubble;
	public GameObject leftObstacle;
	public GameObject rightObstacle;
	public decimal ratio=0.2m;

	public decimal leftBound;
	public decimal rightBound;
	public decimal boundDist;
	public decimal distance;
	// Use this for initialization
	void Start () {
		Player = GameManager.PlayerCharacter.GetComponent<NavMeshAgent>();
		speechBubble = Player.GetComponentInChildren<SpeechBubble>();
		//Player = GameObject.Find ("Sadie").GetComponent<NavMeshAgent>();
		leftBound=(decimal)(-leftObstacle.transform.position.x);
		rightBound =(decimal)( -rightObstacle.transform.position.x);
		distance = rightBound - leftBound;
		boundDist = decimal.Multiply (distance, ratio); // i think this would solve it
	

	}
	
	// Update is called once per frame
	void Update () {
		if (Player.remainingDistance <= Player.stoppingDistance) {
			if (startMoving ) {
				startMoving = false;
				Debug.Log ("Sadie position " + Player.transform.position.x);
			/**	Debug.Log("distance "+ distance);
				Debug.Log ("ratio " + ratio); **/
				//why is our bound distance 0?
				Debug.Log ("boundDist " + boundDist);
				Debug.Log("leftBound+boundDist "+(leftBound+boundDist));
				Debug.Log ("leftBound " + leftBound);
				Debug.Log("rightBound-boundDist "+(rightBound-boundDist));
				Debug.Log("rightBound "+ rightBound); 
				//on the left side
				if ((decimal)Player.transform.position.x <(leftBound+boundDist)/** && (decimal)Player.transform.position.x>leftBound**/ && startMoving==false) { //and we have stopped moving
					
					if (flipped) { //if sadie is facing left
						Flip (); //flip it sadie is facing right
					}

				} 
				//6.2  used to be 8f
				//on the right side
				if (/**(decimal)Player.transform.position.x < rightBound &&**/ (decimal)Player.transform.position.x > (rightBound-boundDist) && startMoving == false) {
					//this is fine

					if (!flipped) { //if sadie is facing right
						Flip (); //flip it sadie is now facing left
					}
				}
			}
		}
	}

	void OnMouseUp(){
		
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
			//Debug.Log("Clicked on Nav Floor.");
			#endif
			NavMeshPath path = new NavMeshPath ();
			Vector3 destination = eventData.pointerCurrentRaycast.worldPosition;
			Player.GetComponent<NavMeshAgent> ().CalculatePath (destination, path);
			//if(path.status == NavMeshPathStatus.PathComplete){
			if (destination.x > Player.transform.position.x) { //destination is to right of Sadie   sadie   destination
				
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
