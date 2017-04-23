using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	private BoxCollider2D boundry = null;
	private playerController player = null;

	void Start () {

		StartCoroutine(this.GetPlayerRef());
		StartCoroutine(this.FollowPlayer());
	}

	public void BoundriesChanged(BoxCollider2D coll) {

		if(this.boundry != coll) {
			boundry = coll;
		}
	}

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
			this.player = FindObjectOfType<playerController>();
			yield return null;
		}
	}
}
