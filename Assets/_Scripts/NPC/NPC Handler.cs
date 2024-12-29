using System;
using cherrydev;
using NUnit.Framework;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class NPCHandler : MonoBehaviour, IInteractable
{
    [SerializeField] DialogBehaviour _dialogueBehaviour;
    [SerializeField] DialogNodeGraph _dialogueNodeGraph;


    public void Interact(GameObject player)
    {
        player.GetComponent<PlayerController>().DisableMovement();

        if (!_dialogueBehaviour.ExternalFunctionsHandler.DoesExternalFunctionExist("enableMovement"))
            _dialogueBehaviour.BindExternalFunction("enableMovement", player.GetComponent<PlayerController>().EnableMovement);
        else
            Debug.Log($"External function already exists");

        _dialogueBehaviour.StartDialog(_dialogueNodeGraph);
    }
}