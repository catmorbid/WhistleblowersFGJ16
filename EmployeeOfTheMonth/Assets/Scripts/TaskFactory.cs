using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TaskFactory
{
    private List<Task> m_personalTasks = new List<Task>();
    private List<Task> m_workTasks= new List<Task>();
    private List<Task> m_optionalTasks = new List<Task>();
    private static TaskFactory m_singleton;
    internal static void Initialize()
    {
        m_singleton = new TaskFactory();

        m_singleton.createPersonalTasks();
        m_singleton.createWorkTasks();
        m_singleton.createOptionalTasks();
    }

    private void createOptionalTasks()
    {
        Debug.Log( "TODO: do these" );
    }

    private void createWorkTasks()
    {
        Debug.Log( "TODO: do these" );
    }

    private void createPersonalTasks()
    {
        Debug.Log( "TODO: do these" );
    }
}
