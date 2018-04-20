using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHelper : MonoBehaviour {

    public GameObject WallPrefab;
    public float WallHeight;

    public WallInfo[] Walls;

    [HideInInspector]
    public WallInfo FrontWall {
        get { return Walls[0]; }
    }

    [HideInInspector]
    public WallInfo BackWall {
        get { return Walls[1]; }
    }

    [HideInInspector]
    public WallInfo LeftWall {
        get { return Walls[2]; }
    }

    [HideInInspector]
    public WallInfo RightWall {
        get { return Walls[3]; }
    }


    private PlayAreaHelper _playHelper;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateWalls() {
        _playHelper = GetComponent<PlayAreaHelper>();
        Walls = new WallInfo[4];

        Vector3[] positions = { _playHelper.FrontCenter, _playHelper.BackCenter, _playHelper.RightCenter, _playHelper.LeftCenter };
        Vector3[] lookRotations = { Vector3.back, Vector3.forward, Vector3.left, Vector3.right };
        WallDir[] dirs = {WallDir.Front, WallDir.Back, WallDir.Right, WallDir.Left};
       
        for (int i = 0; i < 4; i++)
        {
            GameObject newWall = Instantiate(WallPrefab);

            WallInfo info = newWall.GetComponent<WallInfo>();
            if (info != null)
            {
                Vector3 wallCenter = positions[i];
                wallCenter.y = WallHeight / 2;
                newWall.transform.rotation = Quaternion.LookRotation(lookRotations[i]);
                newWall.transform.parent = this.transform;
                float width = _playHelper.PlaySpaceRect.width;
                if (i >= 2)
                {
                    width = _playHelper.PlaySpaceRect.height;
                }

                info.SetWallProperties(wallCenter, width, WallHeight, dirs[i]);
                Walls[i] = info;
            }

        }
    }
}
