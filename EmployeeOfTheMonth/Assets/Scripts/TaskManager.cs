using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public struct TaskTime
{
    public int hours;
    public int minutes;

    public TaskTime(int hours, int minutes)
    {
        this.hours = hours;
        this.minutes = minutes;
    }

    public float seconds
    {
        get
        {
            return ( minutes * 60 + hours * 3600 );
        }
    }

    public static TaskTime operator +(TaskTime t1, TaskTime t2)
    {
        return new TaskTime( t1.hours + t2.hours, t1.minutes + t2.minutes );
    }
    public static TaskTime operator -( TaskTime t1, TaskTime t2 )
    {
        return new TaskTime( t1.hours - t2.hours, t1.minutes - t2.minutes );
    }
}

public class TaskAction
{
    private Task m_task;
    public TaskAction(Task task)
    {
        m_task = task;
    }        

    public void ResolveTaskAction()
    {
        Debug.Log( "Task Action Resolved" );
        
    }
}
public class Task
{
    public enum TaskType
    {
        Personal,
        Important,
        Optional
    }

    private string m_taskName;
    private TaskType m_taskType;

    public string Name
    {
        get
        {
            return m_taskName;
        }
        set
        {
            m_taskName = value;
        }
    }

    public TaskType Type
    {
        get
        {
            return m_taskType;
        }
        set
        {
            m_taskType = value;
        }
    }

    public float TimeRemaining
    {
        get
        {
            Debug.Log( "Todo time remaining" );
            return 0;
        }
    }

    public Task(string taskName, TaskType type)
    {
        m_taskName = taskName;
        m_taskType = type;
    }

    public Task( string taskName, TaskType type, TaskTime startTime, TaskTime endTime, TaskAction successfultask, TaskAction failedtask)
    {

    }


}
public class TaskManager : MonoBehaviour {

    public static TaskManager m_singleton;
    private static Color32[] m_taskTypeColors = new Color32[] {new Color32(228,6,74,255), new Color32(0,143,255,255), new Color32(180,180,180,255) };
    private List<Task> m_taskList = new List<Task>();
    private Text m_text;

    public Color32 GetTaskColor(Task.TaskType t)
    {
        try {
            return m_taskTypeColors[ (int) t ];
        }
        catch (Exception e)
        {
            Debug.LogException( e );
        }
        return Color.white;
    }
    
    void Awake()
    {
        m_singleton = this;
    }
    
    // Use this for initialization
    void Start () {
        m_text = GetComponent<Text>();
        AddTask( new Task( "Walk around", Task.TaskType.Personal ) );
        AddTask( new Task( "Drink coffee", Task.TaskType.Personal ) );
        AddTask( new Task( "Write an essay on Father's philantrophistic deeds", Task.TaskType.Important ) );
    }
	
	// Update is called once per frame
	void Update () {
        m_text.text = CreateTasksString();
	}

    public string CreateTasksString()
    {
        string taskstring = "";
        foreach (Task t in m_taskList)
        {
            string c = ColorUtility.ToHtmlStringRGB(GetTaskColor(t.Type));
            taskstring += String.Format( "<color=#{0}>{1}</color>\n", c, t.Name );
        }

        return taskstring;
    }

    public static void AddTask(Task newTask)
    {
        m_singleton.m_taskList.Add( newTask );
        m_singleton.sortTasks();
    }
    public static void RemoveTask(Task taskToRemove)
    {
        m_singleton.m_taskList.Remove( taskToRemove );
    }
    private void sortTasks()
    {
        Debug.Log( "TODO: task sorting" );
    }
}
