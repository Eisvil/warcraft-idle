using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsManager : Singleton<AnalyticsManager>
{
    private void OnApplicationQuit()
    {
        AppMetrica.Instance.SendEventsBuffer();
    }
    
    private void OnApplicationPause(bool pause)
    {
        AppMetrica.Instance.SendEventsBuffer();
    }
    
    public void SendEvent(string eventName)
    {
        AppMetrica.Instance.ReportEvent(eventName);
    }
}
