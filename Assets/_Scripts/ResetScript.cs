using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour

{
    public GameObject HammerPrefab;
    //cos nie dziala naprawic
    //private Vector3 HammerPosition;
    private GameObject currentHammer;
    public GameObject BowPrefab;
    private Vector3 BowPosition;
    private GameObject currentBow;
    public GameObject PistolPrefab;
    private Vector3 PistolPosition;
    private GameObject currentPistol;
    public GameObject Bow2Prefab;
    private Vector3 Bow2Position;
    private GameObject currentBow2;

    private void Start()
    {

       


        GameObject Bow = GameObject.FindGameObjectWithTag("Bow");
        if (Bow != null)
        {
            BowPosition = Bow.transform.position;
        }


        GameObject Bow2 = GameObject.FindGameObjectWithTag("Bow2");
        if (Bow2 != null)
        {
            Bow2Position = Bow2.transform.position;
        }


        GameObject Pistol = GameObject.FindGameObjectWithTag("Pistol");
        if (Pistol != null)
        {
            PistolPosition = Pistol.transform.position;
        }
        GameObject Hammer = GameObject.FindGameObjectWithTag("Hammers");
        if (Bow != null)
        {
            BowPosition = Bow.transform.position;
        }
    }

    public void ResetBowAndHammerAndPistol()
    {
        //Hammer u¿ywa BowPosition gdy¿ respi siê pod ziemia

        if (currentHammer != null)
        {
            Destroy(currentHammer);
        }

        currentHammer = Instantiate(HammerPrefab, BowPosition, Quaternion.identity);


        if (currentBow != null)
        {
            Destroy(currentBow);
        }

        currentBow = Instantiate(BowPrefab, BowPosition, Quaternion.identity);

        if (currentBow2 != null)
        {
            Destroy(currentBow2);
        }

        currentBow2 = Instantiate(Bow2Prefab, Bow2Position, Quaternion.identity);

        if (currentPistol != null)
        {
            Destroy(currentPistol);
        }
        currentPistol = Instantiate(PistolPrefab, PistolPosition, Quaternion.identity);
    }
}
