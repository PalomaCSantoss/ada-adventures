using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataforma : MonoBehaviour {

	Transform Transformplataforma;
	Vector2 PosicaoInicial;
	public Vector2 Direcao;
	public float Distancia;
	public float VelocidadeMovimento;
	float TempoDecorrido;
	string TagObjetoStay;
	string TagObjetoExit;
	string TagObjetoTocado;

	void Start () {
		TagObjetoStay="";
		TagObjetoExit="";
		TagObjetoTocado = "";
		Transformplataforma = GetComponent<Transform>();
		PosicaoInicial = Transformplataforma.position;
		TempoDecorrido = 0f;
	}

	void Update () {
		mover();
	}
	void mover(){
		TempoDecorrido += Time.deltaTime * VelocidadeMovimento;
		float movimento = Mathf.PingPong(TempoDecorrido, Distancia);
		transform.position = PosicaoInicial + Direcao.normalized * movimento;
	}
	void OnCollisionEnter2D(Collision2D objetoTocado){
		TagObjetoTocado = objetoTocado.gameObject.tag;
		if (tag.Contains("Player")== true){
			objetoTocado.transform.parent = Transformplataforma;
		}
	}
	void OnCollisionExit2D(Collision2D objetoParouTocar){
		TagObjetoExit= objetoParouTocar.gameObject.tag;
		if(tag.Contains("Player")== true){
			objetoParouTocar.transform.parent = null;
		}
	}
}
