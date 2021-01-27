using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
	[SerializeField]
	private float weight;
	public float Weight
	{
		get { return weight; }
		//set { isActiveOnStart = value; }
	}

	[SerializeField]
	private bool isActiveOnStart = true;
	public bool IsActiveOnStart
	{
		get { return isActiveOnStart; }
		//set { isActiveOnStart = value; }
	}

	protected void Start()
	{
		if (isActiveOnStart == true)
			SetActive(true);
	}

	public virtual void SetActive(bool isActive)
	{
		if (isActive == true) 
		{
			CameraController._instance.AddView(this);
		}
		else
		{
			CameraController._instance.RemoveView(this);
		}
	}

	public virtual CameraConfiguration GetConfiguration()
	{
		return null;
	}

	public void OnDrawGizmos()
	{
		GetConfiguration().DrawGizmos(Color.red);
	}

}
