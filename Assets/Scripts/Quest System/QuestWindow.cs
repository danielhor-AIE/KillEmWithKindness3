using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour
{

    //fields for all the elements
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private Transform goalsContent;
    [SerializeField] private Text xpText;
    [SerializeField] private TextAlignment coinsText;

    public void Initialize(Quest quest)
    {
        titleText.text = quest.Information.Name;
        descriptionText.text = quest.Information.Description;

        foreach (var goal in quest.Goals)
        {
            GameObject goalObj = Instantiate(goalPrefab, goalsContent);
            goalObj.transform.Find("Text").GetComponent<Text>().text = goal.GetDescription();

            GameObject countObj = goalObj.transform.Find("Count").gameObject;
            GameObject skipObj = goalObj.transform.Find("Skip").
        }

        if (goal.Completed)
        {
            countObj.SetActive(false);
            skipObj.SetActive(false);
            goalObj.transform.Find("Done").gameObject.SetActive(true);
        }
        else
        {
            countObj.GetComponent<Text>().text = goalCurrent.Amount + "/" + goalPrefab.RequiredAmount;

            skipObj.GetComponent<Button>().onClick.AddListener(call: delegate
            {
                goal.Skip();

                countObj.SetActive(false);
                skipObj.SetActive(false);
                goalObj.transform.Find("Done").gameObject.SetActive(true);
            });
        }

        xpText.text = quest.Reward.XP.ToString();
        coinsText.text = Quest.Reward.Currency.ToString();
   
    }
    public void CloseWindow()
    {
        gameObject.SetActive(false);

        for (int i = 0; i < goalsContent.ChildAmount)
        {
            Destroy(goalsContent.GetChild(i).gameObject);
        }
}
 
}
