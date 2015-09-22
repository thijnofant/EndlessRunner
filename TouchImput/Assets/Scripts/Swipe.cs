using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Swipe : MonoBehaviour
{

	public float Distance;
	public Text Direction;
	public float Speed;
	public float Gravity;
	public float JumpVelocity;
	public float DuckVelocity;
	public float DuckGravity;
	public float TurnSpeed;
	public bool Magnet;
	public float MagnetSize;

	private bool swiped;
	private bool jumping;
	private bool ducking;
	private bool turning;
	private string leftRight;
	private float originalTurn;
	private float lastY;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		magnets ();

		/*
		#region Tilt
		if (Input.acceleration.x < -0.2) {
			//Direction.text = "Tilted Left";
		} else if (Input.acceleration.x > 0.2) {
			//Direction.text = "Tilted Right";
		} else {
			//Direction.text = "Steady";
		}
		
		transform.Translate (Input.acceleration.x - 0.135f, 0, 0);
		#endregion
		*/

		#region Animations 
		if (jumping) {
			jump ();
		} else if (ducking) {
			duck ();
		} else if (turning) {
			//turn ();
		}

		#endregion

		#region Keyboard Input
		if (!(jumping || ducking || turning)) {
			if (Input.anyKey) {				
				if (!swiped) {
					if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
						Direction.text = "Right";
						turn ("Right");
						swiped = true;
					} else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
						Direction.text = "Left";
						turn ("Left");
						swiped = true;
					} else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
						Direction.text = "Up";
						jump ();
						swiped = true;
					} else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
						Direction.text = "Down";
						duck ();
						swiped = true;
					} else {
						moveForwards ();
					}
				} else {
					moveForwards ();
				}
			} else {
				swiped = false;
				moveForwards ();
			}
		}
		#endregion

		#region Swipes
		if (!(jumping || ducking || turning)) {
			if (Input.touchCount > 0) {

				Touch newTouch = Input.GetTouch (0);

				if (newTouch.phase == TouchPhase.Moved && !swiped) {
					if (newTouch.deltaPosition.x > Distance) {
						Direction.text = "Right";
						turn ("Right");
						swiped = true;
					} else if (newTouch.deltaPosition.x < -Distance) {
						Direction.text = "Left";
						turn ("Left");
						swiped = true;
					} else if (newTouch.deltaPosition.y > Distance) {
						Direction.text = "Up";
						jump ();
						swiped = true;
					} else if (newTouch.deltaPosition.y < -Distance) {
						Direction.text = "Down";
						duck ();
						swiped = true;
					} else {
						moveForwards ();
					}
				} else {
					moveForwards ();
				}
			} else {
				swiped = false;
				moveForwards ();
			}
		}
		#endregion
	}

	void moveForwards ()
	{
		transform.position += (transform.forward * (Speed * Time.deltaTime));
		Speed += 0.08f * Time.deltaTime;
	}

	void turn (string direction)
	{
		if (direction == "Left") {
			transform.Rotate (new Vector3 (0, - 90, 0));
		} else if (direction == "Right") {
			transform.Rotate (new Vector3 (0, 90, 0));
		}
	}
	
	void jump ()
	{
		if (!jumping) {
			jumping = true;
			lastY = JumpVelocity;
		} else if (transform.position.y <= 0) {
			transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
			jumping = false;
			moveForwards ();
			return;
		}
		transform.position += (transform.up * (lastY * Time.deltaTime));
		lastY -= Gravity;
		moveForwards ();
	}

	void duck ()
	{
		if (!ducking) {
			ducking = true;
			lastY = DuckVelocity;
		} else if (transform.localScale.y >= 1) {
			transform.position = new Vector3 (transform.position.x, 0f, transform.position.z);
			transform.localScale = new Vector3 (transform.localScale.x, 1, transform.localScale.z);
			ducking = false;
			moveForwards ();
			return;
		}
		transform.position += new Vector3 (0, - ((lastY * Time.deltaTime) / 2), 0);
		transform.localScale += new Vector3 (0, - (lastY * Time.deltaTime), 0);
		lastY -= DuckGravity;
		moveForwards ();
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Obstacle"))
		{
			print("Obstacle hit");
			Speed = Speed / 2f;
		}
	}

	void magnets(){
		/*
		MeshCollider bC = GetComponent<MeshCollider> ();
		if (Magnet == true) {
			bC.size = new Vector3 (MagnetSize, MagnetSize, MagnetSize);
		} else {
			bC.size = new Vector3 (1, 1, 1);
		}*/
	}


}







