using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallNodeTriggers : MonoBehaviour {

	public string TagName;
	public UnityEvent OnTriggerTagDetected;

	private Renderer _renderer;

	public Color LeftHandColor;
	public Color RightHandColor;

	void Awake() {
		_renderer = GetComponent<MeshRenderer>();
	}

	public void OnTriggerEnter(Collider other) {
		if (other.CompareTag(TagName)) {
			OnTriggerTagDetected.Invoke();
		}
	}

	public void SetPunchable(bool isLeft){
		this.gameObject.SetActive(true);
		if (isLeft) {
			TagName = StringConstants.TAG_LEFTHAND;
			_renderer.material.color = LeftHandColor;
		}
		else {
			TagName = StringConstants.TAG_RIGHTHAND;
			_renderer.material.color = RightHandColor;
		}
	}

	public void Punched() {
		this.gameObject.SetActive(false);
	}
	
}
