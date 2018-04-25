using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOffset : MonoBehaviour {

	public float speedX = 0.1f;
	public float speedY = 0.1f;

	private float curX;
	private float curY;

	// Use this for initialization
	void Awake () {

		curX = this.GetComponent<Renderer> ().material.mainTextureOffset.x;
		curY = this.GetComponent<Renderer> ().material.mainTextureOffset.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		curX += Time.deltaTime * speedX;
		curY += Time.deltaTime * speedY;

		this.GetComponent<Renderer> ().material.SetTextureOffset ("_MainTex",new Vector2(curX,curY));
		
	}
}
