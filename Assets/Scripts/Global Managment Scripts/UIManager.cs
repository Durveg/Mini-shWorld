using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	#region Public Variables
	public Image[] hearts = new Image[10];
	#endregion

	#region Private Variables
	private int heartsLeft = 10;
	#endregion

	#region Public Methods
	public void AdjustHearts(float adjustments) {

		this.heartsLeft += (int)(adjustments * 4);
		if(this.heartsLeft > 10) {
			
			this.heartsLeft = 10;
		}
		else if(this.heartsLeft < 0) {

			this.heartsLeft = 0;
		}

		UpdateHearts();
	}
	#endregion

	#region Private Methods
	private void UpdateHearts() {

		for(int i = 0; i <= 9; i++) {

			if(i < this.heartsLeft) {

				this.hearts[i].enabled = true;
			}
			else {
				
				this.hearts[i].enabled = false;
			}
		}
	}
	#endregion
}
