﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour {

	void OnCollisionEnter(Collision c) {
        Destroy(gameObject);
    }
}
