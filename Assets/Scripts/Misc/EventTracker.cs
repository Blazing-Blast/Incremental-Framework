using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// Keeps track of all 
public class GameEvent
{
    public int id;
    public string name;
    public bool isActive;
}

public class EventTracker : MonoBehaviour
{
    public List<GameEvent> events;
    public void Start()
    {
        events = new List<GameEvent>();
        // Add you actual GameEvents here, like this:
        // events.Add(newGameEvent(0, "Example Event", false));
    }

    public void Update()
    {
        // Add your conditions for toggeling an GameEvent here
    }

    // Makes a new GameEvent provided it's values, defaulting to no name and inactive
    public GameEvent newGameEvent(int id, string name = "No Name Given", bool isActive = false)
    {
        GameEvent gameEvent = new()
        {
            id = id,
            name = name,
            isActive = isActive
        };
        return gameEvent;
    }
}
