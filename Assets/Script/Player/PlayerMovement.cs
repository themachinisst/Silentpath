using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{

    public GameObject Player;
    private Vector3 MousePos;

    [Range(1, 9)]
    [Tooltip("Maximum distance from the player, from where the speed shall remain constant and speed should not increase on distance lesser than this.")]
    public float maxDistance;

    [Range(1, 5)]
    [Tooltip("Minimum distance from the player, from where the speed shall remain constant and speed should not decrease on distance lesser than this.")]
    public float minDistance;
    float distance;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine(Move());
        }

        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(Move());
        }


    }

    IEnumerator Move()
    {
        float elapsedTime = 0;
        float rotZ = 0;
        float duration = 5f;
        Vector3 rotate;
        Quaternion rotateQ;
        Quaternion playerRotateQ;

        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 start = Player.transform.localPosition;
        Vector3 end = new Vector3(MousePos.x, MousePos.y, 0);

        float distance = Vector3.Distance(start, end);
        if (distance>= maxDistance)
        {
            distance = 3;
        }else if (distance<= minDistance)
        {
            distance = 2;
        }
        duration = duration / distance;

        while (elapsedTime < duration)
        {
            if (Input.GetMouseButtonDown(0))
            {
                yield break;
            }
            Player.transform.localPosition = Vector3.Lerp(start, end, (elapsedTime / duration));

            rotate = MousePos - Player.transform.localPosition;
            rotZ = Mathf.Atan2(rotate.y, rotate.x) * Mathf.Rad2Deg;
            //Player.transform.rotation = Quaternion.Euler(0, 0, rotZ);
            rotateQ = Player.transform.rotation;
            playerRotateQ = Quaternion.Euler(0, 0, rotZ);
            Player.transform.rotation = Quaternion.Lerp(rotateQ, playerRotateQ, (elapsedTime / duration));

            elapsedTime += Time.deltaTime;

            yield return Player.transform.localPosition;

        }

        Player.gameObject.transform.localPosition = end;
    }
}
