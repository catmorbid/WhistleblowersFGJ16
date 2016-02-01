using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;


public class Objective
{
    Goals.Objects m_object;
    Goals.Triggers m_trigger;
    Objective m_secondaryObjective;

    public Objective( Goals.Objects obj )
    {
        m_object = obj;
        m_trigger = Goals.Triggers.None;
    }
    public Objective( Goals.Objects obj, Goals.Triggers trigger )
    {
        m_object = obj;
        m_trigger = trigger;
    }
    public Objective (Goals.Objects obj, Objective secondary)
    {
        m_object = obj;
        m_secondaryObjective = secondary;
    }

    public bool HasSecondary
    {
        get
        {
            return m_secondaryObjective != null;
        }
    }

    public void UseSecondary()
    {
        if (HasSecondary)
        {
            m_object = m_secondaryObjective.m_object;
            m_trigger = m_secondaryObjective.m_trigger;
            m_secondaryObjective = m_secondaryObjective.m_secondaryObjective;
        }
    }

    public Goals.Objects GoalObject
    {
        get { return m_object; }
    }
    public Goals.Triggers Trigger
    {
        get
        {
            return m_trigger;
        }
    }
}

public static class Goals
{
    public enum Objects
    {
        None,
        EmptyMug,
        FilledCoffeeMug,
        Computer,
        Notepad,
        Toilet,
        Sink,
        Newspaper,
        OfficeDoor,
        ExitDoor,
        Booze,
        Food,
        CookedFood,
        Sofa,
        Bed,
        Shower,
        Radio,
        Phone,
        Letter,
        Cabinet,
        VacuumTube,
        Poster,
        Document
    }
    public enum Triggers
    {
        None,
        Mailbin,
        CoffeeMaker,
        Trashbin,
        Toilet,
        Sink,
        Oven,
        VacuumTube,
        Cabinet
    }
}

public class Reward
{
    private float m_reward;
    private float m_lose;
    public Reward( float win, float lose )
    {
        m_reward = win;
        m_lose = lose;
    }

    public float GetReward()
    {
        return m_reward;
    }
    public float GetPenalty()
    {
        return m_lose;
    }

}

public class TimeConstraint
{
    private float m_startTime;
    private float m_endTime;
    public TimeConstraint( TaskTime duration )
    {
        m_startTime = GameClock.GetTime();
        m_endTime = m_startTime + duration.seconds;
        Debug.Log( "Start Time: " + m_startTime + " EndTime: " + m_endTime );
    }

    public bool HasTimeLeft
    {
        get
        {
            return ( m_endTime - GameClock.GetTime() > 0f );
        }
    }
    public void ResetTime()
    {
        float duration = m_endTime - m_startTime;
        m_startTime = GameClock.GetTime();
        m_endTime = m_startTime + duration;
    }

}
public struct TaskTime
{
    public int hours;
    public int minutes;

    public TaskTime( int hours, int minutes )
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
        set
        {
            hours = (int) ( value / 3600 );
            minutes = (int) ( ( value % 3600 ) / 60 );
        }
    }

    public static TaskTime operator +( TaskTime t1, TaskTime t2 )
    {
        return new TaskTime( t1.hours + t2.hours, t1.minutes + t2.minutes );
    }
    public static TaskTime operator -( TaskTime t1, TaskTime t2 )
    {
        return new TaskTime( t1.hours - t2.hours, t1.minutes - t2.minutes );
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
    private Objective m_objective;
    private Reward m_reward;
    private TimeConstraint m_timeConstraint;

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

    public TimeConstraint Time
    {
        get
        {
            return m_timeConstraint;
        }
    }

    public Task( string taskName, TaskType type )
    {
        m_taskName = taskName;
        m_taskType = type;
    }

    public Task( string taskName, TaskType type, Objective objective, Reward reward, TimeConstraint timeConstraint ) : this( taskName, type )
    {
        m_objective = objective;
        m_reward = reward;
        m_timeConstraint = timeConstraint;
    }

    internal bool hasObjective( Goals.Objects interactableObjectType )
    {
        return interactableObjectType == m_objective.GoalObject;
    }

    internal bool Resolve()
    {
        if (m_objective.HasSecondary)
        {
            m_objective.UseSecondary();
            return false;
        }
        PlayerScore.ModifyStress( m_reward.GetReward() );
        return true;
    }
    public bool Fail()
    {
        
        PlayerScore.ModifyStress( m_reward.GetPenalty() );
        if ( m_reward.GetPenalty() > 0 )
        {
            PlayerText.ShowSpeechBubble( "Oh no, oh no oh no no no no I failed, no I don't want to fail...", 3f );
            //Play fail sound
        }
        return true;
    }

    internal bool hasObjective( Goals.Objects obj, Goals.Triggers trigger )
    {
        return ( m_objective.GoalObject == obj && m_objective.Trigger == trigger );
    }
}

