using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PutCube : Soungs
{
    [SerializeField] KeyCode keySpace;
    public GameObject HandCube;
    public GameObject Triget;
    private BoxCollider handCubeCollider;
    public GameObject CloneCube;
    public GameObject PutCubeS;
    public GameObject Menu_Panel;
    public TMP_Text NowNumberCube;
    public float moveSpeed = 1f;
    float y;
    float x;
    float z;
    private bool Startgame = false;
    private Coroutine Coroutine_spawn;
    bool Wait = false;
    bool TheEnd = false;
    public int Number_cube = 0;

    void RandomP(int randomNumber)
    {
        switch (randomNumber)
        {
            case 1:
                y++; break;
            case 2:
                x++; break;
            case 3:
                z++; break;
            case 4:
                x--; break;
            case 5:
                z--; break;
        }

    }

    IEnumerator Spawn()
    {
        while (true)
        {
            Transform cubeTransform = CloneCube.transform;
            x = cubeTransform.position.x;
            y = cubeTransform.position.y;
            z = cubeTransform.position.z;
            RandomP(UnityEngine.Random.Range(0, 6));
            Vector3 cubeSize = new Vector3(x, y, z);
            if (!Physics.CheckBox(cubeSize, Vector3.zero))
            {
                PutCubeS.transform.position = cubeSize;
                yield return new WaitForSeconds(moveSpeed);
            }
            else
            {
                continue;
            }
        }
    }

    void CreateCube(Vector3 cubeSize)
    {
        if(Coroutine_spawn != null)
        {
            StopCoroutine(Coroutine_spawn);
            Coroutine_spawn = null;
        }
        Rigidbody rbHandCube = HandCube.GetComponent<Rigidbody>();
        GameObject newCube = Instantiate(CloneCube, cubeSize, Quaternion.identity);
        newCube.name = "Clone";
        Rigidbody rb = newCube.GetComponent<Rigidbody>();
        Destroy(rb);
        newCube.transform.parent = HandCube.transform;
        CloneCube = newCube;

        rbHandCube.isKinematic = true;
        rbHandCube.isKinematic = false;
        Number_cube++;
        NowNumberCube.text = Number_cube.ToString();
        Coroutine_spawn = StartCoroutine(Spawn());
        PlaySoungs(soungs[0]);
    }

    void Start()
    {
        NowNumberCube.text = Number_cube.ToString();
        handCubeCollider = HandCube.GetComponent<BoxCollider>();
        CloneCube = HandCube;
    }

    float NormalizeAngle(float angle)
    {
        if (angle > 180)
            angle -= 360;
        return angle;
    }

    void Update()
    {
        Vector3 rotation = handCubeCollider.transform.eulerAngles;

        rotation.x = NormalizeAngle(rotation.x);
        rotation.y = NormalizeAngle(rotation.y);
        rotation.z = NormalizeAngle(rotation.z);
        if (Mathf.Abs(rotation.x) > 1 || Mathf.Abs(rotation.y) > 1 || Mathf.Abs(rotation.z) > 1)
        {
            if (Coroutine_spawn != null)
            {
                StopCoroutine(Coroutine_spawn);
                TheEnd = true;
                Coroutine_spawn = null;
            }
        }
        if (!Menu_Panel.active)
        {
            if (Input.GetKeyUp(keySpace) && !Wait && !TheEnd)
            {
                Vector3 cubeSize;
                if (Startgame)
                {
                    cubeSize = new Vector3(x, y, z);
                    CreateCube(cubeSize);
                }
                else
                {
                    cubeSize = new Vector3(0.2f, 1.5f, -3.33f);
                    CreateCube(cubeSize);
                    Startgame = true;
                }
                StartCoroutine(StopForOneSecond());
            }
        }
    }

    private IEnumerator StopForOneSecond()
    {
        Wait = true;
        yield return new WaitForSeconds(1);
        Wait = false;
    }
}
