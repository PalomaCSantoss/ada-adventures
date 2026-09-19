using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fasemenu2 : MonoBehaviour
{
    public Text moedas;
    public Text vidas;
    public Text inimigo;
    SpriteRenderer Rerender;
    Rigidbody2D CorpoRigidoPersonagem;
    Vector2 VetorVelocidadePersonagem;
    Collider2D Colisor2dPersonagem;
    Animator PersonagemAnimator;
    Transform TransformObjetoCarregado;
    Transform TransformPersonagem;
    PhysicsMaterial2D MaterialSemAtrito;
    string TagObjetoTocado;
    string tagTocadaTrigger;
    string TagTriggerEnter;
    string TagObjetoParouTocar;
    string TagObjetoStay;
    string TagObjetoExit;
    bool ApertouBotaoPular;
    bool EstaTocandoAlgumColisor;
    bool BonusVelocidadePular;
    float TempoBonusVelocidadePular;
    float VelocidadeX;
    float VelocidadeY;
    float DirecaoHorizontal;
    float VelocidadeHorizontalMaxima;
    float VelocidadePuloSimples;
    float SentidoHorizontal;
    int ContadorPulos;
    int qtdmoeda;
    int qtdpontos;
    int qtdvida;
    int qtdinimigo;
    public void Start()
    {
        TagTriggerEnter = "";
        TagObjetoStay = "";
        TagObjetoExit = "";
        TagObjetoParouTocar = "";
        TagObjetoTocado = "";
        TempoBonusVelocidadePular = 10.0f;
        BonusVelocidadePular = false;
        Rerender = GetComponent<SpriteRenderer>();
        PersonagemAnimator = gameObject.GetComponent<Animator>();
        TransformPersonagem = GetComponent<Transform>();
        Colisor2dPersonagem = GetComponent<Collider2D>();
        CorpoRigidoPersonagem = GetComponent<Rigidbody2D>();
        CorpoRigidoPersonagem.gravityScale = 3.0f;
        CorpoRigidoPersonagem.freezeRotation = true;
        CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
        VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);
        VelocidadeX = 0f;
        VelocidadeY = 0f;
        VelocidadeHorizontalMaxima = 5.0f;
        VelocidadePuloSimples = 10.0f;
        DirecaoHorizontal = 0f;
        ContadorPulos = 0;
        ApertouBotaoPular = false;
        EstaTocandoAlgumColisor = false;
        MaterialSemAtrito = new PhysicsMaterial2D();
        MaterialSemAtrito.friction = 0f;
        MaterialSemAtrito.bounciness = 0f;
        Colisor2dPersonagem.sharedMaterial = MaterialSemAtrito;
        qtdmoeda = 0;
        qtdvida = 0;
        qtdinimigo = 0;
    }

    void Update()
    {
        MovimentoHorizontalFlip();
        MovimentoPuloSimples();
        VerificarBonusPular();
        EntradasMouse();
        SoltarObjeto();
        moedas.text = " " + qtdmoeda.ToString();
        vidas.text = " " + qtdvida.ToString();
        inimigo.text = " " + qtdinimigo.ToString();
    }
    void MovimentoHorizontalFlip()
    {
        DirecaoHorizontal = Input.GetAxis("Horizontal");
        VelocidadeX = VelocidadeHorizontalMaxima * DirecaoHorizontal;
        VelocidadeY = CorpoRigidoPersonagem.velocity.y;
        VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);
        CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
        if (DirecaoHorizontal != 0)
        {
            PersonagemAnimator.SetBool("andando", true);
        }
        else
        {
            PersonagemAnimator.SetBool("andando", false);
        }
        if (DirecaoHorizontal < 0)
        {
            Rerender.flipX = true;
        }
        else if (DirecaoHorizontal > 0)
        {
            Rerender.flipX = false;
        }
    }
    void MovimentoPuloSimples()
    {
        ApertouBotaoPular = Input.GetButtonDown("Jump");
        EstaTocandoAlgumColisor = Colisor2dPersonagem.IsTouchingLayers();
        if (ApertouBotaoPular == true && ContadorPulos < 2)
        {
            ContadorPulos = ContadorPulos + 1;
            PersonagemAnimator.SetBool("pulando", true);
            PersonagemAnimator.SetBool("andando", false);
            VelocidadeX = CorpoRigidoPersonagem.velocity.x;
            VelocidadeY = VelocidadePuloSimples;
            VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);
            CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
        }

    }
    void VerificarBonusPular()
    {
        if (BonusVelocidadePular == true)
        {
            if (TempoBonusVelocidadePular >= 0)
            {
                TempoBonusVelocidadePular = TempoBonusVelocidadePular - Time.deltaTime;
                print("Tempo Bonus Pular: " + TempoBonusVelocidadePular);
            }
            else
            {
                BonusVelocidadePular = false;
                TempoBonusVelocidadePular = 10;
                VelocidadePuloSimples = 10;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D objetoTocado)
    {
        print("ENTROU NO OnCollisionEnter2D");
        print("OBJETO: " + objetoTocado.gameObject.name);
        print("TAG: " + objetoTocado.gameObject.tag);

        TagObjetoTocado = objetoTocado.gameObject.tag;
        VerificarInimigos(objetoTocado);
        PersonagemAnimator.SetBool("pulando", false);
        PersonagemAnimator.SetBool("andando", true);

        if (TagObjetoTocado.Contains("semAtrito"))
        {
            print("Tocou TAG: semAtrito");
            objetoTocado.collider.sharedMaterial = MaterialSemAtrito;
        }
        if (TagObjetoTocado == "caixaarmadilha")
        {
            Destroy(objetoTocado.gameObject, 0.6f);
        }
        if (TagObjetoTocado == "bonus")
        {
            print(TagObjetoTocado);
            Destroy(objetoTocado.gameObject, 0.6f);
            print("bonus coletado");
            qtdvida++;
        }
        if (TagObjetoTocado == "trampolim")
        {
            CorpoRigidoPersonagem.velocity = new Vector2(0, 15);
        }
        if (TagObjetoTocado == "recontadorpulo")
        {
            ContadorPulos = 0;
        }
        if (TagObjetoTocado == "morte")
        {
            SceneManager.LoadScene("morreu2");
        }
    }
    void VerificarInimigos(Collision2D objetoTocado)
    {
        if (TagObjetoTocado.Contains("destruirinimigo01"))
        {
            print("COLIDIU COM: " + objetoTocado.gameObject.name);
            GameObject objetoInimigo = objetoTocado.gameObject.transform.parent.gameObject;
            Destroy(objetoInimigo);
            Destroy(objetoTocado.gameObject);
            qtdinimigo++;
        }
        else if (TagObjetoTocado.Contains("inimigo01"))
        {
            print("bateu no inimigo");
        }
    }
    void OnCollisionStay2D(Collision2D objetoStay)
    {
        TagObjetoStay = objetoStay.gameObject.tag;
        if (tag.Contains("mover") == true)
        {
            bool apertou = Input.GetKey(KeyCode.LeftControl);
            if (apertou == true)
            {
                objetoStay.transform.parent = TransformPersonagem;
                TransformObjetoCarregado = objetoStay.transform;
            }
        }
        if (TagObjetoStay == "lama")
        {
            VelocidadeHorizontalMaxima = 1.0f;
        }
    }
    void OnCollisionExit2D(Collision2D objetoExit)
    {
        TagObjetoExit = objetoExit.gameObject.tag;
        if (TagObjetoExit == "lama")
        {
            VelocidadeHorizontalMaxima = 5;
        }
    }
    void SoltarObjeto()
    {
        bool parouApertar = Input.GetKeyUp(KeyCode.LeftControl);
        if (parouApertar == true)
        {
            if (TransformObjetoCarregado != null)
            {
                TransformObjetoCarregado.parent = null;
                TransformObjetoCarregado = null;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D objetoTriggerTocado)
    {
        tagTocadaTrigger = objetoTriggerTocado.gameObject.tag;
        DirecaoHorizontal = Input.GetAxis("Horizontal");
        SentidoHorizontal = DirecaoHorizontal < 0 ? -1 : 1;

        if (tagTocadaTrigger == "moeda")
        {
            qtdmoeda++;
            Destroy(objetoTriggerTocado.gameObject);
            print("Quantidade de moedas coletadas: " + qtdmoeda);
        }
        if (tagTocadaTrigger == "vida")
        {
            qtdvida++;
            Destroy(objetoTriggerTocado.gameObject);
            print("Quantidade de vidas coletadas: " + qtdvida);
        }
        if (tagTocadaTrigger == "pss1")
        {
            SceneManager.LoadScene("passou1");
        }
        if (tagTocadaTrigger == "pss2")
        {
            SceneManager.LoadScene("passou2");
        }
        if (tagTocadaTrigger == "portal1")
        {
            Vector2 posicaoDestino = GameObject.FindGameObjectWithTag("portal2").GetComponent<Transform>().position;
            float xDestino = posicaoDestino.x;
            float yDestino = posicaoDestino.y;
            xDestino = xDestino + (3.5f * SentidoHorizontal);
            transform.position = new Vector2(xDestino, yDestino);
        }
        if (tagTocadaTrigger.Contains("destrutivel"))
        {
            GameObject coletavel = objetoTriggerTocado.transform.GetChild(0).gameObject;
            coletavel.transform.parent = null;
            coletavel.AddComponent<BoxCollider2D>();
            coletavel.AddComponent<Rigidbody2D>();
            coletavel.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
            coletavel.tag = "bonus";
            Destroy(objetoTriggerTocado.gameObject);

        }
        if (tagTocadaTrigger == "portal2")
        {
            Vector2 posicaoDestino = GameObject.FindGameObjectWithTag("portal1").GetComponent<Transform>().position;
            float xDestino = posicaoDestino.x;
            float yDestino = posicaoDestino.y;
            xDestino = xDestino + (3.5f * SentidoHorizontal);
            transform.position = new Vector2(xDestino, yDestino);
        }
        if (tagTocadaTrigger == "armadilha")
        {
            Rigidbody2D corpoRigidoBola;
            corpoRigidoBola = GameObject.FindGameObjectWithTag("caixaarmadilha").GetComponent<Rigidbody2D>();
            corpoRigidoBola.gravityScale = 1.5f;
        }
        if (tagTocadaTrigger == "superpulo")
        {
            if (VelocidadeHorizontalMaxima < 10 && VelocidadePuloSimples < 20)
            {
                VelocidadeHorizontalMaxima = VelocidadeHorizontalMaxima + 1f;
                VelocidadePuloSimples = VelocidadePuloSimples + 11;
                print("Voce coletou o superpulo!!");
                Destroy(objetoTriggerTocado.gameObject, 0.05f);
            }
        }
        if (tagTocadaTrigger == "velocidadebaixa")
        {
            if (VelocidadeHorizontalMaxima < 10 && VelocidadePuloSimples < 22 || VelocidadeHorizontalMaxima > 10 && VelocidadePuloSimples > 22)
            {
                VelocidadeHorizontalMaxima = VelocidadeHorizontalMaxima - 4f;
                VelocidadePuloSimples = VelocidadePuloSimples - 7f;
                print("Voce coletou a velocidade baixa :( !!");
                Destroy(objetoTriggerTocado.gameObject, 0.05f);
            }
        }
        if (tagTocadaTrigger == "bonustemp")
        {
            print("OnTriggerEnter2D: " + tagTocadaTrigger);
            BonusVelocidadePular = true;
            VelocidadePuloSimples = 80;
            Destroy(objetoTriggerTocado.gameObject);
        }
    }
    void EntradasMouse()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            print("Apertou o botão esquerdo");
        }
        if (Input.GetMouseButtonDown(1) == true)
        {
            print("Apertou o botão direito");
        }
        if (Input.GetMouseButtonDown(2) == true)
        {
            print("Apertou o botão do meio");
        }
    }
}