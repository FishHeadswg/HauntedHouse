/*
Author: Trevor Richardson
Flashlight.cs
02-24-2015

	Simple script for turning on/off the flashlight
	
 */

using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour
{

		private bool on = false; // on/off tracker
		public GameObject text; // instruction text

		void Start ()
		{
				gameObject.light.intensity = 0;
		}

	// control flashlight via light intensity
		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.F))
				if (!on) {
						gameObject.light.intensity = 3;
						on = true;
						text.SetActive (false); // disable permanantly
				} else {
						gameObject.light.intensity = 0;
						on = false;
				}
		}
}
