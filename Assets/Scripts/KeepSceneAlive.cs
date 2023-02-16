using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/* This script prevents Unity from switching to the "Game" tab in the editor,
 * where you can't move or look around to see the tower.
 */


public class KeepSceneAlive : MonoBehaviour
{
    public bool KeepSceneViewActive;

    void Start()
    {
        if (this.KeepSceneViewActive && Application.isEditor)
        {
            UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
        }
    }
}
