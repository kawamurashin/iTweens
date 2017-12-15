using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class iTweens : MonoBehaviour
{
	private static iTweens _instance;
	public static void Serial(GameObject target, Hashtable args)
	{
		_instance = target.AddComponent<iTweens>();	
		var hashtable = new Hashtable();
		hashtable.Add("count",0);
		hashtable.Add("target", target);
		hashtable.Add("args", args);
		SerialTween(hashtable);
	}
	
	public static void Parallel(GameObject target, Hashtable args)
	{
		_instance = target.AddComponent<iTweens>();	
		var hashtable = new Hashtable();
		hashtable.Add("count",0);
		hashtable.Add("target", target);
		hashtable.Add("args", args);
		ParallelTween(hashtable);
	}

	private static void SerialTween(Hashtable hashtable)
	{
		var target = (GameObject)hashtable["target"];
		var args = (Hashtable)hashtable["args"];
		var list = (List<Hashtable>)args["list"];
		var iTweenHashtable = list[(int) hashtable["count"]];
		var go = (GameObject) iTweenHashtable["target"];
		var hash = (Hashtable) iTweenHashtable["hash"];
		hash.Add ("oncompletetarget", target);
		hash.Add("oncompleteparams", hashtable);
		hash.Add ("oncomplete", "SerialTweenComplete");
		iTween.MoveAdd(go,hash);
	}
	
	private static void ParallelTween(Hashtable hashtable)
	{
		var target = (GameObject)hashtable["target"];
		var args = (Hashtable)hashtable["args"];
		var list = (List<Hashtable>)args["list"];
		hashtable["count"] = 0;
		var n = list.Count;
		for (var i = 0; i < n; i++)
		{
			var iTweenHashtable = list[i];
			var go = (GameObject) iTweenHashtable["target"];
			var hash = (Hashtable) iTweenHashtable["hash"];
			hash.Add ("oncompletetarget", target);
			hash.Add("oncompleteparams", hashtable);
			hash.Add ("oncomplete", "ParallelTweenComplete");
			iTween.MoveAdd(go,hash);
		}
	}

	protected void SerialTweenComplete(Hashtable hashtable)
	{
		var args = (Hashtable)hashtable["args"];
		var list = (List<Hashtable>)args["list"];
		hashtable["count"] = 1 + (int)hashtable["count"];
		if ((int) hashtable["count"] == list.Count)
		{
			AllComplete(hashtable);
		}
		else
		{
			SerialTween(hashtable);
		}
	}

	protected void ParallelTweenComplete(Hashtable hashtable)
	{
		var args = (Hashtable)hashtable["args"];
		var list = (List<Hashtable>)args["list"];
		hashtable["count"] = 1 + (int)hashtable["count"];
		if ((int) hashtable["count"] == list.Count)
		{
			AllComplete(hashtable);
		}
	}

	private void AllComplete(Hashtable hashtable)
	{
		Destroy(this);
		var args = (Hashtable)hashtable["args"];
		var target = (GameObject) args["oncompletetarget"];
		var complteFunc = (string) args["oncomplete"];
		target.SendMessage(complteFunc);
	}
}
