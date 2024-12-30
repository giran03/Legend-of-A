using cherrydev;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField] DialogBehaviour _dialogueBehaviour;
    [SerializeField] DialogNodeGraph _dialogueNodeGraph;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.gameObject;
            player.GetComponent<PlayerController>().DisableMovement();

            if (!_dialogueBehaviour.ExternalFunctionsHandler.DoesExternalFunctionExist("enableMovement"))
                _dialogueBehaviour.BindExternalFunction("enableMovement", player.GetComponent<PlayerController>().EnableMovement);
            else
                Debug.Log($"External function already exists");

            _dialogueBehaviour.StartDialog(_dialogueNodeGraph);

            gameObject.SetActive(false);
        }
    }
}
