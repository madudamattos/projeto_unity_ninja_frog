using UnityEngine;

public class Gelo : MonoBehaviour
{
    public float deslizamento = 5f; // Força com que o personagem escorrega
    public float velocidadeDeslizamento = 3f; // Velocidade de movimentação no gelo
    private bool noGelo = false; // Verifica se o personagem está na área de gelo
    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu tem a tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            rb = collision.GetComponent<Rigidbody2D>(); // Pega o Rigidbody do player
            noGelo = true; // Personagem entrou na área de gelo
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verifica se o objeto que saiu da área de gelo é o player
        if (collision.gameObject.CompareTag("Player"))
        {
            noGelo = false; // Personagem saiu da área de gelo
        }
    }

    private void FixedUpdate()
    {
        // Se o personagem está na área de gelo
        if (noGelo && rb != null)
        {
            // Captura a entrada do jogador (ex: Setas ou WASD)
            float moveHorizontal = Input.GetAxis("Horizontal");

            // Aplica a velocidade de deslizamento e ajusta a direção conforme a entrada do jogador
            rb.velocity = new Vector2(moveHorizontal * velocidadeDeslizamento + (rb.velocity.x > 0 ? deslizamento : -deslizamento), rb.velocity.y);

            // Força de deslizamento contínua, independente da direção
            rb.velocity += new Vector2((moveHorizontal == 0 ? Mathf.Sign(rb.velocity.x) : moveHorizontal) * deslizamento * Time.fixedDeltaTime, 0);
        }
    }
}