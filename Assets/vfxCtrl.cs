using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;
using UnityEngine;
using OscJack;

public class vfxCtrl : MonoBehaviour {
    GameObject obj;
    private OscServer _server;
    private float _dis;
    
    // Start is called before the first frame update
    void Start()
    {
        // _server = new OscServer(54415);
    }

    // Update is called once per frame
    void Update()
    {
        // _server.MessageDispatcher.AddCallback (
        //     "/Dis",
        //     (string address, OscDataHandle data) => {
        //         _dis = data.GetElementAsFloat(0);
        //     }
        // );
        // VisualEffect vfx = obj.GetComponent<VisualEffect>();
        // vfx.SetFloat("Intensity", _dis);
    }
}
