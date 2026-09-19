using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pulododogfase2 : MonoBehaviour{
    SpriteRenderer Rerender;
    string TagTriggerEnter;
    string TagTriggerStay;
    string TagTriggerExit;
    string TagOjetoTocando;
    string TagObjetoParouTocar;
    string TagObjetoTocado;
    int TotalPulos; 
    int ContadorPulos;
    bool ApertouBotaoPular;
    float VelocidadePuloSimples;
    public Text moedas;
    public Text vidas;
    public Text chaves;
    public Text pocao;
    Collider2D Colisor2dPersonagem;
    bool EstaTocandoAlgumColisor;
    float VelocidadeX;
    float VelocidadeY;

    float VelocidadeHorizontalMaxima;
    float DirecaoHorizontal;

    float VelocidadeVerticalMaxima;

    float DirecaoVertical;

    int qtdMoeda;
    int qtdRacao;
    int qtdBolinha;
    int qtdvida;
    int qtdchave;
    int qtdbonusPuloDuplo;
    Vector2 VetorVelocidadePersonagem;

    Rigidbody2D CorpoRigidoPersonagem;
    PhysicsMaterial2D MaterialSemAtrito;

    public void Start()
    {
        TotalPulos = 0; 
		ContadorPulos = 0;
        TagTriggerStay ="";
        TagTriggerEnter ="";
        TagTriggerExit ="";
        TagOjetoTocando="";
        TagObjetoParouTocar="";
        Rerender = GetComponent<SpriteRenderer> ();
        TagObjetoTocado = "";
        qtdMoeda = 0;
        qtdRacao = 0;
        qtdBolinha = 0;
        qtdchave = 0;
        qtdvida = 0;
        qtdbonusPuloDuplo = 0;
        ApertouBotaoPular = false;
        EstaTocandoAlgumColisor = false;
        VelocidadePuloSimples = 10.0f;
        CorpoRigidoPersonagem = GetComponent<Rigidbody2D>();
        Colisor2dPersonagem = GetComponent<Collider2D>();
        CorpoRigidoPersonagem.gravityScale = 3.0f;
        CorpoRigidoPersonagem.freezeRotation = true;
        VelocidadeX = 0f;
        VelocidadeY = 0f;
        VelocidadeHorizontalMaxima = 5.0f;
        DirecaoHorizontal = 0f;
        VelocidadeVerticalMaxima = 10.0f;
        DirecaoVertical = 0f;
        VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);
        CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;

        MaterialSemAtrito = new PhysicsMaterial2D();
        MaterialSemAtrito.friction = 0f; 
        MaterialSemAtrito.bounciness = 0f;
        Colisor2dPersonagem.sharedMaterial = MaterialSemAtrito;
    }

	void Update (){
		if (qtdMoeda == 50 || qtdRacao == 3 && qtdBolinha == 2) {
        SceneManager.LoadScene("passou1");
		} else {
			MovimentoHorizontalFlip ();
			MovimentoPuloMultiplo ();
		}
        if (chaves) chaves.text = "Chaves: " + qtdchave.ToString();
        if (vidas) vidas.text = "Vidas: " +  qtdvida.ToString();
        if (pocao) pocao.text = "Poção: " + qtdbonusPuloDuplo.ToString();

        
         if (moedas) moedas.text = "Moedas: " + qtdMoeda.ToString();
        
	}
 

    void MovimentoHorizontalFlip()
    {
        DirecaoHorizontal = Input.GetAxis("Horizontal");
        VelocidadeX = VelocidadeHorizontalMaxima * DirecaoHorizontal;
        VelocidadeY = CorpoRigidoPersonagem.velocity.y;
        VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);
        CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
        if (DirecaoHorizontal < 0) {
			Rerender.flipX = true;
		} else if (DirecaoHorizontal > 0) {
			Rerender.flipX = false;
		}
    }
    void MovimentoPuloMultiplo(){
    ApertouBotaoPular = Input.GetButtonDown ("Jump");
    if (ApertouBotaoPular == true && ContadorPulos < TotalPulos) {
    ContadorPulos = ContadorPulos + 1; 
    VelocidadeX = CorpoRigidoPersonagem.velocity.x;
    VelocidadeY = VelocidadePuloSimples;
    VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);
    CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
}
}
    void MovimentoPuloSimples()
    {
        ApertouBotaoPular = Input.GetButtonDown("Jump");
        EstaTocandoAlgumColisor = Colisor2dPersonagem.IsTouchingLayers();
        if (ApertouBotaoPular == true && EstaTocandoAlgumColisor == true)
        {
            VelocidadeX = CorpoRigidoPersonagem.velocity.x;
            VelocidadeY = VelocidadePuloSimples;
            VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);
            CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
        }

    }

    void OnCollisionEnter2D(Collision2D objetoTocado)
	{
        TagObjetoTocado = objetoTocado.gameObject.tag;
        if (TagObjetoTocado == "Moeda")
        {
            print(TagObjetoTocado);
            Destroy(objetoTocado.gameObject);
            qtdMoeda++;
            print("Quantidade de moedas coletadas: " + qtdMoeda);
        }

        if (TagObjetoTocado == "Racao")
        {
            print(TagObjetoTocado);
            Destroy(objetoTocado.gameObject);
            qtdRacao++;
            print("Quantidade de ração coletadas:" + qtdRacao);
        }
        if (TagObjetoTocado == "Bolinha")
        {
            print(TagObjetoTocado);
            Destroy(objetoTocado.gameObject);
            qtdBolinha++;
            print("Quantidade de bolinhas coletadas: " + qtdBolinha);
        }
        if (TagObjetoTocado == "Laranja")
        {
            if (VelocidadeHorizontalMaxima < 10 && VelocidadePuloSimples < 20)
            {
                VelocidadeHorizontalMaxima = VelocidadeHorizontalMaxima + 1f;
                VelocidadePuloSimples = VelocidadePuloSimples + 11;
                print("Voce coletou o cogumelo!!");
                Destroy(objetoTocado.gameObject, 0.05f);
            }
        }
        if (TagObjetoTocado == "espinho")
        {
            if (VelocidadeHorizontalMaxima < 10 && VelocidadePuloSimples < 22 || VelocidadeHorizontalMaxima > 10 && VelocidadePuloSimples > 22)
            {
                VelocidadeHorizontalMaxima = VelocidadeHorizontalMaxima - 4f;
                VelocidadePuloSimples = VelocidadePuloSimples - 7f;
                print("Voce coletou o espinho :( !!");
                Destroy(objetoTocado.gameObject, 0.05f);
            }
        }
        if (TagObjetoTocado == "chao")
        {
            print(TagObjetoTocado);
            Destroy(objetoTocado.gameObject, 0.6f);
            print("O chão está caindo!!!!!");
        }
        if (TagObjetoTocado == "chao2")
        {
            print(TagObjetoTocado);
            Destroy(objetoTocado.gameObject, 0.06f);
            print("O chão está caindo!!!!!");
        }
         if (TagObjetoTocado == "chao3")
        {
            print(TagObjetoTocado);
            Destroy(objetoTocado.gameObject, 0.8f);
            print("O chão está caindo!!!!!");
        }
        if (TagObjetoTocado == "porta")
        {
            print(TagObjetoTocado);
            Destroy(objetoTocado.gameObject, 0.06f);
            print("Você destruiu a portinha!!");
        }
        if (TagObjetoTocado == "porta2")
        {
            print(TagObjetoTocado);
            Destroy(objetoTocado.gameObject, 5.0f);
            print("Você destruiu a portinha!!");
        }
        if (TagObjetoTocado == "poção")
        {
            if(VelocidadeHorizontalMaxima < 10 )
            {
                VelocidadeHorizontalMaxima = VelocidadeHorizontalMaxima +2;
                print("Você coletou a poção :) !!");
                Destroy (objetoTocado.gameObject);
            }
        } 
        if (TagObjetoTocado.Contains("sematrito")) {
        print ("Tocou TAG: sematrito");
        objetoTocado.collider.sharedMaterial = MaterialSemAtrito;   
        }
        if (TagObjetoTocado == "morre") {
        SceneManager.LoadScene ("gameover");
         } 
         if (TagObjetoTocado == "morre2") {
        SceneManager.LoadScene ("gameover2");
         } 
         if (TagObjetoTocado == "pulapula") {
            CorpoRigidoPersonagem.velocity = new Vector2 (0, 17);   
        } 
        if (TagObjetoTocado =="pula pulinho"){
		SceneManager.LoadScene("espaço");	
	}
        if (TagObjetoTocado =="f3"){
		SceneManager.LoadScene("fase03");	
    	}
        if (TagObjetoTocado =="ganhou"){
		SceneManager.LoadScene("voceganhou");	
    	}
        if (TagObjetoTocado == "bonusPuloDuplo") {
            qtdbonusPuloDuplo++;
            TotalPulos = 8;
            Destroy (objetoTocado.gameObject);
        }
        if (TagObjetoTocado =="naopulo") {
            ContadorPulos = 0;
}

    }                                                  
    void OnTriggerEnter2D(Collider2D objetoTriggerEnter){
        TagTriggerEnter = objetoTriggerEnter.gameObject.tag;
        if (TagTriggerEnter =="Moeda")
        {
            qtdMoeda++;
            Destroy(objetoTriggerEnter.gameObject);
            print("Quantidade de moedas coletadas: " + qtdMoeda);
            Debug.Log(qtdMoeda);
        }
         if (TagTriggerEnter  == "porta")
        {
            Destroy(objetoTriggerEnter.gameObject, 0.06f);
            print("Você destruiu a portinha!!");
        }
        if (TagTriggerEnter == "vida")
        {
            Destroy(objetoTriggerEnter.gameObject);
            qtdvida++;
            print("Quantidade de vidas coletadas: " + qtdvida);
        }
        if (TagTriggerEnter == "chave")
        {
            Destroy(objetoTriggerEnter.gameObject);
            qtdchave++;
            print("Quantidade de chaves coletadas: " + qtdchave);
        }
    }
    void EntradasMouse(){
    if (Input.GetMouseButtonDown (0) == true) {
    print ("Apertou o botão esquerdo");
    }
    if (Input.GetMouseButtonDown (1) == true) {
     print ("Apertou o botão direito");
    }
    if (Input.GetMouseButtonDown (2) == true) {
     print ("Apertou o botão do meio");
    }
  }
}