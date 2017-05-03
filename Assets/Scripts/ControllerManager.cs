using UnityEngine;
using System.Collections;

public class ControllerManager : MonoBehaviour
{

    SteamVR_TrackedObject tracked;
    SteamVR_Controller.Device device;
    GameObject manager;
    PauseMenu pauser;
    SwordTipCollision swordTip;
    // Use this for initialization
    void Start()
    {
        tracked = this.GetComponent<SteamVR_TrackedObject>();
        manager = KeyComponents.gameManager;
        pauser = KeyComponents.pauseMenu;
        swordTip = GameObject.Find("sword_button").GetComponent<SwordTipCollision>();
    }

    bool checkIsAllowednextExercise()
    {
        if(swordTip.getExerciseName() != "Gauntlet")
        {
            return false;
        }

        if(swordTip.getExerciseNumber() +1 >= swordTip.getMaxExercises())
        {
            return false;
        }
        return true;
    }

    bool checkIsAllowedMenuButton()
    {
        if(swordTip.getExerciseName() == "MainMenu")
        {
            return false;
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)tracked.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            //device.TriggerHapticPulse(3999);
            //TriggerPulse(0.2f,1.0f);
            pauser.setAllowMenuButton(checkIsAllowedMenuButton());
            pauser.setAllowNextExerciseButton(checkIsAllowednextExercise());
            pauser.managePause((int)device.GetAxis().x);
        }
    }

    public void TriggerPulse(float length, float strength)
    {
        //device.TriggerHapticPulse(length);
        Debug.Log("pulse happened");
        StartCoroutine(LongVibration(1, 0.5f));

    }

    IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            device.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            yield return null;
        }
    }
}
