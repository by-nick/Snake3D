using UnityEngine;

public class Coletavel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que encostou tem a tag "snake" ou o componente Snake
        if (other.CompareTag("snake"))
        {
            // Busca o script Snake na cabeça que colidiu
            Snake cobra = other.GetComponent<Snake>();

            if (cobra != null)
            {
                cobra.AumentaTamanho(); // Chama o método da cobrinha
            }

            // Destrói o item coletável atual
            Destroy(gameObject);
        }
    }
}