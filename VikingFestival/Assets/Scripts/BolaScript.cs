using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaScript : MonoBehaviour {

    public float forcaChute = 7f;
    public Transform mira;
    Rigidbody rb;
    SphereCollider spCollider;
    MovimentaMira mm;

    void Start () {
        rb = GetComponent<Rigidbody>();
        spCollider = GetComponent<SphereCollider>();
        mm = mira.GetComponent<MovimentaMira>();
    }
	
	void Update () {
	}

    public void Chutar() {
        Vector3 chuteAlvo = Vector3.up + mira.position - transform.position;
        chuteAlvo = chuteAlvo.normalized;
        rb.AddForce(chuteAlvo * forcaChute, ForceMode.Impulse);
    }

    void OnTriggerEnter (Collider c) {
        if (c.CompareTag("Finish")) {
            Chutar();
            Destroy(gameObject, 3f);
        }
        if (c.CompareTag("fora")) {
            spCollider.enabled = false;
            mm.estado = MovimentaMira.Estados.Mira;
            HUDScript.Instance.vidas--;
            HUDScript.Instance.AtualizarHud();
            Destroy(gameObject, 1f);
        }

        if (c.CompareTag("gol")) {
            spCollider.enabled = false;
            mm.estado = MovimentaMira.Estados.Mira;
            HUDScript.Instance.gols++;
            HUDScript.Instance.AtualizarHud();
            Destroy(gameObject, 1f);
        }
    }

    void OnCollisionEnter (Collision c) {
        if (c.collider.CompareTag("goleiro")) {
            spCollider.enabled = false;
            mm.estado = MovimentaMira.Estados.Mira;
            HUDScript.Instance.vidas--;
            HUDScript.Instance.AtualizarHud();
            Destroy(gameObject, 1f);
        }
    }
}
