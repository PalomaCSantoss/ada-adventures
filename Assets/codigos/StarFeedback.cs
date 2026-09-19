using UnityEngine;
using UnityEngine.UI;

public class StarFeedback : MonoBehaviour
{
    public Image estrela;
    public Sprite estrelaAmarela;
    public Sprite estrelaCinza;

    private bool selecionada = false;

    public void ClicarEstrela()
    {
        if (selecionada == false)
        {
            estrela.sprite = estrelaAmarela;
            selecionada = true;
        }
        else
        {
            estrela.sprite = estrelaCinza;
            selecionada = false;
        }
    }
}