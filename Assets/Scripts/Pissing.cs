using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pissing : MonoBehaviour
{
    public float lastTime;
    public Transform pissTarget;
    public ParticleSystem pissParticles;
    StarterAssets.FirstPersonController fpsController;

    void Start()
    {
        lastTime = Time.time;
        fpsController = GetComponent<StarterAssets.FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastTime < 5f) //ma byc rando 30-60 sekund
        {
            Camera.main.GetComponent<Cinemachine.CinemachineBrain>().ActiveVirtualCamera.LookAt = null;
            fpsController.movementLocked = false;
            fpsController.mouseLocked = false;
        }
        else
        {
            print("pissing");
            fpsController.movementLocked = true;
            fpsController.mouseLocked = true;
            pissParticles.gameObject.SetActive(true);
            Camera.main.GetComponent<Cinemachine.CinemachineBrain>().ActiveVirtualCamera.LookAt = pissTarget;

            Invoke(nameof(WaitForPiss), 2f);
        }

    }

    void WaitForPiss()
    {
        //yield return new WaitForSeconds(2f);
        lastTime = Time.time;
        pissParticles.gameObject.SetActive(false);
        //pissParticles.Stop();
    }
}
