using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CameraConfiguration cameraConfiguration;
    [SerializeField] Camera camera;

    private static CameraController _instance;

    void Awake()
    {

        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            //Rest of your Awake code
            //Supprimer les commentaires quand fini de toucher au Awake

        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        camera.transform.position = cameraConfiguration.GetPosition();
        camera.transform.rotation = cameraConfiguration.GetRotation();

        camera.fieldOfView = cameraConfiguration.fieldOfView;
    }

    private void OnDrawGizmos()
    {
        cameraConfiguration.DrawGizmos(Color.red);
    }
}
