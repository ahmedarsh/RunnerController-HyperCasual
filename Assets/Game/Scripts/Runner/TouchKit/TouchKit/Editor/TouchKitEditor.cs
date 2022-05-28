#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor( typeof( MTouchInput ) )]
public class TouchKitEditor : Editor
{
	private bool showDebug = true;
	private string status = "Touch Debugging";


	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		showDebug = EditorGUILayout.Foldout( showDebug, status );
		if( showDebug )
		{
			MTouchInput mTouchInput = (MTouchInput)target;
			mTouchInput.drawDebugBoundaryFrames = EditorGUILayout.Toggle( "Draw boundary frames", mTouchInput.drawDebugBoundaryFrames );
			mTouchInput.drawTouches = EditorGUILayout.Toggle( "Draw touches", mTouchInput.drawTouches );
			mTouchInput.simulateTouches = EditorGUILayout.Toggle( "Simulate touches", mTouchInput.simulateTouches );

			GUI.enabled = mTouchInput.simulateTouches;
			if( GUI.enabled || true )
			{
				var helpText = "Touches can be simulated in the editor or on the desktop with mouse clicks.";
				if( mTouchInput.simulateMultitouch )
					helpText += "\nTo simulate a two-finger gesture, press and hold the left alt key and move your mouse around. Shift the touches by additionally holding down left shift.";

				EditorGUILayout.HelpBox( helpText, MessageType.Info, true );
			}

			mTouchInput.simulateMultitouch = EditorGUILayout.Toggle( "Simulate multitouch", mTouchInput.simulateMultitouch );
		}

		if( GUI.changed )
			EditorUtility.SetDirty( target );
	}

}
#endif