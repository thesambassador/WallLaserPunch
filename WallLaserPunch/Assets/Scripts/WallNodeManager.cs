using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class WallNodeManager : MonoBehaviour {

    WallInfo _info;

    public WallNode NodePrefab;
    public float NodeWidth = .25f;
    public float NodeHeight = .25f;

    public int NumNodesHorizontal = 0;
    public int NumNodesVertical = 0;

	public WallNode[,] WallNodes;

	// Use this for initialization
	void Awake () {
        _info = GetComponent<WallInfo>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnWallUpdated() {
        SpawnWallNodes();
    }

    void SpawnWallNodes() {

        NumNodesHorizontal = Mathf.FloorToInt(_info.Width / NodeWidth);
        NumNodesVertical = Mathf.FloorToInt(_info.Height / NodeHeight);

		WallNodes = new WallNode[NumNodesHorizontal, NumNodesVertical];

        float xRemainder = _info.Width - (NodeWidth * NumNodesHorizontal);
        float yRemainder = _info.Height - (NodeHeight * NumNodesVertical);

        float spacingX = xRemainder / (NumNodesHorizontal + 1);
        float spacingY = yRemainder / (NumNodesVertical + 1);

        //start at bottom left corner
        //transform.position is at the center of the wall
        float startX = -(_info.Width / 2);
        float startY = -(_info.Height / 2);

        startX += spacingX + (NodeWidth / 2);
        startY += spacingY + (NodeHeight / 2);

        for (int x = 0; x < NumNodesHorizontal; x++) {
			float curX = startX + (x * NodeWidth) + (x * spacingX);
            for (int y = 0; y < NumNodesVertical; y++) {
				float curY = startY + (y * NodeHeight) + (y * spacingY);
                WallNode newNode = Instantiate(NodePrefab);
                newNode.transform.rotation = this.transform.rotation;
                newNode.transform.parent = this.transform;
				newNode.transform.localPosition = new Vector3(curX, curY, 0);
				newNode.transform.localScale = new Vector3(NodeWidth, NodeHeight, NodeWidth);
				WallNodes[x, y] = newNode;
            }
            
        }

    }

	public WallNode GetRandomInactiveWallNode(){

		var query = from WallNode node in WallNodes
					where node.NodeState == WallNodeState.Off
					select node;

		return query.ElementAt(Random.Range(0, query.Count()));
	}

	public WallNode SetRandomWallNodePunchable(bool isLeft) {
		WallNode node = GetRandomInactiveWallNode();
		node.SetPunchable(isLeft);
		return node;
	}

	
}
