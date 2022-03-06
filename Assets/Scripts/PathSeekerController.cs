using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSeekerController : MonoBehaviour {
    Transform[] t;
    GameObject enemy, trigger;
    List<PathSeekerTrigger> pstList = new List<PathSeekerTrigger>();
    SeekPath sp;
    bool PathSeekerEnemyIsOnMove = false;

    // Use this for initialization
    void Start()
    {
        t = GetComponentsInChildren<Transform>();
        foreach (Transform tr in t)
        {
            if (tr.gameObject.name == "PathSeekingEnemy")
            {
                enemy = tr.gameObject;
                sp = enemy.GetComponent<SeekPath>();
            }
            if (tr.gameObject.tag == "PathSeekerTrigger")
            {
                trigger = tr.gameObject;
                pstList.Add(trigger.GetComponent<PathSeekerTrigger>());
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PathSeekerTrigger pst in pstList)
        {
            if (pst.triggerPressed && !PathSeekerEnemyIsOnMove)
            {
                sp.targetLocked = true;
                PathSeekerEnemyIsOnMove = true;
            }
        }
       
    }
}
