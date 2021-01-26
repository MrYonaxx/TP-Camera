using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CameraConfiguration cameraConfiguration;
    [SerializeField] CameraConfiguration targetCameraConfiguration;
    [SerializeField] Camera camera;

    public float speed = 10.0f;

    public static CameraController _instance;

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


        Smooting();
    }

    public void Smooting()
    {
        if (speed * Time.deltaTime < 1)
            cameraConfiguration.pivot = cameraConfiguration.pivot + (targetCameraConfiguration.pivot - cameraConfiguration.pivot) * speed * Time.deltaTime;
        else
            cameraConfiguration.pivot = targetCameraConfiguration.pivot;
    }

    private void OnDrawGizmos()
    {
        cameraConfiguration.DrawGizmos(Color.red);
    }
}
