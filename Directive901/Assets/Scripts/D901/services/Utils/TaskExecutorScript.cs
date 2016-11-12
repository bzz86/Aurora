using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public delegate void Task();

public class TaskExecutorScript : D901Singleton<TaskExecutorScript> {

	private Queue<Task> TaskQueue = new Queue<Task>();
	private object _queueLock = new object();

	void Awake(){
		base.onAwake(this);
	}

	void Update () {
		lock (_queueLock)
		{
			if (TaskQueue.Count > 0)
				TaskQueue.Dequeue()();
		}
	}

	public void ScheduleTask(Task newTask)
	{
		lock (_queueLock)
		{
			if (TaskQueue.Count < 100)
				TaskQueue.Enqueue(newTask);
		}
	}
}