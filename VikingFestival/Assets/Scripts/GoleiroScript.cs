using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoleiroScript : MonoBehaviour {

    public float velocidadeGoleiro = 1f;
    public float limitesX;
    //public GameObject goleiro;

    Vector3 proxPos;
    Vector3 primPos;

    //Animator anim;

	void Start () {
        //anim = goleiro.GetComponent<Animator>();
        primPos = transform.position;
        proxPos = new Vector3(Random.Range(-limitesX, limitesX), transform.position.y, transform.position.z);
    }
	
	void Update () {
	}

    public void Defender() {
        transform.position = Vector3.Lerp(transform.position, proxPos, velocidadeGoleiro);
        //if(transform.position.x > 0f) {
        //    anim.SetTrigger("right");
        //}

        //if (transform.position.x < 0f) {
        //    anim.SetTrigger("left");
        //}
    }

    public void Voltar() {
        transform.position = Vector3.Lerp(transform.position, primPos, velocidadeGoleiro);
        proxPos = new Vector3(Random.Range(-limitesX, limitesX), transform.position.y, transform.position.z);
    }
}
