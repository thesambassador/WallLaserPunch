using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LaserManager : MonoBehaviour {

	public LaserNodePair[,] FrontBackLasers;
	public LaserNodePair[,] LeftRightLasers;

	public GameObject WarnLaserPrefab;
	public GameObject ActiveLaserPrefab;

	public void Initialize() {
		InitializePair(SessionManager.Instance.WallNodeManagers[0], SessionManager.Instance.WallNodeManagers[1], ref FrontBackLasers);
		InitializePair(SessionManager.Instance.WallNodeManagers[2], SessionManager.Instance.WallNodeManagers[3], ref LeftRightLasers);
	}

	void InitializePair(WallNodeManager wall1, WallNodeManager wall2, ref LaserNodePair[,] pairArray) {
		int xLen = wall1.WallNodes.GetLength(0);
		int yLen = wall1.WallNodes.GetLength(1);

		pairArray = new LaserNodePair[xLen, yLen];

		for (int x = 0; x < xLen; x++) {
			for (int y = 0; y < yLen; y++) {
				pairArray[x, y] = new LaserNodePair(wall1.WallNodes[x, y], wall2.WallNodes[xLen - x - 1, y]);
				CreateLaser(pairArray[x, y]);
			}
		}
	}

	void CreateLaser(LaserNodePair pair){
		LaserNode lasernode = pair.Node1.Laser;

		float dist = Vector3.Distance(pair.Node1.transform.position, pair.Node2.transform.position);

		Quaternion rot = lasernode.transform.rotation;
		Vector3 pos = lasernode.transform.position;
		Vector3 scale = new Vector3(1, 1, dist * (1/SessionManager.Instance.WallNodeManagers[0].NodeWidth));

		lasernode.WarnLaser = Instantiate(WarnLaserPrefab);
		lasernode.WarnLaser.transform.rotation = rot;
		lasernode.WarnLaser.transform.position = pos;
		lasernode.WarnLaser.transform.parent = lasernode.transform.parent;
		lasernode.WarnLaser.transform.localScale = scale;
		lasernode.WarnLaser.SetActive(false);

		lasernode.ActiveLaser = Instantiate(ActiveLaserPrefab);
		lasernode.ActiveLaser.transform.rotation = rot;
		lasernode.ActiveLaser.transform.position = pos;
		lasernode.ActiveLaser.transform.parent = lasernode.transform.parent;
		lasernode.ActiveLaser.transform.localScale = scale;
		lasernode.ActiveLaser.SetActive(false);

	}

	public int testX;
	public int testY;
	public bool fb;

	[Button]
	void TestPair() {
		if (fb) {
			FrontBackLasers[testX, testY].Activate();
		}
		else {
			LeftRightLasers[testX, testY].Activate();
		}
	}
	
}
