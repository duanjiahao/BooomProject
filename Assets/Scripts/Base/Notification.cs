using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : Singleton<Notification>
{
    public const string PlayerDataEquipmentChanged = "PlayerDataEquipmentChanged";

    public const string PlayerDataAttributeChanged = "PlayerDataAttributeChanged";
    
    public const string PlayerItemOverflow = "PlayerItemOverflow";
    
    public const string BattleActionPointsChanged = "BattleActionPointsChanged";
    
    public const string BattleAfterHeroPerform = "BattleAfterHeroPerform";
    
    public const string BattleAfterMonsterPerform = "BattleAfterMonsterPerform";

    public delegate void NotificationHandler(object data = null);

    private Dictionary<string, NotificationHandler> handlerDic = new Dictionary<string, NotificationHandler>();

    public void Register(string notificationName, NotificationHandler handler) 
    {
        if (handlerDic.TryGetValue(notificationName, out var notificationHandler))
        {
            notificationHandler += handler;
            handlerDic[notificationName] = notificationHandler;
        }
        else 
        {
            handlerDic.Add(notificationName, handler);
        }
    }

    public void Unregister(string notificationName, NotificationHandler handler) 
    {
        if (handlerDic.TryGetValue(notificationName, out var notificationHandler))
        {
            notificationHandler -= handler;
            handlerDic[notificationName] = notificationHandler;
        }
    }

    public void Notify(string notificationName, object data = null) 
    {
        if (handlerDic.TryGetValue(notificationName, out var notificationHandler)) 
        {
            notificationHandler.Invoke(data);
        }
    }
}
