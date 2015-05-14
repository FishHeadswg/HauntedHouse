/*
Author: Trevor Richardson
HouseTrigger.cs
02-24-2015

	Controls the trigger inside the house. The trigger (de)activates
	the lights on the 2nd floor and controls the audio tracks inside
	the house (BGM1 & BGM2).
	
 */

using UnityEngine;
using System.Collections;

public class HouseTrigger : MonoBehaviour
{
	// light controls
	public GameObject lights;
	private int triggerCount = 1;

	// audio controls
	public AudioSource bgm1;
	public AudioSource bgm2;
	private float fadeSpeed = 1.5f;
	public bool bgm1Flag = true;
	public bool bgm2Flag = false;
	public bool houseFlag = true;

	// interscript comm for triggers
	void SetFlag(bool setter)
	{
		houseFlag = setter;
	}

	// trigger audio and lights
	void OnTriggerEnter (Collider player)
	{
		// add a collider == player check if necessary later
		++triggerCount;
		// entering trigger area
		if (triggerCount % 2 == 0) {
			lights.SetActive (true);
			bgm1Flag = false;
			bgm2Flag = true;
			if (!bgm2.isPlaying)
				bgm2.Play();
		} 
		// exiting trigger area
		else {
			lights.SetActive (false);
			bgm1Flag = true;
			bgm2Flag = false;
		}
	}

	// init volume for fade in
	void Start() {
		bgm2.volume = 0;
	}

	/* control audio depending on which triggers/audio sources are active
	 * Lerp used for fading effect */
	void Update()
	{
		// entering room
		if (houseFlag && bgm1Flag == true)
			bgm1.volume = Mathf.Lerp (bgm1.volume, 1, fadeSpeed * Time.deltaTime);
		if (houseFlag && bgm2Flag == true)
			bgm2.volume = Mathf.Lerp (bgm2.volume, 1, fadeSpeed * Time.deltaTime);
		// exiting room
		if (houseFlag && bgm1Flag == false)
			bgm1.volume = Mathf.Lerp (bgm1.volume, 0, fadeSpeed * Time.deltaTime);
		if (houseFlag && bgm2Flag == false)
			bgm2.volume = Mathf.Lerp (bgm2.volume, 0, fadeSpeed * Time.deltaTime);
	}

}
