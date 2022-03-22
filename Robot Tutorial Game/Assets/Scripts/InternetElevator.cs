using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetElevator : MonoBehaviour
{
	public Vector3 FloorDistance = Vector3.up;
	public float Speed = 1.0f;
	public int Floor = 0;
	public int MaxFloor = 1;
	public Transform moveTransform;

	private float tTotal;
	private bool isMoving;
	private float moveDirection;


	// Use this for initialization
	void Start()
	{
		moveTransform = moveTransform ?? transform;
	}

	// Update is called once per frame
	void Update()
	{
		if (isMoving)
		{
			// elevator is moving
			MoveElevator();
		}
	}

	void MoveElevator()
	{
		var v = moveDirection * FloorDistance.normalized * Speed;
		var t = Time.deltaTime;
		var tMax = FloorDistance.magnitude / Speed;
		t = Mathf.Min(t, tMax - tTotal);
		moveTransform.Translate(v * t);
		tTotal += t;
		print(tTotal);

		if (tTotal >= tMax)
		{
			// we arrived on floor
			isMoving = false;
			tTotal = 0;
			Floor += (int)moveDirection;
			print(string.Format("elevator arrived on floor {0}!", Floor));
		}
	}

	/// <summary>
	/// Start moving up one floor
	/// </summary>
	public void StartMoveUp()
	{
		if (isMoving)
			return;

		isMoving = true;
		moveDirection = 1;
	}

	/// <summary>
	/// Start moving down one floor
	/// </summary>
	public void StartMoveDown()
	{
		if (isMoving)
			return;

		isMoving = true;
		moveDirection = -1;
	}

	/// <summary>
	/// Tell the elevator to move up or down
	/// </summary>
	public void CallElevator()
	{
		if (isMoving)
			return;

		print("elevator starts moving!");

		// start moving
		if (Floor < MaxFloor)
		{
			StartMoveUp();
		}
		else
		{
			StartMoveDown();
		}

	}
}
