using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class LaserNode : MonoBehaviour {

	public Color[] WarningColors;
	public Color FinalColor;

	public float BeepDarkeningAmount = .8f;
	public bool Activated = false;

	private Renderer _renderer;
	private Color _curColor;
	private float _curEmis = .5f;

	public GameObject WarnLaser;
	public GameObject ActiveLaser;

	public float FullEmisValue = 2;
	public float NormEmisValue = .5f;

	// Use this for initialization
	void Awake () {
		_renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		//print(_renderer.material.GetColor("_EmissionColor"));


	}

	
	public void StartLaser(float warningBlipTime = 1, float activeTime = 2, float cooldownTime = 1, UnityAction OnLaserDone = null) {
		this.gameObject.SetActive(true);
		Activated = true;
		StartCoroutine(LaserCoroutine(warningBlipTime, activeTime, cooldownTime, OnLaserDone));
		ActivateLaser(true, false);
	}

	IEnumerator LaserCoroutine(float warningBlipTime = 1, float activeTime = 2, float cooldownTime = 1, UnityAction OnLaserDone = null) {
		int warningIndex = 0;

		float curTime = warningBlipTime;
		//warming up
		while (warningIndex < WarningColors.Length) {
			float currentEmis = FullEmisValue;
			SetEmisColor(WarningColors[warningIndex], currentEmis);
			PlayBeep();

			while (curTime > 0) {
				yield return null;
				curTime -= Time.deltaTime;

				currentEmis = Mathf.Lerp(NormEmisValue, FullEmisValue, curTime / warningBlipTime);
				SetEmisColor(WarningColors[warningIndex], currentEmis);
			}
			curTime = warningBlipTime;
			warningIndex++;
		}

		//active
		SetEmisColor(FinalColor, FullEmisValue);
		ActivateLaser(false, true);
		yield return new WaitForSeconds(activeTime);

		//cooldown
		SetEmisColor(FinalColor, NormEmisValue);
		ActivateLaser(false, false);

		curTime = cooldownTime;
		while (curTime > 0) {
			yield return null;
			curTime -= Time.deltaTime;

			Color col = FinalColor;
			col.a = curTime / cooldownTime;

			SetEmisColor(col, (curTime / cooldownTime) * NormEmisValue);
		}

		if (OnLaserDone != null) {
			OnLaserDone.Invoke();
		}

		this.gameObject.SetActive(false);
	}

	Color SetEmisColor(Color newEmisColor, float intensity) {
		Color col = newEmisColor * Mathf.LinearToGammaSpace(intensity);
		_renderer.material.color = newEmisColor;
		_renderer.material.SetColor("_EmissionColor", col);
		return col;
	}

	public void ActivateLaser(bool warnOn, bool activeOn) {
		if (ActiveLaser != null) {
			WarnLaser.SetActive(warnOn);
			ActiveLaser.SetActive(activeOn);
		}
		Activated = activeOn;
	}

	public void PlayBeep() {

	}
}
