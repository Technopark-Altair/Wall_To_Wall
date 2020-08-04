using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    enum Difficulty { easy, normal, hard }

    [SerializeField]
    Text countdownText;
    [SerializeField]
    int minTextSize = 25;
    [SerializeField]
    int maxTextSize = 70;

    [SerializeField]
    Transform Carriage1;
    [SerializeField]
    Transform Carriage2;

    [SerializeField]
    private Transform EndTrace;

    [SerializeField]
    List<GameObject> EasyWalls;
    [SerializeField]
    List<GameObject> NormalWalls;
    [SerializeField]
    List<GameObject> HardWalls;

    public bool Touched_Wall = false;

    GameObject present_wall;
    bool end_printed = false;
    Difficulty difficulty = Difficulty.easy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Carriage1.transform.localPosition.z < -0.1f && present_wall == null)
        {
            Carriage1.position += Carriage1.forward * 0.1f;
            Carriage2.position += Carriage1.forward * 0.1f;
        }
        if (Carriage1.transform.localPosition.z > -0.1f && Carriage1.transform.localPosition.z < 0 && present_wall == null)
        {
            Carriage1.localPosition = Vector3.zero;
            Carriage2.localPosition = Vector3.zero;

            if (!Touched_Wall) {
                StartCoroutine(CountDown());
            }
        }

        if (Touched_Wall && !end_printed)
        {
            end_printed = true;
            StartCoroutine(EndGame());
        }
    }

    public void WallPassed()
    {
        if (!end_printed) {
            StartCoroutine(WallPassedCorutine());
        }
    }

    IEnumerator CountDown()
    {
        if (EasyWalls.Count == 0 && difficulty == Difficulty.easy)
        {
            difficulty = Difficulty.normal;
        }
        else if (NormalWalls.Count == 0 && difficulty == Difficulty.normal)
        {
            difficulty = Difficulty.hard;
        }
        else if (HardWalls.Count == 0 && difficulty == Difficulty.hard)
        {
            countdownText.text = "Вы победили!";

            for (int size = minTextSize; size <= maxTextSize; size++)
            {
                countdownText.fontSize = size;
                yield return new WaitForSeconds(0.5f / (maxTextSize - minTextSize));
            }
            yield return new WaitForSeconds(0.5f);

            end_printed = true;
            yield break;
        }

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();

            for (int size = minTextSize; size <= maxTextSize; size++)
            {
                countdownText.fontSize = size;
                yield return new WaitForSeconds(0.5f / (maxTextSize - minTextSize));
            }
            yield return new WaitForSeconds(0.5f);
        }

        countdownText.text = "Поехали!";
        for (int size = minTextSize; size <= maxTextSize; size++)
        {
            countdownText.fontSize = size;
            yield return new WaitForSeconds(0.5f / (maxTextSize - minTextSize));
        }

        int ind;
        switch (difficulty)
        {
            case Difficulty.easy:
                ind = Random.Range(0, EasyWalls.Count);
                present_wall = Instantiate(EasyWalls[ind], transform.position, EasyWalls[ind].transform.rotation);
                EasyWalls.RemoveAt(ind);
                present_wall.GetComponent<movement>().Speed = 3;
                break;
            case Difficulty.normal:
                ind = Random.Range(0, NormalWalls.Count);
                present_wall = Instantiate(NormalWalls[ind], transform.position, NormalWalls[ind].transform.rotation);
                NormalWalls.RemoveAt(ind);
                present_wall.GetComponent<movement>().Speed = 4;
                break;
            case Difficulty.hard:
                ind = Random.Range(0, HardWalls.Count);
                present_wall = Instantiate(HardWalls[ind], transform.position, HardWalls[ind].transform.rotation);
                HardWalls.RemoveAt(ind);
                present_wall.GetComponent<movement>().Speed = 5;
                break;
        } 

        present_wall.GetComponent<movement>().Carriage1 = Carriage1;
        present_wall.GetComponent<movement>().Carriage2 = Carriage2;
        present_wall.GetComponent<movement>().Z_End = EndTrace.position.z;
        present_wall.GetComponent<movement>().spawner = this;
        present_wall.GetComponent<Colliding>().spawner = this;

        yield return new WaitForSeconds(2);

        countdownText.text = "";
    }

    IEnumerator WallPassedCorutine()
    {
        countdownText.text = "Есть!";
        for (int size = minTextSize; size <= maxTextSize + 10; size++)
        {
            countdownText.fontSize = size;
            yield return new WaitForSeconds(0.5f / (maxTextSize + 10 - minTextSize));
        }
        yield return new WaitForSeconds(2);
        countdownText.text = "";
    }

    IEnumerator EndGame()
    {
        countdownText.text = "Мимо!";
        for (int size = minTextSize; size <= maxTextSize + 10; size++)
        {
            countdownText.fontSize = size;
            yield return new WaitForSeconds(0.5f / (maxTextSize + 10 - minTextSize));
        }
    }
}
