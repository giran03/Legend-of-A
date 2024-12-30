using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    [SerializeField] GameObject _endGamePanel;

    private void Start() => _endGamePanel.SetActive(false);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Initiate Quest Complete Screen!");
            _endGamePanel.SetActive(true);

            other.GetComponent<PlayerController>().DisableMovement();

            gameObject.SetActive(false);
        }
    }
}