using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour {

	#region Public Variables
	public delegate void ZoneActivityChanged();
	public event ZoneActivityChanged zoneActivated;
	public event ZoneActivityChanged zoneDeactivated;
	#endregion

	#region Private Variables 
	private BoxCollider2D coll = null;
	private bool zoneActive = false;
	#endregion

	#region Private Methods
	private void BoundriesChanged (Collider2D coll) {

		if(this.coll == (BoxCollider2D)coll && this.zoneActive == false) {

			this.zoneActive = true;
			if(this.zoneActivated != null) {
				this.zoneActivated();
			}
		}
		else if(this.coll != (BoxCollider2D)coll && this.zoneActive == true){

			this.zoneActive = false;
			if(this.zoneDeactivated != null) {
				this.zoneDeactivated();
			}
		}
	}
	#endregion

	#region Unity Methods
	void Start () {

		this.coll = this.GetComponent<BoxCollider2D>();
		StartCoroutine(this.GetPlayerRef());
	}
	#endregion

	#region CoRoutines
	private IEnumerator GetPlayerRef() {

		while(true) {

			PlayerController player = FindObjectOfType<PlayerController>();
			if(player != null) {

				player.boundriesUpdated += this.BoundriesChanged;
				break;
			}

			yield return null;
		}
	}
	#endregion
}
