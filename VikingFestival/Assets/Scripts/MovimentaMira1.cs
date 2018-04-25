using UnityEngine;

public class MovimentaMira1 : MonoBehaviour {

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
    BolaScript1 bolaScript;
    GoleiroScript goleiroScript;
    Animator jogadorAnim;
    bool animar = false;
    float timer;
    Vector3 primeiraPosMira;
	
	void Start () {
        estado = Estados.Mira;
        HUDScript.Instance.Reiniciar();
        goleiroScript = goleiroMira.GetComponent<GoleiroScript>();
        jogadorAnim = jogador.GetComponent<Animator>();
        primeiraPosMira = transform.position;
    }
	
	
	void Update () {
        switch (estado) {
            case Estados.Mira:
                if(HUDScript.Instance.vidas < 1) {
                    HUDScript.Instance.AtivarReinicio();
                    return;
                }
                InstanciaBola();
                goleiroScript.Voltar();
                // Movimento horizontal
                transform.Translate(Vector3.right * velocidadeMiraHorizontal * Time.deltaTime);
                if (transform.position.x < -limitesMiraX || transform.position.x > limitesMiraX) {
                    velocidadeMiraHorizontal = -velocidadeMiraHorizontal;
                }
                //Movimento vertical
                transform.Translate(Vector3.up * velocidadeMiraVertical * Time.deltaTime);
                if (transform.position.y < limiteMiraMinY || transform.position.y > limiteMiraMaxY) {
                    velocidadeMiraVertical = -velocidadeMiraVertical;
                }
                break;
            case Estados.Forca:
                HUDScript.Instance.sliderAtivo = true;
                animar = true;
                break;
            case Estados.Chute:
                HUDScript.Instance.sliderAtivo = false;
                goleiroScript.Defender();
                break;
        }
    }

    public void RecentralizarMira () {
        transform.position = Vector3.MoveTowards(transform.position, primeiraPosMira, 1f);
    }

    public void AnimarJogador() {
        if (animar) {
            jogadorAnim.SetTrigger("Kick");
            animar = false;
        }
    }

    public void InstanciaBola () {
        if(bola == null) {
            RecentralizarMira();
            bola = Instantiate(bolaPrefab, bolaSpawn.position, bolaSpawn.rotation);
            bolaScript = bola.GetComponent<BolaScript1>();
            bolaScript.mira = transform;
            HUDScript.Instance.chutarAtivo = true;
        }
    }

    public void TrocaEstado () {
        if (HUDScript.Instance.vidas < 1) {
            HUDScript.Instance.AtivarReinicio();
            return;
        }
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

    public void Reset () {
        transform.position = primeiraPosMira;
    }
}
