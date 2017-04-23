using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Image[] hearts = new Image[10];
	private int heartsLeft = 10;

	public void AdjustHearts(float adjustments) {

		heartsLeft += (int)(adjustments * 4);
		if(heartsLeft > 10) {
			
			heartsLeft = 10;
		}
		else if(heartsLeft < 0) {

			heartsLeft = 0;
		}

		UpdateHearts();
	}

	private void UpdateHearts() {

		for(int i = 0; i <= 9; i++) {

			if(i < heartsLeft) {
				hearts[i].enabled = true;
			}
			else {
				hearts[i].enabled = false;
			}
		}
	}
}
