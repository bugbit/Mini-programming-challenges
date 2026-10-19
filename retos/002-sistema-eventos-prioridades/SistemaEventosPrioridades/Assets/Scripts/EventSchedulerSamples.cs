using System;
using UnityEngine;

namespace EventSystemWithPpriorities
{
    public class EventSchedulerSamples : MonoBehaviour
    {
        private EventScheduler _eventScheduler = new EventScheduler { PrintLogs = false };
        private int nextEventId = 1;

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            if (GUILayout.Button("Create 10 Events"))
            {
                CreateEvents(10);
            }

            if (GUILayout.Button("Create 100.000 Events"))
            {
                CreateEvents(100000);
            }

            if (GUILayout.Button("Execute 5 Events"))
            {
                _eventScheduler.ExecuteNext(5);
            }

            if (GUILayout.Button("Execute 10 Events"))
            {
                _eventScheduler.ExecuteNext(10);
            }

            if (GUILayout.Button("Execute 100 Events"))
            {
                _eventScheduler.ExecuteNext(100);
            }

            GUILayout.EndVertical();
        }

        private void CreateEvents(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int priority = UnityEngine.Random.Range(0, 10);
                string eventId = $"Event_{nextEventId++}";
                _eventScheduler.TrySchedule(eventId, priority,
                    () => Debug.Log($"Executing {eventId} with priority {priority}"));
            }
        }
    }
}