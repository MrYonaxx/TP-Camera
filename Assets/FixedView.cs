using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedView : AView
{
	public float yaw;
	public float pitch;
	public float roll;
	public float fov;

	public override CameraConfiguration GetConfiguration()
	{
		CameraConfiguration res = new CameraConfiguration();
		res.yaw = yaw;
		res.pitch = pitch;
		res.roll = roll;
		res.fieldOfView = fov;
		res.pivot = this.transform.position;
		res.distance = 0;
		return res;
	}

}
