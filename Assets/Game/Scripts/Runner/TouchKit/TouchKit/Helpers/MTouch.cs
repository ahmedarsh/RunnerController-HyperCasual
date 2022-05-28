using System;
using UnityEngine;



public class MTouch
{
	public readonly int fingerId;
	public Vector2 position;
	public Vector2 startPosition;
	public Vector2 deltaPosition;
	public float deltaTime;
	public int tapCount;
	public TouchPhase phase = TouchPhase.Ended;

	public static Action<Vector2> TapHold;
	public static Action<Vector2> TapBegan;
	public static Action<Vector2> TapEnd;
	
	public Vector2 previousPosition
	{
		get { return position - deltaPosition; }
	}

#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER || UNITY_WEBGL
    // used to track mouse movement and fake touches
    private Vector2? _lastPosition;
	private double _lastClickTime;
	private double _multipleClickInterval = 0.2;
#endif


	public MTouch( int fingerId )
	{
		// lock this TKTouch to the fingerId
		this.fingerId = fingerId;
	}

	public MTouch populateWithTouch( Touch touch )
	{
		position = touch.position;
		deltaPosition = touch.deltaPosition;
		deltaTime = touch.deltaTime;
		tapCount = touch.tapCount;

		if (touch.phase== TouchPhase.Began)
		{
			startPosition = position;
			TapBegan(deltaPosition);
		}

		if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
		{
			TapHold(deltaPosition);
		}
		

		// canceled and ended are the same to us
		if( touch.phase == TouchPhase.Canceled )
			phase = TouchPhase.Ended;
		else
			phase = touch.phase;


		if (touch.phase == TouchPhase.Ended)
		{
			TapEnd(deltaPosition);
		}

		return this;
	}

#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER || UNITY_WEBGL
    public MTouch populateWithPosition( Vector3 currentPosition, TouchPhase touchPhase )
	{
		var currentPosition2d = new Vector2( currentPosition.x, currentPosition.y );

		// if we have a lastMousePosition use it to get a delta
		if( _lastPosition.HasValue )
			deltaPosition = currentPosition2d - _lastPosition.Value;
		else
			deltaPosition = new Vector2( 0, 0 );

		switch( touchPhase )
		{
			case TouchPhase.Began:
				phase = TouchPhase.Began;
				
				TapBegan(deltaPosition);
				// check for multiple clicks
				if( Time.time < _lastClickTime + _multipleClickInterval )
					tapCount++;
				else
					tapCount = 1;
				_lastPosition = currentPosition2d;
				startPosition = currentPosition2d;
				_lastClickTime = Time.time;
				break;
			case TouchPhase.Stationary:
			case TouchPhase.Moved:
				if( deltaPosition.sqrMagnitude == 0 )
					phase = TouchPhase.Stationary;
				else
					phase = TouchPhase.Moved;

				_lastPosition = currentPosition2d;
				TapHold(deltaPosition);
				break;
			case TouchPhase.Ended:
				phase = TouchPhase.Ended;
				_lastPosition = null;
				TapEnd(deltaPosition);
				break;
		}

		position = currentPosition2d;

		return this;
	}


	public MTouch populateFromMouse()
	{
		// do we have some input to work with?
		if( Input.GetMouseButtonUp( 0 ) || Input.GetMouseButton( 0 ) )
		{
			var phase = TouchPhase.Moved;

			// guard against down and up being called in the same frame
			if( Input.GetMouseButtonDown( 0 ) && Input.GetMouseButtonUp( 0 ) )
				phase = TouchPhase.Canceled;
			else if( Input.GetMouseButtonUp( 0 ) )
				phase = TouchPhase.Ended;
			else if( Input.GetMouseButtonDown( 0 ) )
				phase = TouchPhase.Began;

			var currentMousePosition = new Vector2( Input.mousePosition.x, Input.mousePosition.y );
			this.populateWithPosition( currentMousePosition, phase );
		}

		return this;
	}
#endif


	public override string ToString()
	{
		return string.Format( "[TKTouch] fingerId: {0}, phase: {1}, position: {2}", fingerId, phase, position );
	}

}