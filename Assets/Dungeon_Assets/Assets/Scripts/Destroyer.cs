﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		
		if (!other.CompareTag("Player") && !other.CompareTag("MainCamera")&& !other.CompareTag("Enemy"))
		{
			Destroy(other.gameObject);
		}

		
	}
}
