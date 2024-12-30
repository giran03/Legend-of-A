using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestHandler : MonoBehaviour
{
    public List<NPCHandler> npcHandlersList;
    [SerializeField] TMP_Text _questCounter;
    int _interactionCount = 0;

    bool isQuestComplete = false;

    public static QuestHandler Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start() => _questCounter.SetText($"{_interactionCount}/6");

    private void Update()
    {
        if (!isQuestComplete)
        {
            if (npcHandlersList.TrueForAll(x => x.playerHasInteracted))
            {
                isQuestComplete = true;
                Debug.LogWarning($"Quest Complete!");
            }
        }
    }

    public void UpdateQuestTextCounter()
    {
        _interactionCount = npcHandlersList.Count(x => x.playerHasInteracted);
        _questCounter.SetText($"{_interactionCount}/6");
    }
}