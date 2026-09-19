using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class cenas : MonoBehaviour
{
    void OnMouseDown()
    {
        string tagObjeto = gameObject.tag;
        print("GameObject clicado: " + tagObjeto);
        if (tagObjeto == "iniciar")
        {
            SceneManager.LoadScene("fase01");
        }
        if (tagObjeto == "menu")
        {
            SceneManager.LoadScene("menu");
        }
        if (tagObjeto == "f1")
        {
            SceneManager.LoadScene("fase01");
        }
        if (tagObjeto == "f2")
        {
            SceneManager.LoadScene("fasemenu2");
        }
        if (tagObjeto == "f3")
        {
            SceneManager.LoadScene("fasemenu3");
        }
        if (tagObjeto == "v1")
        {
            SceneManager.LoadScene("fase01");
        }
        if (tagObjeto == "v2")
        {
            SceneManager.LoadScene("fase02");
        }
        if (tagObjeto == "v3")
        {
            SceneManager.LoadScene("fase02");
        }
        if (tagObjeto == "p1")
        {
            SceneManager.LoadScene("fase02");
        }
        if (tagObjeto == "p2")
        {
            SceneManager.LoadScene("fase03");
        }
        if (tagObjeto == "sair")
        {
            SceneManager.LoadScene("inicio");
        }
        if (tagObjeto == "avaliacao")
        {
            SceneManager.LoadScene("avaliacao");
        }
        if (tagObjeto.Contains("fechar"))
        {
            Debug.Log("Saindo do jogo...");
            Application.Quit();
            print("fechou o jogo");
        }

    }
    void OnMouseUp()
    {
        string tagObjeto = gameObject.tag;
        print("Mouse solto sobre o GameObject: " + tagObjeto);
    }
    void OnMouseEnter()
    {
        string tagObjeto = gameObject.tag;
        print("Mouse entrou no GameObject: " + tagObjeto);
    }
    void OnMouseExit()
    {
        string tagObjeto = gameObject.tag;
        print("Mouse saiu do GameObject: " + tagObjeto);
    }
    void OnMouseOver()
    {
        string tagObjeto = gameObject.tag;
        print("Mouse está sobre o GameObject: " + tagObjeto);
    }
}