using cherrydev;
using UnityEngine;

public class NPCHandler : MonoBehaviour, IInteractable
{
    [SerializeField] DialogBehaviour _dialogueBehaviour;
    [SerializeField] DialogNodeGraph _dialogueNodeGraph;
    [SerializeField] GameObject _nextLevelUnlock;
    public bool flipOnce;
    bool isFlipped;

    GameObject _questMark;
    public bool playerHasInteracted = false;

    private void Start()
    {
        if (transform.childCount != 0)
            _questMark = transform.GetChild(0).gameObject;
    }

    public void Interact(GameObject player)
    {
        playerHasInteracted = true;

        player.GetComponent<PlayerController>().DisableMovement();

        if (!_dialogueBehaviour.ExternalFunctionsHandler.DoesExternalFunctionExist("enableMovement"))
            _dialogueBehaviour.BindExternalFunction("enableMovement", player.GetComponent<PlayerController>().EnableMovement);
        else
            Debug.Log($"External function already exists");

        _dialogueBehaviour.StartDialog(_dialogueNodeGraph);

        // Unlock next level
        if (_nextLevelUnlock != null)
        {
            if (flipOnce && !isFlipped)
            {
                isFlipped = true;
                _nextLevelUnlock.SetActive(!_nextLevelUnlock.activeSelf);
            }
            else if (!flipOnce)
                _nextLevelUnlock.SetActive(!_nextLevelUnlock.activeSelf);
        }

        if (_questMark != null)
            _questMark.SetActive(false);

        QuestHandler.Instance.UpdateQuestTextCounter();
    }
}