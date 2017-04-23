using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWall : MonoBehaviour {

	#region Public Methods
	public void BlownUp() {

		GameObject.Destroy(this.gameObject);
	}
	#endregion
}
