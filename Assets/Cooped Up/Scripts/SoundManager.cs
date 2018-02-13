using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public List<AudioClip> sounds = new List<AudioClip>();

	public AudioSource source;

	public void PlaySound(AudioClip clip) {
		source.PlayOneShot(clip);
	}

	public void PlayJump() {
		PlaySound(sounds[0]);
	}
}
