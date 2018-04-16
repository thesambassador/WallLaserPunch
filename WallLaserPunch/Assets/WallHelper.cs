using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHelper : MonoBehaviour {

    public GameObject WallPrefab;
    public float WallHeight;

    [HideInInspector]
    public GameObject FrontWall;

    [HideInInspector]
    public GameObject BackWall;

    [HideInInspector]
    public GameObject RightWall;

    [HideInInspector]
    public GameObject LeftWall;

    private PlayAreaHelper _playHelper;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateWalls()
    {
        _playHelper = GetComponent<PlayAreaHelper>();

        Vector3[] positions = { _playHelper.FrontCenter, _playHelper.BackCenter, _playHelper.RightCenter, _playHelper.LeftCenter };
        Vector3[] lookRotations = { Vector3.back, Vector3.forward, Vector3.left, Vector3.right };
       

        for (int i = 0; i < 4; i++)
        {
            GameObject newWall = Instantiate(WallPrefab);
            Vector3 wallCenter = positions[i];
            wallCenter.y = WallHeight / 2;
            newWall.transform.position = wallCenter;
            newWall.transform.rotation = Quaternion.LookRotation(lookRotations[i]);

            if (i < 2)
            {
                newWall.transform.localScale = new Vector3(_playHelper.PlaySpaceRect.width, WallHeight, 1);
            }
            else
            {
                newWall.transform.localScale = new Vector3(_playHelper.PlaySpaceRect.height, WallHeight, 1);
            }
        }

       
        
    }
}
