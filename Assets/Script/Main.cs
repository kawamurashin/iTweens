using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script
{
	public class Main : MonoBehaviour {
		// Use this for initialization
		private void Start ()
		{
			GameObject obj;
			obj = new GameObject("Canvas");
			var canvas = obj.AddComponent<Canvas>();
			obj.transform.SetParent(this.transform);
			
			obj = new GameObject("Image");
			var image = obj.AddComponent<Image>();
			obj.transform.SetParent(canvas.transform);
			
			List<Hashtable> list = new List<Hashtable>();
			Hashtable iTweenHashtable;
			Hashtable hash;
			Hashtable hashtable;
			//serial sample
			iTweenHashtable = new Hashtable();
			iTweenHashtable.Add("target", obj);
			hash = new Hashtable();
			hash.Add("x",300);
			hash.Add("time",2);
			hash.Add("delay",1f);
			iTweenHashtable.Add("hash",hash);
			list.Add(iTweenHashtable);
			//
			iTweenHashtable = new Hashtable();
			iTweenHashtable.Add("target", obj);
			hash = new Hashtable();
			hash.Add("x",-300);
			hash.Add("time",5);
			iTweenHashtable.Add("hash",hash);
			list.Add(iTweenHashtable);
			
			//
			hashtable = new Hashtable();
			hashtable.Add("list" , list);
			hashtable.Add("oncompletetarget", this.gameObject);
			hashtable.Add("oncomplete" , "OnCompleteSerial");
			iTweens.Serial(this.gameObject, hashtable);

			//palallele sample
			list = new List<Hashtable>();
			//
			obj = new GameObject("Image");
			image = obj.AddComponent<Image>();
			obj.transform.SetParent(canvas.transform);
			obj.transform.localPosition = new Vector3(0,300);
			
			iTweenHashtable = new Hashtable();
			iTweenHashtable.Add("target", obj);
			hash = new Hashtable();
			hash.Add("x",-500);
			hash.Add("time",1);
			hash.Add("delay",0f);
			iTweenHashtable.Add("hash",hash);
			list.Add(iTweenHashtable);
			//
			obj = new GameObject("Image");
			image = obj.AddComponent<Image>();
			obj.transform.SetParent(canvas.transform);
			obj.transform.localPosition = new Vector3(0,100);
			
			iTweenHashtable = new Hashtable();
			iTweenHashtable.Add("target", obj);
			hash = new Hashtable();
			hash.Add("x",-400);
			hash.Add("time",2);
			hash.Add("delay",0f);
			iTweenHashtable.Add("hash",hash);
			list.Add(iTweenHashtable);
			//
			obj = new GameObject("Image");
			image = obj.AddComponent<Image>();
			obj.transform.SetParent(canvas.transform);
			obj.transform.localPosition = new Vector3(0,-100);
			
			iTweenHashtable = new Hashtable();
			iTweenHashtable.Add("target", obj);
			hash = new Hashtable();
			hash.Add("x",-400);
			hash.Add("time",10);
			hash.Add("delay",0f);
			iTweenHashtable.Add("hash",hash);
			list.Add(iTweenHashtable);
			//
			
			hashtable = new Hashtable();
			hashtable.Add("list" , list);
			hashtable.Add("oncompletetarget", this.gameObject);
			hashtable.Add("oncomplete" , "OnCompleteParallel");
			iTweens.Parallel(this.gameObject, hashtable);
		}

		private void OnCompleteParallel()
		{
			Debug.Log("OnCompleteParallel");
		}
		private void OnCompleteSerial()
		{
			Debug.Log("OnCompleteSerial");
		}
	
		// Update is called once per frame
		private void Update () {
		
		}
	}
}
