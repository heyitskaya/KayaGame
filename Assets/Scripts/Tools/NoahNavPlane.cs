﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NoahNavPlane : MonoBehaviour, IPointerClickHandler {

	NavMeshAgent Player;
	// Use this for initialization
	void Start () {
		Player = GameManager.PlayerCharacter.GetComponent<NavMeshAgent>();
		//Player = GameObject.Find ("Sadie").GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){
		//Debug.Log ("Mouse UP");
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		#if DEBUG
		Debug.Log("Clicked on Nav Floor.");
		#endif
		NavMeshPath path = new NavMeshPath ();
		Vector3 destination = eventData.pointerCurrentRaycast.worldPosition;
		Player.GetComponent<NavMeshAgent> ().CalculatePath (destination, path);
		//if(path.status == NavMeshPathStatus.PathComplete){
			Player.SetDestination (destination);
		//}
	}
}
