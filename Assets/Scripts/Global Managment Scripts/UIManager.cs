using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

	#region Public Variables
	public Image[] hearts = new Image[10];
	public Image gameOver = null;
	public Button restartButton = null;

	public Image Title = null;
	public Button startButton;

	public Image Win = null;
	#endregion

	#region Private Variables
	private int heartsLeft = 10;
	private int bossesDefeated = 0;
	#endregion

	#region Public Methods
	public void GameOver() {

		this.gameOver.enabled = true;
		this.restartButton.gameObject.SetActive(true);
	}

	public void RestartGame() {

		Application.LoadLevel("MainGame");
	}

	public void StartGame() {

		Title.enabled = false;
		this.startButton.gameObject.SetActive(false);
	}

	public void BossDefeated() {

		this.bossesDefeated++;
		if(bossesDefeated == 4) {

			Title.enabled = false;
			gameOver.enabled = false;
			Win.enabled = true;
			this.restartButton.gameObject.SetActive(true);
		}
	}

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

	#region Unity Methods
	void Start() {

		instance = this;
	}
	#endregion
}
