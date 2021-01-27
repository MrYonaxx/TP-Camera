using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFollowView : AView
{
	public Curve curve;
	public float curvePosition;
	public float curveSpeed;

	[Space]
	public Transform target;
	[Space]
	public float yaw;
	public float yawSpeed;
	[Space]
	public float[] pitch = new float[3];
	public float[] roll = new float[3];
	[Space]
	public float[] fov = new float[3];

	public override CameraConfiguration GetConfiguration()
	{
		curvePosition += Input.GetAxis("Vertical") * Time.deltaTime * curveSpeed;
		curvePosition = Mathf.Clamp(curvePosition, 0f, 1f);
		yaw += Input.GetAxis("Horizontal") * Time.deltaTime * yawSpeed;


		CameraConfiguration res = new CameraConfiguration();
		Vector3 rotation = MathsUtils.QuadraticBezier(new Vector3(pitch[0], yaw, roll[0]), 
													  new Vector3(pitch[1], yaw, roll[1]), 
													  new Vector3(pitch[2], yaw, roll[2]), curvePosition);

		Matrix4x4 curveToWorldMatrix = Matrix4x4.TRS(target.transform.position, Quaternion.Euler(rotation), this.transform.localScale);

		res.distance = Vector3.Distance(curve.GetPosition(curvePosition, curveToWorldMatrix), target.transform.position);
		res.pivot = target.transform.position;

		res.yaw = yaw;
	    res.pitch = rotation.x;
		res.roll = rotation.z;
		res.fieldOfView = 60; // test
		return res;
	}




	private void OnDrawGizmos()
	{
		curve.DrawGizmo(Color.yellow, transform.localToWorldMatrix);
	}
}
