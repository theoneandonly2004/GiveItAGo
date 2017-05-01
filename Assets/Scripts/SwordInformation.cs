using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordInformation {
    List<Vector3> swordPositions;
    List<bool> isSwordRaised;

    public SwordInformation()
    {
        swordPositions = new List<Vector3>();
        isSwordRaised = new List<bool>();
    }

    public void addSwordPosition(Vector3 swordPos)
    {
        swordPositions.Add(swordPos);
    }

    public void addSwordState(bool isRaised)
    {
        isSwordRaised.Add(isRaised);
    }

    public List<bool> getIsSwordRaisedList()
    {
        return isSwordRaised;
    }

    public List<Vector3> getSwordPositionList()
    {
        return swordPositions;
    }

    public Vector3 getPositionAtTime(int time)
    {
        return swordPositions[time];
    }

    public bool getIsRaisedAtTime(int time)
    {
        return isSwordRaised[time];
    }
}


