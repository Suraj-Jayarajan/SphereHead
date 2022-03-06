using System.Collections.Generic;
using UnityEngine;

public class ColorCubesController : MonoBehaviour {

    public GameObject[] game_Objects;
    public bool disableFollowPath = true;
    public bool disablePathLooping = true;


    private Object[] crystalMaterials;
    private int crystalCount;
    private int noOfPairedCrystals = 0;
    private string firstOfPairName = "", secondOfPairName = "", primaryMatName = "CrystalWhite";
    private int firstOfPairIndex = -1, secondOfPairIndex = -1;
    private int[] OrderOfMaterials = { 0, 1, 2, 3, 0, 1, 2, 3};
    private bool allPaired = false;
    private List<string> listOfPairedCrystals = new List<string>();
    

    void Start()
    {
        

        crystalMaterials = Resources.LoadAll<Material>("MatchCubeColorMaterials");
        crystalCount = transform.childCount;
        secondOfPairName = firstOfPairName = primaryMatName;

        OrderOfMaterials = ShuffleArray(OrderOfMaterials, OrderOfMaterials.Length);
        OrderOfMaterials = FilterArray(OrderOfMaterials);
        for (int i = 0; i < OrderOfMaterials.Length; i++)
        {

            CrystalController cc = transform.GetChild(i).GetComponent<CrystalController>();
            cc.SetSecondaryMaterial(crystalMaterials[OrderOfMaterials[i]] as Material, i);
        }

        if (disableFollowPath)
        {
            TriggerSwitchEvent(false);
        }
    }

   
    void LateUpdate()
    {
        
    }

    int[] ShuffleArray(int[] arr, int noOfShuffle)
    {
        for(int k = 0; k < noOfShuffle; k++)
        {
            int i = Random.Range(0, arr.Length);
            int j = Random.Range(0, arr.Length);

            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
        return arr;
    }

    int[] FilterArray(int[] arr)
    {
        List<int> newList = new List<int>();
        for (int i = 0; i< arr.Length; i++)
        {
            if (arr[i] < crystalCount / 2)
            {
                newList.Add(arr[i]);
            }
        }

        return newList.ToArray();
    } 
    
    public void PairHandler(int crystalIndex , string matName)
    {
        if(firstOfPairIndex== -1)
        {
            firstOfPairIndex = crystalIndex;
        }
        else
        {
            secondOfPairIndex = crystalIndex;
        }

        if(firstOfPairName == primaryMatName)
        {
            firstOfPairName = matName;
        }
        else
        {
            secondOfPairName = matName;
        }


        if (firstOfPairIndex >= 0 && secondOfPairIndex >= 0)
        {
            if (firstOfPairName != primaryMatName && secondOfPairName != primaryMatName)
            {
                if (firstOfPairIndex != secondOfPairIndex)
                {
                    if (firstOfPairName == secondOfPairName && !listOfPairedCrystals.Contains(firstOfPairName))
                    {
                        noOfPairedCrystals++;
                        transform.GetChild(firstOfPairIndex).GetComponent<CrystalController>().MatchFound();
                        transform.GetChild(secondOfPairIndex).GetComponent<CrystalController>().MatchFound();

                        listOfPairedCrystals.Add(firstOfPairName);
                    }
                    else
                    {
                        transform.GetChild(firstOfPairIndex).GetComponent<CrystalController>().ToggleBackToPrimary();
                        firstOfPairIndex = secondOfPairIndex;
                        firstOfPairName = secondOfPairName;
                        secondOfPairIndex = -1;
                        secondOfPairName = primaryMatName;
                    }
                }//the else part to be noted
            }
        }

        if (!allPaired)
        {
            if (noOfPairedCrystals == crystalCount / 2)
            {                
                TriggerSwitchEvent(true);

                allPaired = true;
            }
        }
    }

    
    void TriggerSwitchEvent(bool isSwitchPressed)
    {
        foreach (GameObject g in game_Objects)
        {
            FollowPath fp = g.GetComponent<FollowPath>();

            fp.switchPressed = isSwitchPressed;
            if (disablePathLooping)
            {
                fp.loopPath = false;
            }

        }
    }
}
