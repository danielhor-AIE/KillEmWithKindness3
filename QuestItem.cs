using UnityEngine;

public class QuestItem : MonoBehaviour
{
    // Variables
    public int itemId;
    public string itemName;
    public bool isCollected;

    // Event to handle item collection
    public event System.Action<int> OnItemCollected;

    // Method to collect the quest item
    public void CollectItem()
    {
        if (!isCollected)
        {
            isCollected = true;
            Debug.Log("Quest item collected: " + itemName);

            // Trigger the event to notify listeners
            if (OnItemCollected != null)
            {
                OnItemCollected(itemId);
            }
        }
    }
}
