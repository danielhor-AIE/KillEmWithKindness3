using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGameEvent : MonoBehaviour
{
    private string buildingName;

    public BuildingGameEvent(string buildingName)
    {
        this.buildingName = buildingName;
    }

    public string BuildingName { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
