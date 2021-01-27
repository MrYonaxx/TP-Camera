using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyView : AView
{
	public float roll;
	public float distance;
	public float fov;

	public Transform target;
	public Rail rail;
	public Vector3 railPosition;

	public float speed;

	public override CameraConfiguration GetConfiguration()
	{
		CameraConfiguration res = new CameraConfiguration();
		res.roll = roll;
		res.fieldOfView = fov;
		distance += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		railPosition = rail.GetPosition(distance);
		res.pivot = railPosition;

		Vector3 dir = (target.position - res.pivot).normalized;
		res.yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
		res.pitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;

		return res;
	}
}
