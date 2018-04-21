using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserNodePair {
	public WallNode Node1;
	public WallNode Node2;

	public LaserNodePair(WallNode n1, WallNode n2) {
		Node1 = n1;
		Node2 = n2;
	}

	public bool IsOff() {
		return Node1.NodeState == WallNodeState.Off && Node2.NodeState == WallNodeState.Off;
	}

	public void Activate() {
		Node1.SetLaser();
		Node2.SetLaser();

	}

}
