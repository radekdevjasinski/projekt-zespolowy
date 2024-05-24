using Cinemachine;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CameraFollowRoom : MonoBehaviour
{
    [Header("DungeonRoomsCamera")]
    private DungeonRoom activePlayerRoom;
    private DungeonGenerator dungeonGenerator;
    private GameObject player;
    public float smallRoomCameraSize = 3;
    public float bossRoomCameraSize = 8;
    public float mediumRoomCameraSize = 4;

    [Header("Cameras")]
  [SerializeField]  private GameObject mainCamera;
    [SerializeField] private CinemachineVirtualCamera bossRoomCamera;
    [SerializeField] private float transitionDuration;
    public bool transitionDone = false;

    [Header("SwipeAnimation")]
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;



    private void Start()
    {
        dungeonGenerator = GameObject.Find("Dungeon").GetComponent<DungeonGenerator>();
        player = GameObject.Find("Player");

        transform.position = new Vector3(transform.position.x, transform.position.y, -11);
        setCameraFollowPonit(this.gameObject.transform);
        //setCameraToBasicRoom();
    }


    private void setCameraToBasicRoom()
    {
        mainCamera.SetActive(true);
        bossRoomCamera.enabled = false;
    }
 

    //public void moveCameraToPosition(DungeonRoom newRoom)
    //{
    //    Vector3 newPos = newRoom.gameObject.transform.position;
    //    mainCamera.GetComponent<Camera>().orthographicSize = newRoom.gameObject.GetComponent<Room>().getRoomSize();
    //    newPos.z = -11;
    //    StartCoroutine(moveCamera(newPos));
    //    switch (newRoom.roomType)
    //    {
    //        case RoomType.BOSSROOM:
    //            setCameraToBossRoom(newRoom.visited);
    //            break;
    //        default:
    //            setCameraToBasicRoom();
    //            break;
    //    }
        
    //}


 
    //IEnumerator moveCamera(Vector3 destin)
    //{

    //    //Vector3 vel = Vector3.zero;
    //    //while (Vector3.Distance(transform.position, destin) > 0.01)
    //    //{
    //    //    Debug.Log("moving camera from "+ transform.position+" to "+ destin);
    //    //    transform.position = Vector3.SmoothDamp(transform.position, destin, ref velocity, smoothTime);
    //    //    yield return new WaitForEndOfFrame(); 
    //    //}
    //}
    IEnumerator DelayedCameraActivation(float delay)
    {
        Debug.Log("DelayedCameraActivation");
        transitionDone = true;
        transitionDuration = 5f;
        float t = 0;
        bossRoomCamera.enabled = true;
        bossRoomCamera.GetComponent<CinemachineStoryboard>().m_Alpha = 1;

        yield return new WaitForSeconds(delay);
  
            GameObject bossRoomPrefab = GameObject.FindGameObjectWithTag("LichBossRoom");
            if (bossRoomPrefab != null)
            {
                PolygonCollider2D roomCollider = bossRoomPrefab.GetComponentInChildren<PolygonCollider2D>();
                if (roomCollider != null)
                {
                    bossRoomCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = roomCollider;
                }
                else
                {
                    Debug.LogError("Nie znaleziono Collidera 2D w LichBossRoom.");
                }
            }
            else
            {
                Debug.LogError("Nie znaleziono prefaba LichBossRoom na scenie.");
            }

            yield return new WaitForSeconds(delay);
            while (t < transitionDuration)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Lerp(1, 0, t / transitionDuration);
                bossRoomCamera.GetComponent<CinemachineStoryboard>().m_Alpha = alpha;
                yield return null;
            }
        }

    public void setCameraFollowPonit(Transform followpoint)
    {
        Debug.Log("seting folor point to "+ followpoint.transform.name + " at positon: "+ followpoint.transform.position);
        bossRoomCamera.Follow=followpoint;
        
    }

    public void cameraFadeIn()
    {
        Debug.Log("cameraFadeIn") ;
        StartCoroutine(DelayedCameraActivation(0.25f));
    }

    public void disableConfirer()
    {
        
        this.bossRoomCamera.GetComponent<CinemachineConfiner2D>().enabled=false;
    }
    public void enableConfirer()
    {

        this.bossRoomCamera.GetComponent<CinemachineConfiner2D>().enabled = true;
    }

    internal void setCameraSize(float v)
    {
        Debug.Log("setting camera size: " + v);
        bossRoomCamera.m_Lens.OrthographicSize = v;
    }
}
