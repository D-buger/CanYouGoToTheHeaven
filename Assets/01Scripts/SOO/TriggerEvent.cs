using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class TriggerEvent
{
    public delegate void Func(PointerEventData eventData);

    public static EventTrigger.Entry MakeNewEvent(EventTriggerType eventType, Func action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener((data) => { action((PointerEventData)data); });

        return entry;
    }

    public static void AddNewEvent(EventTrigger trigger, params EventTrigger.Entry[] entry)
    {
        for(int i = 0; i < entry.Length; i++)
        {
            trigger.triggers.Add(entry[i]);
        }
    }
}
