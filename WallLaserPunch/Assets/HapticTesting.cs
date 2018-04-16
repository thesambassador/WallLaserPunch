using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticTesting : MonoBehaviour {

    SteamVR_TrackedObject _trackedObject;

    public AnimationCurve HapticCurve;

    public Coroutine CurrentHapticRoutine;

    void Awake()
    {
        _trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CurrentHapticRoutine != null)
            {
                StopCoroutine(CurrentHapticRoutine);
            }

            CurrentHapticRoutine = StartCoroutine(DoTest());
        }

	}

    IEnumerator DoTest()
    {
        float t = 0;

        while (t < 1)
        {
            int amount = Mathf.Clamp((int)(HapticCurve.Evaluate(t) * 4999.0), 0, 4999);
            DoPulse(amount);
            yield return null;
            t += Time.deltaTime;
        }


    }

    void DoPulse(int amount)
    {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)_trackedObject.index);
        if(device != null){
            device.TriggerHapticPulse((ushort)amount);
        }
    }
}
