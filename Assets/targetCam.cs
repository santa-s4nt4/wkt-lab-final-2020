using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;
using OscJack;
using UnityEngine;

public class targetCam : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject targetObject;
    public GameObject obj;
    public Transform target;
    private Vector3 offset;
    private OscServer _server;
    private Vector3 _pos;
    private float _high;
    private float _mid;
    private float _low;
    private float _dis;

    void Start () {
        _server = new OscServer(54414);
        Application.targetFrameRate = 30;
        //offset = GetComponent<Transform>().position - target.position;
    }

    private void OnDestroy () {
        _server.Dispose ();
    }

    // Update is called once per frame
    void Update () {
        _server.MessageDispatcher.AddCallback (
            "/High",
            (string address, OscDataHandle data) => {
                _high = data.GetElementAsFloat(0);
            }
        );
        _server.MessageDispatcher.AddCallback (
            "/Mid",
            (string address, OscDataHandle data) => {
                _mid = data.GetElementAsFloat(0);
            }
        );
        _server.MessageDispatcher.AddCallback (
            "/Low",
            (string address, OscDataHandle data) => {
                _low = data.GetElementAsFloat(0);
            }
        );
        _server.MessageDispatcher.AddCallback (
            "/Dis",
            (string address, OscDataHandle data) => {
                // _pos.x = data.GetElementAsFloat(0);
                // _pos.y = 0;
                // _pos.z = data.GetElementAsFloat(0);
                _dis = data.GetElementAsFloat(0);
            }
        );
        VisualEffect vfx = obj.GetComponent<VisualEffect>();
        vfx.SetFloat("Intensity", _dis);
        transform.RotateAround(targetObject.transform.position, new Vector3(_low, 1, 0), _high);
    }
}