using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowRays : MonoBehaviour
{
    [SerializeField] private GameObject godRayObject;

    [Header("Move Points")]
    [SerializeField] private Transform startTransfrom;
    [SerializeField] private Transform endTransfrom;
    private float ratio;

    private void Start()
    {
        godRayObject.transform.position = startTransfrom.position;
        godRayObject.transform.rotation = startTransfrom.rotation;
    }
    private void Update()
    {
        ratio = SessionTimer.Instance.GetTimeRatio();

        godRayObject.transform.position = Vector3.Lerp(startTransfrom.position, endTransfrom.position, ratio);
        godRayObject.transform.rotation = Quaternion.Lerp(startTransfrom.rotation, endTransfrom.transform.rotation, ratio);
    }
}
