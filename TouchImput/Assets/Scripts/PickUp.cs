using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickUp : MonoBehaviour {

	private int count;
	public Text CountText;
	public float PowerUpDuration;

	private float time;
	private bool countingTime;

	// Use this for initialization
	void Start () {
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (countingTime) {
			if (time >= PowerUpDuration) {
				GetComponent<Swipe>().Magnet = false;
			}
			else{
				time += Time.deltaTime;
			}
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Coin"))
		{
			other.gameObject.SetActive(false);
			count++;
			playPickUpSound();
			updateScore();
		}
		if (other.gameObject.CompareTag("Magnet"))
		{
			other.gameObject.SetActive(false);
			GetComponent<Swipe>().Magnet = true;
		}/*
		if (other.gameObject.CompareTag("Obstacle"))
		{
			print("Obstacle hit");
			GetComponent<Swipe>().Speed = GetComponent<Swipe>().Speed / 2f;
		}*/
	}

	void updateScore(){
		CountText.text = "Score: " + count;
	}



	private void playPickUpSound(){
		AudioSource AS = GetComponent<AudioSource> ();
		AS.Play ();
	}
}
