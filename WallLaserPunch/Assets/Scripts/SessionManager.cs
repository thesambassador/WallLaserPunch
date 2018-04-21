using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum SessionState {
	None,
	Game,
	Paused
}

public class SessionManager : SingletonBehavior<SessionManager> {
	public SessionState State = SessionState.None;
	public WallHelper Walls;

	public WallNodeManager[] WallNodeManagers;
	public LaserManager LaserManager;
	
	private int[] _randomWallIndexes = {0,1,2,3};

	[HideInInspector]
	public int NumPunchableNodesRemaining = 0;

	public int NodesPunched = 0;
	public float GameTime = 0;

	public AnimationCurve NumNodeDifficulty;

	private bool _lastNodeLeft = false;

	void Start(){
		
	}

	public void Initialize() {
		Walls = FindObjectOfType<WallHelper>();

		WallNodeManagers = new WallNodeManager[4];
		for (int i = 0; i < 4; i++) {
			WallNodeManagers[i] = Walls.Walls[i].GetComponent<WallNodeManager>();
		}

		_randomWallIndexes.Shuffle();
		LaserManager.Initialize();
	}

	//Selects a random wall to use using _randomWallIndexes
	//The value at _randomWallIndexes[3] is always the most recently used wall, so we only care about 0-2
	WallNodeManager GetRandomWallNodeManager() {

		int i = Random.Range(0, 3);

		int wnmIndex = _randomWallIndexes[i];
		_randomWallIndexes[i] = _randomWallIndexes[3];
		_randomWallIndexes[3] = wnmIndex;

		return WallNodeManagers[wnmIndex];
	}

	[Button]
	public void StartGame() {
		Initialize();

		NodesPunched = 0;
		GameTime = 0;
		State = SessionState.Game;

		ActivatePunchableNodeWave();
	}

	void ActivatePunchableNodeWave() {
		int numNodes = (int)NumNodeDifficulty.Evaluate(NodesPunched);
		ActivatePunchableNodeWave(numNodes);
	}

	void ActivatePunchableNodeWave(int numNodes) {
		WallNodeManager randomWall = GetRandomWallNodeManager();

		for (int i = 0; i < numNodes; i++) {
			WallNode node = randomWall.SetRandomWallNodePunchable(_lastNodeLeft);
			node.OnNodePunched.AddListener(NodePunched);
			_lastNodeLeft = !_lastNodeLeft;
		}

		NumPunchableNodesRemaining = numNodes;
	}

	void NodePunched(WallNode node) {
		node.OnNodePunched.RemoveListener(NodePunched);
		NodesPunched++;
		NumPunchableNodesRemaining--;
		if (NumPunchableNodesRemaining <= 0) {
			ActivatePunchableNodeWave();
		}
	}

	void Update() {
		if (State == SessionState.Game) {
			GameTime += Time.deltaTime;
		}
		
	}

	
}
