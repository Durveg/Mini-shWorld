using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	#region Private Variables
	private Collider2D boundry = null;
	private PlayerController player = null;
	#endregion

	#region Unity Methods
	void Start () {

		StartCoroutine(this.GetPlayerRef()); //Get the reference to the player in a coroutine incase the camera is loaded before the player
		StartCoroutine(this.FollowPlayer()); //Start following the player (does check for null pointers)
	}
	#endregion

	#region Private Methods
	/**
	 * Delegate method from the PlayerManager script, used to create the metroid style camera.
	 */
	private void BoundriesChanged(Collider2D coll) {

		if(this.boundry != coll) {
			this.boundry = coll;
		}
	}
	#endregion

	#region CoRoutines
	private IEnumerator FollowPlayer() {

		while(true) {

			if(this.boundry != null && this.player != null) {
			
				float vertExtent = Camera.main.orthographicSize;
				float horzExtent = vertExtent * Screen.width / Screen.height;

				float x = Mathf.Clamp(this.player.transform.position.x, this.boundry.bounds.min.x + horzExtent, this.boundry.bounds.max.x - horzExtent);
				float y = Mathf.Clamp(this.player.transform.position.y, this.boundry.bounds.min.y + vertExtent, this.boundry.bounds.max.y - vertExtent);
				float z = this.transform.localPosition.z;

				this.transform.localPosition = new Vector3(x, y, z);
			}

			yield return null;
		}
	}

	private IEnumerator GetPlayerRef() {

		while(this.player == null) {
			
			this.player = FindObjectOfType<PlayerController>();
			this.player.boundriesUpdated += this.BoundriesChanged;

			yield return null;
		}
	}
	#endregion
}
