using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlataformaGeloDerretendo : MonoBehaviour
{
    [SerializeField] private float tempoParaDerreter = 3f; // Tempo total até a plataforma derreter
    [SerializeField] private float tempoVisivel = 0.1f; // Tempo que a plataforma permanece visível ao piscar
    [SerializeField] private float tempoInvisivel = 0.1f; // Tempo que a plataforma permanece invisível ao piscar
    [SerializeField] private float tempoAtrasoFinal = 0.5f; // Tempo extra após a última piscada antes de derreter
    [SerializeField] private float tempoParaReaparecer = 3f; // Tempo para a plataforma reaparecer após derreter

    private bool jogadorPisou = false;
    private Tilemap tilemap;
    private CompositeCollider2D compositeCollider;
    private TilemapCollider2D tilemapCollider;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        compositeCollider = GetComponent<CompositeCollider2D>();
        tilemapCollider = GetComponent<TilemapCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !jogadorPisou)
        {
            jogadorPisou = true;
            StartCoroutine(PlataformaPiscarEDerreter());
        }
    }

    private IEnumerator PlataformaPiscarEDerreter()
    {
        float tempoDecorrido = 0f;

        // Enquanto o tempo não acabar, a plataforma pisca
        while (tempoDecorrido < tempoParaDerreter)
        {
            // Alterna entre visível e invisível
            tilemap.color = new Color(1f, 1f, 1f, 1f); // Visível
            compositeCollider.enabled = true; // Ativa collider
            tilemapCollider.enabled = true; // Ativa collider
            yield return new WaitForSeconds(tempoVisivel);

            tilemap.color = new Color(1f, 1f, 1f, 0f); // Invisível
            compositeCollider.enabled = true; // Mantém collider ativo
            tilemapCollider.enabled = true; // Mantém collider ativo
            yield return new WaitForSeconds(tempoInvisivel);

            // Aumenta o tempo decorrido
            tempoDecorrido += (tempoVisivel + tempoInvisivel);
        }

        // Aguarda um pequeno atraso após a última piscada
        yield return new WaitForSeconds(tempoAtrasoFinal);

        // Após a última piscada, a plataforma derrete
        tilemap.color = new Color(1f, 1f, 1f, 0f); // Torna a plataforma invisível
        compositeCollider.enabled = false; // Desativa o CompositeCollider2D
        tilemapCollider.enabled = false; // Desativa o TilemapCollider2D

        // Aguarda o tempo para a plataforma reaparecer
        yield return new WaitForSeconds(tempoParaReaparecer);

        // Faz a plataforma reaparecer
        tilemap.color = new Color(1f, 1f, 1f, 1f); // Torna a plataforma visível novamente
        compositeCollider.enabled = true; // Reativa o CompositeCollider2D
        tilemapCollider.enabled = true; // Reativa o TilemapCollider2D

        jogadorPisou = false; // Reseta o estado para permitir que o jogador pise novamente
    }
}
