using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWall : MonoBehaviour {

	public void BlownUp() {

		GameObject.Destroy(this.gameObject);
	}
}
