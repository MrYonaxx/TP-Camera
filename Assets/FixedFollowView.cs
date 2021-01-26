using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollowView : AView
{
	public float roll;
	public float fov;
	public Transform target;

	public GameObject centralPoint;
	public float yawOffsetMax;
	public float pitchOffsetMax;

	public override CameraConfiguration GetConfiguration()
	{
		CameraConfiguration res = new CameraConfiguration();
		Vector3 dir = (target.position - transform.position).normalized;
		res.yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
		res.pitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;

		Vector3 dirCenter = (centralPoint.transform.position - transform.position).normalized;
		float yawCenter = Mathf.Atan2(dirCenter.x, dirCenter.z) * Mathf.Rad2Deg;
		float pitchCenter = -Mathf.Asin(dirCenter.y) * Mathf.Rad2Deg;

		/*if (res.yaw > 180)
			res.yaw -= 360;
		else if (res.yaw < -180)
			res.yaw += 360;*/

		res.yaw = Mathf.Clamp(res.yaw, yawCenter - yawOffsetMax, yawCenter + yawOffsetMax);
		res.pitch = Mathf.Clamp(res.pitch, pitchCenter - pitchOffsetMax, pitchCenter + pitchOffsetMax);

		res.roll = roll;
		res.fieldOfView = fov;
		res.pivot = this.transform.position;
		res.distance = 0;
		return res;
	}
}
