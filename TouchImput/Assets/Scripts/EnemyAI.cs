using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	private bool spawned;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindWithTag("Player");
		transform.LookAt (player.transform.position);
		float speed = player.GetComponent<Swipe>().Speed;



		/*
		if (speed < 4) {
			//kill
		} else */if (speed < 7) {
			if (!spawned) {
				GameObject SP = GameObject.FindWithTag("SpawnPoint");
				transform.position = SP.transform.position;
				spawned = true;
			}
			float distance = Vector3.Distance(transform.position, player.transform.position);

			if (distance >= 6) {
				moveForwards(speed+7);
			}
			else {
				moveForwards(speed+1);
			}
			

			//move into frame
		} else if (speed >= 7 && spawned) {
			float distance = Vector3.Distance(transform.position, player.transform.position);
			if (distance >= 6) {
				spawned = false;
			}
			else{
				moveForwards(speed-1);
			}

			//move out of frame
			//stop movement
		}
	}

	void moveForwards (float movementSpeed)
	{
		transform.position += (transform.forward * (movementSpeed * Time.deltaTime));
	}

	void moveAfterObject(GameObject gameObject){
		transform.position = gameObject.transform.position - new Vector3 (0, 0, 4);
	}
}
