using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class QTESys : MonoBehaviour
{
    public GameObject DisplayBox; // 1 - z, 2 - x, 3 - c
    public GameObject PassBox;
    public int QTEGen;
    public int WaitingForKey;
    public int CorrectKey;
    public int CountingDown;

    private void Update()
    {
        if (WaitingForKey == 0)
        {
            QTEGen = Random.Range(1, 4);
            CountingDown = 1;
            StartCoroutine(CountDown());


            if (QTEGen == 1)
            {
                WaitingForKey = 1;
                //Debug.Log("[Z]");
            }

            if (QTEGen == 2)
            {
                WaitingForKey = 1;
                //Debug.Log("[X]");
            }

            if (QTEGen == 3)
            {
                WaitingForKey = 1;
                //Debug.Log("[C]");
            }
        }

        if (QTEGen == 1)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("ZKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }

        if (QTEGen == 2)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("XKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }

        if (QTEGen == 3)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("CKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }

        IEnumerator KeyPressing()
        {
            QTEGen = 4;
            if (CorrectKey == 1)
            {
                CountingDown = 2;
                //Debug.Log("ХОРОШ");
                yield return new WaitForSeconds(0.25f);
                CorrectKey = 0;
                yield return new WaitForSeconds(0.25f);
                WaitingForKey = 0;
                CountingDown = 1;
            }

            if (CorrectKey == 2)
            {
                CountingDown = 2;
                //Debug.Log("ЛОХ");
                yield return new WaitForSeconds(0.25f);
                CorrectKey = 0;
                yield return new WaitForSeconds(0.25f);
                WaitingForKey = 0;
                CountingDown = 1;
            }

            if (CorrectKey == 3)
            {
                CountingDown = 2;
                //Debug.Log("НУБИК");
                yield return new WaitForSeconds(0.25f);
                CorrectKey = 0;
                yield return new WaitForSeconds(0.25f);
                WaitingForKey = 0;
                CountingDown = 1;
            }
        }

        IEnumerator CountDown() 
        {
            yield return new WaitForSeconds(0.55f);
            if (CountingDown == 1)
            {
                QTEGen = 4;
                CountingDown = 2;
                //Debug.Log("НУБИК");
                yield return new WaitForSeconds(0.25f);
                CorrectKey = 0;
                yield return new WaitForSeconds(0.25f);
                WaitingForKey = 0;
                CountingDown = 1;
            }
        }
    }
}
