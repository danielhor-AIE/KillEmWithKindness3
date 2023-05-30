using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Quest : ScriptableObject
{
   [System.Serializable]
   public struct Info
   {
	   public string Name;
	   public Sprite Icon;
	   public string Description;

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


	[System.Serializable]
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
		{
			//this is where i put my skipping related stuff
		}
	}

	[System.Serializable]
	public abstract class QuestGoals : ScriptableObject
	{
	public List<QuestGoal> Goals;

	}


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
	SerializedProperty m_QuestInfoProperty;
	SerializedProperty m_QuestStatProperty;

	List<string> m_QuestGoalType;
	SerializedProperty m_QuestGoalListProperty;

	[MenuItem("Assets/Quest", priority = 0)]
	public static void CreateQuest()
	{
		var newQuest = CreateInstance<Quest>();

		ProjectWindowUtil.CreateAsset(newQuest, pathName: "quest.asset");
	}

	void OnEnable()
	{
		m_QuestInfoProperty = serializedObject.FindProperty(nameof(Quest.Information));
		m_QuestStatProperty = serializedObject.FindProperty(nameof(Quest.Reward));

		m_QuestGoalListProperty = serializedObject.FindProperty(nameof(Quest.Goals));

		var lookup:Type = typeof(Quest.QuestGoal);

		m_QuestGoalType = System.AppDomain.CurrentDomain.GetAssemblies()//Assembly[]
		.SelectMany(assembly => assembly.GetTypes())
		.Where(x: Type => x.IsClass && !x.IsAbstract && x.IsSubclassOf(lookup))//!Enumeraable
		.Select(type => type.Name)//!Enumerable<string>
		.ToList();//List<string>

	}

	public override void OnInspectorGUI()
	{
		var child:SerializedProperty = m_QuestInfoProperty.Copy();
		var depth:int = child.depth;
		child.NextVisible(enterChildren: false);
	}

	child = m_QuestStatProperty.Copy();
	depth = child.depth;
	child.NextVisible(enterChildren:true);

	EditorGUILayout.LabelField("Quest reward", EditorStyles.boldLabel);
	while (child.depth > depth)
	{
		EditorGUILayout.LabelField("Quest reward", EditorStyles.boldLabel);
		child.NextVisible(enterChildren:false);
	}
}
	int choice = EditorGUILayout.Popup(label: "Add new Quest Goal", selectedIndex: -1,
		displayedOptions: m_QuestGoalType.ToArray())


	if(choice != -1)
	{
		var newInstance = ScriptableObject.CreateInstance(m_QuestGoalType[choice]);
		AssetDatabase.AddObjectToAsset(objectToAdd: newInstancce, assetObject: target);

		m_QuestGoalListProperty.InsertArrayElementAdIndex(m_QuestGoalListProperty.arraySize);
		m_QuestGoalListProperty.GetArrayElementAtIndex(m_QuestGoalListProperty.arraySize - 1)
		.objectReferenceValue = newInstance;
	}
#endif 