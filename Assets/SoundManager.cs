using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;

	public AudioSource jump;
	public AudioSource spider;
	public AudioSource step;
	public AudioSource hurt;
	public AudioSource explosion;
	public AudioSource health;
	public AudioSource venom;
	public AudioSource sword;

	void Start() {

		instance = this;
	}

	public void PlayVenom() {

		this.venom.Play();
	}

	public void PlaySword() {

		this.sword.Play();
	}

	public void PlayExplosion() {

		this.explosion.Play();
	}

	public void PlayHealth() {

		this.health.Play();
	}

	public void playJump() {

		this.jump.Play();
	}

	public void playHurt() {

		this.hurt.Play();
	}

	public void enableSpider(bool on) {

		if(on == true && this.spider.isPlaying == false) {

			this.spider.Play();
		}
		else if(on == false) {

			this.spider.Stop();
		}
	}

	public void enableStep(bool on) {

		if(on == true && this.step.isPlaying == false) {
			
			this.step.Play();
		}
		else if(on == false) {

			this.step.Stop();
		}
	}
}
