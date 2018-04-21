using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayAreaHelper : SingletonBehavior<PlayAreaHelper> {

    public SteamVR_PlayArea PlayArea;
    public float PlaySpaceMargin = 0.15f;

    public Rect PlaySpaceRect;

    public UnityEvent OnPlayAreaCalculated;


    #region Edge Accessors
    public Vector3 FrontCenter
    {
        get
        {
            return new Vector3(0, 0, PlaySpaceRect.height / 2);
        }
    }

    public Vector3 BackCenter
    {
        get
        {
            return new Vector3(0, 0, -PlaySpaceRect.height / 2);
        }
    }

    public Vector3 RightCenter
    {
        get
        {
            return new Vector3(PlaySpaceRect.width / 2, 0, 0);
        }
    }

    public Vector3 LeftCenter
    {
        get
        {
            return new Vector3(-PlaySpaceRect.width / 2, 0, 0);
        }
    }

    public Vector3 CornerFrontLeft
    {
        get
        {
            return new Vector3(-PlaySpaceRect.width / 2, 0, PlaySpaceRect.height / 2);
        }
    }

    public Vector3 CornerFrontRight
    {
        get
        {
            return new Vector3(PlaySpaceRect.width / 2, 0, PlaySpaceRect.height / 2);
        }
    }

    public Vector3 CornerBackLeft
    {
        get
        {
            return new Vector3(-PlaySpaceRect.width / 2, 0, -PlaySpaceRect.height / 2);
        }
    }

    public Vector3 CornerBackRight
    {
        get
        {
            return new Vector3(PlaySpaceRect.width / 2, 0, -PlaySpaceRect.height / 2);
        }
    }
    #endregion

    // Use this for initialization
	void Start () {
        CalculatePlayAreaRect();
        //ShowDebugSpheres();
	}

    void CalculatePlayAreaRect()
    {
        float x = 0;
        float y = 0;

        float width = Vector3.Distance(PlayArea.vertices[4], PlayArea.vertices[5]);
        float height = Vector3.Distance(PlayArea.vertices[5], PlayArea.vertices[6]);

        PlaySpaceRect = new Rect(x,y,width - (2 * PlaySpaceMargin), height - (2 * PlaySpaceMargin));
        if (OnPlayAreaCalculated != null)
        {
            OnPlayAreaCalculated.Invoke();
        }
    }

    void ShowDebugSpheres()
    {
        Color[] colors = { Color.blue, Color.red, Color.green, Color.yellow };
        //Vector3[] positions = {FrontCenter, RightCenter, BackCenter, LeftCenter};
        Vector3[] positions = { CornerFrontLeft, CornerFrontRight, CornerBackRight, CornerBackLeft };

        for (int i = 0; i < 4; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.GetComponent<MeshRenderer>().material.color = colors[i];
            sphere.transform.position = positions[i];
        }
    }

	// Update is called once per frame
	void Update () {
      
	}
}
