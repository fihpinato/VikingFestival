using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaMira : MonoBehaviour {

    public float velocidadeMiraHorizontal = 1f;
    public float velocidadeMiraVertical = 1f;
    public float limitesMiraX;
    public float limiteMiraMaxY;
    public float limiteMiraMinY;
    public GameObject jogador;
    public GameObject goleiroMira;
    public GameObject bolaPrefab;
    public Transform bolaSpawn;
    public enum Estados {Mira, Forca, Chute};
    public Estados estado;

    GameObject bola;
    BolaScript bolaScript;
    GoleiroScript goleiroScript;
    Animator jogadorAnim;
    bool animar = false;
    float timer;
	
	void Start () {
        estado = Estados.Mira;
        HUDScript.Instance.Reiniciar();
        goleiroScript = goleiroMira.GetComponent<GoleiroScript>();
        jogadorAnim = jogador.GetComponent<Animator>();
    }
	
	
	void Update () {
        switch (estado) {
            case Estados.Mira:
                InstanciaBola();
                goleiroScript.Voltar();
                // Movimento horizontal
                transform.Translate(Vector3.right * velocidadeMiraHorizontal * Time.deltaTime);
                if (transform.position.x < -limitesMiraX || transform.position.x > limitesMiraX) {
                    velocidadeMiraHorizontal = -velocidadeMiraHorizontal;
                }
                break;
            case Estados.Forca:
                //Movimento vertical
                transform.Translate(Vector3.up * velocidadeMiraVertical * Time.deltaTime);
                if (transform.position.y < limiteMiraMinY || transform.position.y > limiteMiraMaxY) {
                    velocidadeMiraVertical = -velocidadeMiraVertical;
                }
                animar = true;
                break;
            case Estados.Chute:
                goleiroScript.Defender();
                break;
        }
    }

    public void AnimarJogador() {
        if (animar) {
            jogadorAnim.SetTrigger("Kick");
            animar = false;
        }
    }

    public void InstanciaBola () {
        if(bola == null) {
            bola = Instantiate(bolaPrefab, bolaSpawn.position, bolaSpawn.rotation);
            bolaScript = bola.GetComponent<BolaScript>();
            bolaScript.mira = transform;
        }
    }

    public void TrocaEstado () {
        switch (estado) {
            case Estados.Mira:
                estado = Estados.Forca;
                break;
            case Estados.Forca:
                estado = Estados.Chute;
                break;
            case Estados.Chute:
                estado = Estados.Mira;
                break;
        }
    }
}
