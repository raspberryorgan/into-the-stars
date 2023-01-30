using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager 
{
    private static UpdateManager _instance;
    public static UpdateManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new UpdateManager();
            return _instance;
        }
        private set { }
    }

    List<IUpdate> allUpdateElements = new List<IUpdate>();
    List<IFixedUpdate> allFixedUpdateElements = new List<IFixedUpdate>();

    public void Initialize()
    {
        List<IUpdate> allUpdateElements = new List<IUpdate>();
        List<IFixedUpdate> allFixedUpdateElements = new List<IFixedUpdate>();
        Instance = this;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < allUpdateElements.Count; i++)
        {
            allUpdateElements[i].OnUpdate();
        }
    }

    public void OnFixedUpdate()
    {
        for (int i = 0; i < allFixedUpdateElements.Count; i++)
        {
            allFixedUpdateElements[i].OnFixedUpdate();
        }
    }

    public void ClearAll()
    {
        allUpdateElements = new List<IUpdate>();
        allFixedUpdateElements = new List<IFixedUpdate>();
    }


    public void AddUpdate(IUpdate element)
    {
        if (!allUpdateElements.Contains(element))
            allUpdateElements.Add(element);
    }

    public void RemoveUpdate(IUpdate element)
    {
        if (allUpdateElements.Contains(element))
            allUpdateElements.Remove(element);
    }


    public void AddFixedUpdate(IFixedUpdate element)
    {
        if (!allFixedUpdateElements.Contains(element))
            allFixedUpdateElements.Add(element);
    }

    public void RemoveFixedUpdate(IFixedUpdate element)
    {
        if (allFixedUpdateElements.Contains(element))
            allFixedUpdateElements.Remove(element);
    }


}
