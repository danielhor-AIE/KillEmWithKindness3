using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string questName;
    // Add other quest properties as needed
}

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();

    public static object Instance { get; internal set; }

    public void ShowQuests()
    {
        Debug.Log("Quests:");

        foreach (Quest quest in quests)
        {
            Debug.Log("Quest Name: " + quest.questName);
            // Display other quest information if needed
        }
    }

    public void AddQuest(string questName)
    {
        Quest newQuest = new Quest();
        newQuest.questName = questName;
        quests.Add(newQuest);

        Debug.Log("Added quest: " + questName);
    }

    public void RemoveQuest(string questName)
    {
        Quest questToRemove = quests.Find(quest => quest.questName == questName);

        if (questToRemove != null)
        {
            quests.Remove(questToRemove);
            Debug.Log("Removed quest: " + questName);
        }
        else
        {
            Debug.Log("Quest not found: " + questName);
        }
    }
}
