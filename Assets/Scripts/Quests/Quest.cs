using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : ScriptableObject
{
   [System.Serializable]
   public struct Info
   {
	   public string Name;
	   public Sprite Icon;
	   public string Description

   }


    [System.Serializable]
	public struct Stat
	{
		public int Currency;
		public int XP;
	}


	[Header("Reward")] public Stat Reward = new Stat {Currency = 10, XP = 10};

	public bool Completed { get; private set; }
	public QuestCompletedEvent QuestCompleted;

	public abstract class QuestGoal : ScriptableObject
	{
		protected string Description;
		public int CurrentAmount { get; protected set; }
		public int RequiredAmount = 1;

		public bool Completed { get; protected set; }
		[HideInInspector] public UnityEvent GoalCompleted;

		public virtual string GetDescription()
		{
			return Description;
		}

		public virtual void Initialize()
		{
			Completed = false;
			GoalCompleted = new UnityEvent();
		}

		protected void Evaluate()
		{
			if (CurrentAmount >= RequiredAmount)
			{
				Complete();
			}
		}

		private void Complete()
		{
			Completed = true;
			GoalCompleted.Invoke();
			GoalCompleted.RemoveAllListeners();
		}

		public void Skip()
	}

	public abstract class QuestGoal : ScriptableObject{...}

	public List<QuestGoal> Goals;

	public void Initialize()
	{
		Completed = false;
		QuestCompleted = new QuestCompletedEvent();

		foreach (var goal in Goals)
		{
			goal.Initialize();
			goal.GoalCompleted.AddListener(call: delegate { CheckGoals(); });
		}
	}

	private void CheckGoals()
	{
		Completed = Goals.All(g :QuestGoal => g.Completed);
		if (Completed)
		{
			//give Reward
			QuestComplete.Invoke(this);
			QuestCompleted.RemoveAllListeners();
		}
	}
}

public class QuestCompletedEvent : UnityEvent<Quest> { }

#if UNITY_EDITOR
[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{

}
#endif 