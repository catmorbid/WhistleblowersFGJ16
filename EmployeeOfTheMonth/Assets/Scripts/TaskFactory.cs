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

    public static Task[] SleepTasks
    {
        get
        {
            Task[] tasks = new Task[]
            {
                new Task( "Go to sleep", Task.TaskType.Personal, new Objective(Goals.Objects.Bed), new Reward(-250,200),new TimeConstraint(new TaskTime(6,0))),
                new Task("Brush your teeth",Task.TaskType.Personal, new Objective(Goals.Objects.Sink), new Reward(-50, 75), new TimeConstraint(new TaskTime(2,0))),
                new Task("Take a shower",Task.TaskType.Personal, new Objective(Goals.Objects.Shower), new Reward(-50,75), new TimeConstraint(new TaskTime(2,0))),
                new Task("Use the toilet",Task.TaskType.Personal,new Objective(Goals.Objects.Toilet), new Reward(-50, 75), new TimeConstraint(new TaskTime(2,0)))
            };

            return tasks;
        }
    }
    public static Task[] FreeTimeTasks
    {
        get
        {
            Task[] tasks = new Task[]
            {
                new Task("Drink some whiskey",Task.TaskType.Personal, new Objective(Goals.Objects.Booze), new Reward(-50,100), new TimeConstraint(new TaskTime(3,0))),
                new Task("Relax on the your leisure chair",Task.TaskType.Personal, new Objective(Goals.Objects.Sofa), new Reward(-50,100), new TimeConstraint(new TaskTime(3,0))),
                new Task("Take a Shower",Task.TaskType.Personal, new Objective(Goals.Objects.Shower), new Reward(-50,100), new TimeConstraint(new TaskTime(3,0)))
            };
            return tasks;

        }
    }

    private Task[] GetRandomFreeTimeTasks( int count )
    {
        throw new NotImplementedException();
    }

    public static Task RandomWorkTask
    {
        get
        {
            int i = UnityEngine.Random.Range(0,m_singleton.m_workTasks.Count);
            Task task = m_singleton.m_workTasks[ i ];
            Debug.Log( "Randomized Task " + i+": " +task.Name );
            return task;
        }
    }
    public static Task[] LunchTask
    {
        get
        {
            Task[] t = new Task[] {
            new Task("Cook some food in the Oven",Task.TaskType.Personal, new Objective(Goals.Objects.Food, Goals.Triggers.Oven), new Reward(-50,100), new TimeConstraint(new TaskTime(1,0))),
            new Task("Eat the cooked food",Task.TaskType.Personal, new Objective(Goals.Objects.CookedFood), new Reward(-50,150), new TimeConstraint(new TaskTime(1,0))),
            new Task("Eat uncooked food",Task.TaskType.Optional, new Objective(Goals.Objects.Food), new Reward(-25,0), new TimeConstraint(new TaskTime(1,0))),
            new Task("Make some Coffee", Task.TaskType.Personal, new Objective(Goals.Objects.EmptyMug, Goals.Triggers.CoffeeMaker), new Reward(-25,25), new TimeConstraint(new TaskTime(1,0))),
            new Task("Drink some Coffee", Task.TaskType.Personal, new Objective(Goals.Objects.FilledCoffeeMug), new Reward(-25,25), new TimeConstraint(new TaskTime(1,0)))
            };
            return t;
        }
    }

    public static Task[] BasicWorkTask
    {
        get
        {
            return new Task[] {
            new Task( "Check the mail tube occasionally for new tasks", Task.TaskType.Important, new Objective( Goals.Objects.VacuumTube ), new Reward( 0, 0 ), new TimeConstraint( new TaskTime( 10, 0 ) ) ),
            new Task( "Open the door to your office and enter", Task.TaskType.Important, new Objective( Goals.Objects.OfficeDoor ), new Reward( -10, 50 ), new TimeConstraint( new TaskTime( 0, 20 ) ) )
            };
        }
    }
    public static Task[] MorningChores
    {
        get
        {
            Task[] tasks = new Task[]
            {
                new Task("Read the Letter in your mailbox",Task.TaskType.Important, new Objective(Goals.Objects.Letter), new Reward(0, 0), new TimeConstraint(new TaskTime(24,0))),
                new Task("Brush your teeth",Task.TaskType.Personal, new Objective(Goals.Objects.Sink), new Reward(-25, 75), new TimeConstraint(new TaskTime(2,0))),
                new Task("Take a shower",Task.TaskType.Personal, new Objective(Goals.Objects.Shower), new Reward(-25,75), new TimeConstraint(new TaskTime(2,0))),
                new Task("Use the toilet",Task.TaskType.Personal,new Objective(Goals.Objects.Toilet), new Reward(-25, 75), new TimeConstraint(new TaskTime(2,0))),
                new Task("Make some Coffee", Task.TaskType.Personal, new Objective(Goals.Objects.EmptyMug, Goals.Triggers.CoffeeMaker), new Reward(-25,75), new TimeConstraint(new TaskTime(2,0))),
                new Task("Drink some Coffee", Task.TaskType.Personal, new Objective(Goals.Objects.FilledCoffeeMug), new Reward(-25,75), new TimeConstraint(new TaskTime(2,0)))
            };
            return tasks;
        }
    }

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
        m_workTasks.Add( new Task( "Type down the provided facts in the Historical Database", Task.TaskType.Important, new Objective( Goals.Objects.Computer ), new Reward( -20, 50 ), new TimeConstraint( new TaskTime( 2, 0 ) ) ) );
        m_workTasks.Add( new Task( "Write an essay on the gloriousness of our Father the Supreme Leader", Task.TaskType.Important, new Objective( Goals.Objects.Computer ), new Reward( -20, 50 ), new TimeConstraint( new TaskTime( 2, 0 ) ) ) );
        m_workTasks.Add( new Task( "Write a speech where Father will explain why wellfare is bad thing to have", Task.TaskType.Important, new Objective( Goals.Objects.Computer ), new Reward( -20, 50 ), new TimeConstraint( new TaskTime( 2, 0 ) ) ) );
        m_workTasks.Add( new Task( "Take the important documents to the archive", Task.TaskType.Important, new Objective( Goals.Objects.Letter, Goals.Triggers.Cabinet), new Reward( -20, 50 ), new TimeConstraint( new TaskTime( 2, 0 ) ) ) );
        m_workTasks.Add( new Task( "Listen to the radio. There's an important message coming up", Task.TaskType.Important, new Objective( Goals.Objects.Radio), new Reward( -20, 50 ), new TimeConstraint( new TaskTime( 2, 0 ) ) ) );
        m_workTasks.Add( new Task( "Answer the phone, then write down the conversation", Task.TaskType.Important, new Objective( Goals.Objects.Phone, new Objective(Goals.Objects.Notepad) ), new Reward( -20, 50 ), new TimeConstraint( new TaskTime( 2, 0 ) ) ) );
        m_workTasks.Add( new Task( "Fetch some documents from the archive and dispose of them", Task.TaskType.Important, new Objective( Goals.Objects.Cabinet, new Objective(Goals.Objects.Letter, Goals.Triggers.Trashbin) ), new Reward( -20, 50 ), new TimeConstraint( new TaskTime( 2, 0 ) ) ) );
        m_workTasks.Add( new Task( "Listen up, take these documents and put them in YOUR mail box,  for pickup.", Task.TaskType.Important, new Objective( Goals.Objects.Letter, Goals.Triggers.Mailbin ), new Reward( -20, 50 ), new TimeConstraint( new TaskTime( 2, 0 ) ) ) );
        //Debug.Log( "TODO: do these" );
    }

    private void createPersonalTasks()
    {
        Debug.Log( "TODO: do these" );
    }
}