public class InteractTask : Task
{
    private string m_object;
    public InteractTask( string taskName, TaskType type, string objectName ) : base( taskName, type )
    {

    }
}
public class TaskManager : MonoBehaviour
{

    public static TaskManager m_singleton;
    private static Color32[] m_taskTypeColors = new Color32[] {new Color32(228,6,74,255), new Color32(0,143,255,255), new Color32(180,180,180,255) };
    private List<Task> m_taskList = new List<Task>();
    private Text m_text;

    public Color32 GetTaskColor( Task.TaskType t )
    {
        try
        {
            return m_taskTypeColors[ (int) t ];
        }
        catch ( Exception e )
        {
            Debug.LogException( e );
        }
        return Color.white;
    }

    void Awake()
    {
        m_singleton = this;
        TaskFactory.Initialize();
    }

    // Use this for initialization
    void Start()
    {
        m_text = GetComponent<Text>();
        //AddTask( new Task( "Drink coffee", Task.TaskType.Personal, new Objective( Goals.Objects.CoffeeMug ), new Reward( -10, 10 ), new TimeConstraint( new TaskTime( 10, 0 ) ) ) );
        //AddTask( new Task( "Write an essay on Father's philantrophistic deeds", Task.TaskType.Important, new Objective( Goals.Objects.Computer ), new Reward( -5, 5 ), new TimeConstraint( new TaskTime( 10, 0 ), new TaskTime( 15, 30 ) ) ) );
        //AddTask( new Task( "Open the Door", Task.TaskType.Optional, new Objective( Goals.Objects.OfficeDoor ), new Reward( -1, 0 ), null ) );
    }

    private float m_lastUpdate = 0f;
    private float m_updateFrequency = 10f;
    // Update is called once per frame
    void Update()
    {
        m_text.text = CreateTasksString();
        updateTasks();

    }

    private void updateTasks()
    {
        if ( m_lastUpdate + m_updateFrequency > +Time.realtimeSinceStartup )
        {
            for ( int i = 0; i < m_taskList.Count; i++ )
            {
                Task t = m_taskList[i];
                if ( !t.Time.HasTimeLeft )
                {
                    if ( t.Fail() )
                    {
                        Debug.Log( "Task ran out of time and failed! " );
                        m_taskList.Remove( t );
                        i--;
                    }
                }
            }
            m_lastUpdate = Time.realtimeSinceStartup;
        }
    }

    public string CreateTasksString()
    {
        string taskstring = "";
        foreach ( Task t in m_taskList )
        {
            string c = ColorUtility.ToHtmlStringRGB(GetTaskColor(t.Type));
            taskstring += String.Format( "<color=#{0}>{1}</color>\n", c, t.Name );
        }

        return taskstring;
    }

    public static void AddTask( Task newTask )
    {
        m_singleton.m_taskList.Add( newTask );
        m_singleton.sortTasks();
    }
    public static void AddTask( Task[] tasks )
    {
        m_singleton.m_taskList.AddRange( tasks );
        m_singleton.sortTasks();
    }
    public static void RemoveTask( Task taskToRemove )
    {
        m_singleton.m_taskList.Remove( taskToRemove );
    }
    private void sortTasks()
    {
        Debug.Log( "TODO: task sorting" );
    }
    public static void RegisterInteraction( Interactable obj )
    {
        obj.ObjectInteractionEvent += m_singleton.OnObjectInteracted;
    }
    public static void RegisterTrigger( TaskTrigger trigger )
    {
        trigger.TaskTriggerEvent += m_singleton.OnTaskTriggerFired;
    }
    public void OnObjectInteracted( Interactable obj )
    {
        Debug.Log( "Handling interactions" );
        for ( int i = 0; i < m_taskList.Count; i++ )
        {
            Task t = m_taskList[i];
            if ( t.hasObjective( obj.InteractableObjectType ) )
            {
                if ( t.Resolve() )
                {
                    m_taskList.Remove( t );
                    i--;
                }

            }
        }
    }
    public void OnTaskTriggerFired( TaskTrigger trigger, Interactable obj )
    {
        Debug.Log( "Handling triggers" );
        for ( int i = 0; i < m_taskList.Count; i++ )
        {
            Task t = m_taskList[i];
            if ( t.hasObjective( obj.InteractableObjectType, trigger.TriggerType ) )
            {
                if ( t.Resolve() )
                {
                    trigger.AudioSrc.Play();
                    m_taskList.Remove( t );
                    i--;
                }

            }
        }
    }

    public static bool HasTaskWithGoal( Goals.Objects objective )
    {
        foreach (Task t in m_singleton.m_taskList)
        {
            if ( t.hasObjective( objective ) )
                return true;
        }
        return false;
    }
}
