/*
Author: Trevor Richardson
OutsideTrigger.cs
02-24-2015

	Controls the trigger between the house and outside. The trigger 
	controls the BGM1 and BGM3 audio tracks.
	
 */

using UnityEngine;
using System.Collections;

public class OutsideTrigger : MonoBehaviour
{
	// audio controls
	private int triggerCount = 1;
	public AudioSource bgm1;
	public AudioSource bgm3;
	private float fadeSpeed = 1.5f;
	public bool bgm1Flag = true;
	public bool bgm3Flag = false;

	// check if outside
	public bool outsideFlag = false;

	// communicate with other trigger
	public GameObject house;

	void OnTriggerEnter (Collider player)
	{
		++triggerCount;

		// entering trigger
		if (triggerCount % 2 == 0) {
			bgm1Flag = false;
			bgm3Flag = true;
			if (!bgm3.isPlaying)
				bgm3.Play();
			outsideFlag = true;
			// disable fading from house trigger
			house.SendMessage("SetFlag", false);
			// turn on fog effect
			RenderSettings.fog = true;
		} 
		// exiting triger
		else {
			bgm1Flag = true;
			bgm3Flag = false;
			outsideFlag = false;
			// re-enable fading from house trigger
			house.SendMessage("SetFlag", true);
			RenderSettings.fog = false;
		}
	}
	
	void Start() {
		bgm3.volume = 0;
	}

	/* control audio depending on which triggers/audio sources are active
	 * Lerp used for fading effect */
	void Update()
	{
		// going outside
		if (outsideFlag && bgm1Flag == false)
			bgm1.volume = Mathf.Lerp (bgm1.volume, 0, fadeSpeed * Time.deltaTime);
		if (outsideFlag && bgm3Flag == true)
			bgm3.volume = Mathf.Lerp (bgm3.volume, 1, fadeSpeed * Time.deltaTime);
		// going inside (house trigger automatically fades bgm1 back in)
		if (bgm3Flag == false)
			bgm3.volume = Mathf.Lerp (bgm3.volume, 0, fadeSpeed * Time.deltaTime);
	}
	
}
