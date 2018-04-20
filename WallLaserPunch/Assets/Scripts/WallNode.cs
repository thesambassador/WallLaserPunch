﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum WallNodeState {
	Off,
	Punchable,
	Laser
}

public class WallNodeEvent : UnityEvent<WallNode> { };

public class WallNode : MonoBehaviour {

	public WallNodeState NodeState = WallNodeState.Off;
	public WallNodeTriggers NodeTriggers;

	private WallNodeEvent _onNodePunched;
	public WallNodeEvent OnNodePunched {
		get {
			if (_onNodePunched == null)
				_onNodePunched = new WallNodeEvent();
			return _onNodePunched;
		}
	}
	

	// Use this for initialization
	void Awake () {
		//NodeTriggers = GetComponentInChildren<WallNodeTriggers>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetPunchable(bool isLeft) {
		NodeTriggers.SetPunchable(isLeft);
		NodeState = WallNodeState.Punchable;
	}

	public void NodePunched() {
		if (NodeState == WallNodeState.Punchable) {
			OnNodePunched.Invoke(this);
			NodeState = WallNodeState.Off;
		}
	}

}
