using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	public float jumpHeight = 350;
	public float sideSpeed = 25;
	public const float MAX_VELOCITY_X = 5;

	private bool applyUpForce = false;
	private bool applyRightForce = false;
	private bool applyLeftForce = false;
	private bool applyDownForce = false;

	public int jumpCharges = 1;
	public int maxJumpCharges = 1;
	private bool jump = false;

	private Rigidbody2D rBody = null;
	public SpriteRenderer sprite = null;
	private HookShot hookShot = null;

	private float gravityScale = 0;
	public float ladderSpeed = 2;
	public bool onLadder = false;

	// Use this for initialization
	void Start () {

		this.rBody = this.GetComponent<Rigidbody2D>();
		this.sprite = this.GetComponentInChildren<SpriteRenderer>();
		this.hookShot = this.GetComponentInChildren<HookShot>();

		this.gravityScale = this.rBody.gravityScale;
	}

	// Update is called once per frame
	void Update () {

		if(this.hookShot.hookShotActive == false) {

			if(Input.GetKeyDown(KeyCode.Q)) {

				this.hookShot.fireHookShot();
			}

			if(Input.GetKeyDown(KeyCode.E)) {

				GameObject bomb = Instantiate(Resources.Load("Bomb") as GameObject);

				float multi = 1;
				if(this.sprite.flipX == true) {
					multi = -1;
				}

				bomb.transform.localPosition = new Vector2(this.transform.localPosition.x + (.5f * multi), this.transform.localPosition.y - .35f);
			}

			this.applyUpForce = false;
			if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			
				if(this.onLadder == true) {

					this.applyUpForce = true;
				}
			}

			this.applyRightForce = false;
			if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {

				this.applyRightForce = true;
				//TODO: Move to the right
			}

			this.applyLeftForce = false;
			if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {

				this.applyLeftForce = true;
				//TODO: Move left
			}

			this.applyDownForce = false;
			if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {

				if(this.onLadder == true) {

					this.applyDownForce = true;
				}
				//TODO: Crouch? Climb down ladders??
			}

			if(Input.GetKeyDown(KeyCode.Space)) {

				if(this.jumpCharges > 0 && this.onLadder == false) {

					this.jump = true;
				}
			}
		}
	}

	public void TakeDamage() {
		//TODO: Take Damage
	}

	public void PlayerOnLadder(bool onLadder) {

		this.onLadder = onLadder;
		if(onLadder == true) {
			
			this.rBody.gravityScale = 0;
		}
		else {
			
			this.rBody.gravityScale = this.gravityScale;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {

		Debug.Log("Collider Entered");
		if(this.jumpCharges < this.maxJumpCharges) {
		
			this.jumpCharges = this.maxJumpCharges;
		}
	}

	void FixedUpdate() {



		if(this.applyLeftForce == false && this.applyRightForce == false && this.jumpCharges == this.maxJumpCharges) {

			this.rBody.velocity = new Vector2(0, this.rBody.velocity.y);
		}

		if(this.applyRightForce == true) {

			this.sprite.flipX = false;
			this.rBody.AddForce(new Vector2(this.sideSpeed, 0));
		}
		else if(this.applyLeftForce == true) {

			this.sprite.flipX = true;
			this.rBody.AddForce(new Vector2(-this.sideSpeed,0));
		}

		if(this.jump == true) {

			this.rBody.AddForce(new Vector2(0,jumpHeight));
			this.jump = false;
			this.jumpCharges--;
		}

		if(this.applyUpForce == true) {

			this.rBody.AddForce(new Vector2(0, ladderSpeed));
		}
		else if(this.applyDownForce == true) {

			this.rBody.AddForce(new Vector2(0, -ladderSpeed));
		}
		else if(this.onLadder == true) {

			this.rBody.velocity = new Vector2(this.rBody.velocity.x, 0);
		}

		if(this.rBody.velocity.x > MAX_VELOCITY_X)
		{
			this.rBody.velocity = new Vector3(MAX_VELOCITY_X, this.rBody.velocity.y);
		}
		else if(this.rBody.velocity.x < -MAX_VELOCITY_X)
		{
			this.rBody.velocity = new Vector3(-MAX_VELOCITY_X, this.rBody.velocity.y);
		}
	}
}
