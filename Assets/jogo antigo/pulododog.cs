using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pulododog : MonoBehaviour
{
	int ContadorPulos;

    SpriteRenderer Rerender;
    string TagTriggerEnter;
    string TagObjetoTocado;

    bool ApertouBotaoPular;
    float VelocidadePuloSimples;

    public Text moedas;
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
    int qtdcogumelo;
    int qtdpocao;
    public Text rações;
    public Text bolinhas;
    public Text cogumelo;
    public Text pocao;
    Vector2 VetorVelocidadePersonagem;

    Rigidbody2D CorpoRigidoPersonagem;
    PhysicsMaterial2D material;

    public void Start()
    {
        Rerender = GetComponent<SpriteRenderer> ();
        TagObjetoTocado = "";

        qtdMoeda = 0;
        qtdRacao = 0;
        qtdBolinha = 0;
        qtdcogumelo = 0;
        qtdpocao = 0;
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

        // Cria um material
        material = new PhysicsMaterial2D("Material");

        // Deixa o atrito dele 0
        material.friction = 0f;

        // Deixa a elasticidade dele 0
        material.bounciness = 0f;

        // Substitui o material original do colisor pelo material criado
        Colisor2dPersonagem.sharedMaterial = material;

    }

	void Update (){
		if (qtdMoeda == 50 || qtdRacao == 3 && qtdBolinha == 2) {
        SceneManager.LoadScene("passou1");
		} else {
			MovimentoHorizontalFlip ();
			MovimentoPuloSimples ();
		}

        Colisor2dPersonagem.sharedMaterial.friction = 0f;
        Colisor2dPersonagem.sharedMaterial.bounciness = 0f;

        moedas.text = "Moedas: " +  qtdMoeda.ToString();
        rações.text = "Rações: " +  qtdRacao.ToString();
        bolinhas.text = "Bolinhas: " +  qtdBolinha.ToString();
        cogumelo.text = "Cogumelo Mágico: " + qtdcogumelo.ToString();
        pocao.text = "Poção: " + qtdpocao.ToString();
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
                qtdcogumelo++;
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
        if (TagObjetoTocado == "poção")
        {
            if(VelocidadeHorizontalMaxima < 10 )
            {
                qtdpocao++;
                VelocidadeHorizontalMaxima = VelocidadeHorizontalMaxima +2;
                print("Você coletou a poção :) !!");
                Destroy (objetoTocado.gameObject);
            }
        } 
        if (TagObjetoTocado == "morre") {
        SceneManager.LoadScene ("gameover");
         }                                               
    }

    void OnTriggerEnter2D(Collider2D objetoTriggerEnter)
    {
        TagTriggerEnter = objetoTriggerEnter.gameObject.tag;
        if (TagTriggerEnter =="Moeda")
        {
            qtdMoeda++;
            Destroy(objetoTriggerEnter.gameObject);
            print("Quantidade de moedas coletadas: " + qtdMoeda);
        }
        if (TagTriggerEnter == "porta")
        {
            Destroy(objetoTriggerEnter.gameObject, 0.06f);
            print("Você destruiu a portinha!!");
        }
        if (TagTriggerEnter == "chao")
        {
            Destroy(objetoTriggerEnter.gameObject, 0.6f);
            print("O chão está caindo!!!!!");
        }
        if (TagTriggerEnter == "chao2")
        {
            Destroy(objetoTriggerEnter.gameObject, 0.06f);
            print("O chão está caindo!!!!!");
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