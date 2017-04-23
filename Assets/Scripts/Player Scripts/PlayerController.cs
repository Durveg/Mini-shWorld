using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	#region Constants
	public float MAX_VELOCITY_X = 5;
	public float MAX_INVULN_TIME = 0.5f;

	#region Public Variables
	public bool onLadder = false;

	public int health = 10;
	public int jumpCharges = 1;
	public int maxJumpCharges = 1;

	public float jumpHeight = 350;
	public float sideSpeed = 25;
	public float ladderSpeed = 2;

	public SpriteRenderer sprite = null;

	#region Private Variables
	private bool invuln = false;
	private bool applyUpForce = false;
	private bool applyRightForce = false;
	private bool applyLeftForce = false;
	private bool applyDownForce = false;
	private bool jump = false;

	private float gravityScale = 0;

	private Rigidbody2D rBody = null;
	private HookShot hookShot = null;
	private Sword sword = null;
	private UIManager uiManager = null;

	#region Delegates
	public delegate void BoundriesUpdated(Collider2D coll);
	public event BoundriesUpdated boundriesUpdated;

	#region Public Methods
	public void ZeroOutVelocity() {

		this.rBody.velocity = Vector2.zero;
	}

	public void AdjustHealth(float adjustment) {

		this.health += (int)(adjustment * 2);
		this.uiManager.AdjustHearts(adjustment);
	}

	public void HealthRegen(float healthRegen) {

		this.AdjustHealth(healthRegen);
	}

	public void TakeDamage(float damage, float knockBackForce, Vector2 damageLocation) {

		if(this.invuln == false) {
		
			Vector2 dir = (damageLocation - (Vector2)this.transform.localPosition).normalized * -1;
			this.rBody.AddForce(dir * knockBackForce, ForceMode2D.Force);

			this.AdjustHealth(-damage);

			StartCoroutine(this.InvulnTimer());
		}
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
	#endregion 

	#region Unity Methods
	void Start () {

		this.rBody = this.GetComponent<Rigidbody2D>();
		this.sprite = this.GetComponentInChildren<SpriteRenderer>();
		this.hookShot = this.GetComponentInChildren<HookShot>();
		this.sword = this.GetComponentInChildren<Sword>();

		this.uiManager = FindObjectOfType<UIManager>();

		this.gravityScale = this.rBody.gravityScale;

		StartCoroutine(this.CheckForBoundryManager());
	}

	void OnCollisionEnter2D(Collision2D coll) {

		//Debug.Log("Collider Entered");
		if(this.jumpCharges < this.maxJumpCharges) {

			this.jumpCharges = this.maxJumpCharges;
		}
	}
	#endregion

	#region Update Methods
	void Update () {

		if(this.hookShot.hookShotActive == false) {

			if(Input.GetKeyDown(KeyCode.Q)) {

				this.hookShot.fireHookShot();
			}

			if(Input.GetKeyDown(KeyCode.LeftShift)) {

				this.sword.SwingSword();
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
		else {

			this.rBody.velocity = Vector2.zero;
		}
	}

	void FixedUpdate() {



		if(this.applyLeftForce == false && this.applyRightForce == false && this.jumpCharges == this.maxJumpCharges) {

			this.rBody.velocity = new Vector2(0, this.rBody.velocity.y);
		}

		if(this.applyRightForce == true) {

			this.sprite.flipX = false;
			this.sword.FlipSword(false);
			this.rBody.AddForce(new Vector2(this.sideSpeed, 0));
		}
		else if(this.applyLeftForce == true) {

			this.sprite.flipX = true;
			this.sword.FlipSword(true);
			this.rBody.AddForce(new Vector2(-this.sideSpeed,0));
		}

		if(this.jump == true) {

			this.rBody.AddForce(new Vector2(0, jumpHeight));
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
	#endregion

	#region CoRoutines
	private IEnumerator InvulnTimer() {

		this.invuln = true;
		float timer = 0;
		while(true) {

			timer += Time.deltaTime;

			//TODO: Add flash effect to the invuln

			if(timer >= this.MAX_INVULN_TIME) {

				break;
			}

			yield return null;
		}

		this.invuln = false;
	}

	private IEnumerator CheckForBoundryManager() {

		while(true) {

			int mask = 1 << 10;
			RaycastHit2D hit = Physics2D.Raycast(this.transform.localPosition, Vector2.zero, 0, mask, -10, 10);
			if(hit != null && hit.collider != null) {

				this.boundriesUpdated(hit.collider);
			}

			yield return null;
		}
	}
	#endregion
}
