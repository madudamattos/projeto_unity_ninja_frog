using System.Collections;
using UnityEngine;

public class PlataformaGelo : MonoBehaviour
{
    [SerializeField] private float _tempoParaDerreter = 3f; // Tempo total at� a plataforma derreter
    [SerializeField] private float _tempoVisivel = 0.1f; // Tempo que a plataforma permanece vis�vel ao piscar
    [SerializeField] private float _tempoInvisivel = 0.1f; // Tempo que a plataforma permanece invis�vel ao piscar
    [SerializeField] private float _tempoAtrasoFinal = 0.5f; // Tempo extra ap�s a �ltima piscada antes de derreter
    [SerializeField] private float _tempoParaReaparecer = 3f; // Tempo para a plataforma reaparecer ap�s derreter

    private bool _jogadorPisou = false;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_jogadorPisou)
        {
            _jogadorPisou = true;
            StartCoroutine(PlataformaPiscarEDerreter());
        }
    }

    private IEnumerator PlataformaPiscarEDerreter()
    {
        float _tempoDecorrido = 0f;

        // Enquanto o tempo n�o acabar, a plataforma pisca
        while (_tempoDecorrido < _tempoParaDerreter)
        {
            // Alterna entre vis�vel e invis�vel
            _spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Vis�vel
            _boxCollider.enabled = true; // Ativa o collider
            yield return new WaitForSeconds(_tempoVisivel);

            _spriteRenderer.color = new Color(1f, 1f, 1f, 0f); // Invis�vel
            _boxCollider.enabled = true; // Mant�m collider ativo
            yield return new WaitForSeconds(_tempoInvisivel);

            // Aumenta o tempo decorrido
            _tempoDecorrido += (_tempoVisivel + _tempoInvisivel);
        }

        // Aguarda um pequeno atraso ap�s a �ltima piscada
        yield return new WaitForSeconds(_tempoAtrasoFinal);

        // Ap�s a �ltima piscada, a plataforma derrete
        _spriteRenderer.color = new Color(1f, 1f, 1f, 0f); // Torna a plataforma invis�vel
        _boxCollider.enabled = false; // Desativa o BoxCollider2D

        // Aguarda o tempo para a plataforma reaparecer
        yield return new WaitForSeconds(_tempoParaReaparecer);

        // Faz a plataforma reaparecer
        _spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Torna a plataforma vis�vel novamente
        _boxCollider.enabled = true; // Reativa o BoxCollider2D

        _jogadorPisou = false; // Reseta o estado para permitir que o jogador pise novamente
    }
}
