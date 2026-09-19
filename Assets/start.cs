using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start: MonoBehaviour {

void OnMouseDown() {
	string tagObjeto = gameObject.tag;
		print("GameObject clicado: " + tagObjeto);
	if (tagObjeto == "botaostart") {
	SceneManager.LoadScene("fase01");
	}
	if(tagObjeto =="retry"){
		SceneManager.LoadScene("start");
	}
    if(tagObjeto =="proximo"){
		SceneManager.LoadScene("fase02");
	}
}
	void OnMouseUp() {
	string tagObjeto = gameObject.tag;
	print("Mouse solto sobre o GameObject: " + tagObjeto);
	}

void OnMouseEnter() {
string tagObjeto = gameObject.tag;
print("Mouse entrou no GameObject: " + tagObjeto);
}

void OnMouseExit() {
string tagObjeto = gameObject.tag;
print("Mouse saiu do GameObject: " + tagObjeto);
}

void OnMouseOver() {
string tagObjeto = gameObject.tag;
print("Mouse está sobre o GameObject: " + tagObjeto);
}
  
}