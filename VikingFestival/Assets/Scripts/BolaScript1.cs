using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaScript1 : MonoBehaviour {

    public float forcaChute = 7f;
    public Transform mira;
    Rigidbody rb;
    SphereCollider spCollider;
    MovimentaMira1 mm;

    void Start () {
        rb = GetComponent<Rigidbody>();
        spCollider = GetComponent<SphereCollider>();
        mm = mira.GetComponent<MovimentaMira1>();
    }
	
	void Update () {

	}

    public void Chutar() {
        Vector3 chuteAlvo = Vector3.up + mira.position - transform.position;
        chuteAlvo = chuteAlvo.normalized;
        if(HUDScript.Instance.sliderValor < 0) {
            HUDScript.Instance.sliderValor *= -1f;
        }
        rb.AddForce(chuteAlvo * forcaChute * HUDScript.Instance.sliderValor, ForceMode.Impulse);
        HUDScript.Instance.chutarAtivo = false;
    }

    IEnumerator Reinicio () {
        yield return new WaitForSeconds(2f);
        mm.estado = MovimentaMira1.Estados.Mira;
        HUDScript.Instance.vidas--;
        HUDScript.Instance.AtualizarHud();
        Destroy(gameObject);
    }

    void OnTriggerEnter (Collider c) {
        if (c.CompareTag("Finish")) {
            Chutar();
            StartCoroutine(Reinicio());
        }

        if (c.CompareTag("fora")) {
            spCollider.enabled = false;
            mm.estado = MovimentaMira1.Estados.Mira;
            HUDScript.Instance.vidas--;
            HUDScript.Instance.AtualizarHud();
            Destroy(gameObject, 1f);
        }

        if (c.CompareTag("gol")) {
            spCollider.enabled = false;
            mm.estado = MovimentaMira1.Estados.Mira;
            HUDScript.Instance.gols++;
            HUDScript.Instance.AtualizarHud();
            Destroy(gameObject, 1f);
        }
    }

    void OnCollisionEnter (Collision c) {
        if (c.collider.CompareTag("goleiro")) {
            spCollider.enabled = false;
            mm.estado = MovimentaMira1.Estados.Mira;
            HUDScript.Instance.vidas--;
            HUDScript.Instance.AtualizarHud();
            Destroy(gameObject, 1f);
        }
    }
}
