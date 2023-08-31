using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

public class TestScript : MonoBehaviour, MMEventListener<MMAchievementUnlockedEvent>
{
    // Start is called before the first frame update
    void Start()
    {
        MMGameEvent.Trigger("GameStart");
    }

    private void OnEnable()
    {
        this.MMEventStartListening<MMAchievementUnlockedEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<MMAchievementUnlockedEvent>();
    }

    public virtual void OnMMEvent(MMAchievementUnlockedEvent unlockedEvent)
    {
        Debug.Log("Achievememt Unlocked???");
    }
}