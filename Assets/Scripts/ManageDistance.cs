using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManageDistance : MonoBehaviour
{

    public List<Transform> targets;
    bool newTarget = true;
    bool canChoose = true;
    int lastNumber = -1;

    [Range(0f, 1f)]
    public float LerpFraction;

    Vector3 target;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canChoose)
        {
            canChoose = false;
            if (newTarget)
            {
                StopCoroutine("MoveFunction");

                //int targetNumber = Random.Range(0, 4);
                canChoose = false;
                SelectTarget();

                if (target != null)
                {
                    StartCoroutine(MoveFunction(target));
                }
                else
                {
                    SelectTarget();
                }
            }
        }
        else
        {
            //Debug.Log("cant move yet");
        }

    }

    void SelectTarget()
    {
        int targetNumber = Random.Range(0, targets.Count);

        if(lastNumber >= 0)
        {
            while(targetNumber == lastNumber)
            {
                targetNumber = Random.Range(0, targets.Count);
            }

        }
        lastNumber = targetNumber;
        target = targets[targetNumber].position;
        canChoose = false;
        Debug.Log(targets[targetNumber].name);

    }

    IEnumerator MoveFunction(Vector3 target)
    {
        int protectCounter = 0;
        float timeSinceStarted = 0f;
        print("into MoveFunction");
        while (true)
        {
            protectCounter++;
            //print("into MoveFunction While");
            timeSinceStarted += Time.deltaTime;
            
            transform.position = Vector3.Lerp(transform.position, target, 0.4f * LerpFraction);

            // If the object has arrived, stop the coroutine
            if ((transform.position - target).magnitude < 0.001f)
            {
                newTarget = true;
                Invoke("SetCanChoose", .2f);
                yield break;
            }

            /*if(protectCounter > 50)
            {
                newTarget = true;
                canChoose = true;
                yield break;
            }*/

            // Otherwise, continue next frame
            newTarget = false;
            yield return null;
        }
    }

    void SetCanChoose()
    {
        canChoose = true;
    }
}