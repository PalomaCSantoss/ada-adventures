using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigo : MonoBehaviour
{

    SpriteRenderer PersonagemSpriteRenderer;
    Vector2 PosicaoInicial;
    public Vector2 Direcao;
    public float VelocidadeMovimento;
    public float Distancia;
    float TempoDecorrido;
    bool MovendoParaDireita = true;

    void Start()
    {
        PersonagemSpriteRenderer = GetComponent<SpriteRenderer>();
        PosicaoInicial = transform.position;
        TempoDecorrido = 0f;
    }

    void Update()
    {
        mover();
    }
    void mover()
    {
        float tempoAnterior = TempoDecorrido;
        TempoDecorrido += Time.deltaTime * VelocidadeMovimento;
        float movimento = Mathf.PingPong(TempoDecorrido, Distancia);
        if (Mathf.PingPong(tempoAnterior, Distancia) > Mathf.PingPong(TempoDecorrido, Distancia))
        {
            PersonagemSpriteRenderer.flipX = true;
        }
        else
        {
            PersonagemSpriteRenderer.flipX = false;
        }
        transform.position = PosicaoInicial + Direcao.normalized * movimento;
    }
}

