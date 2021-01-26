using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CameraConfiguration cameraConfigurationDefault;
    [SerializeField] CameraConfiguration cameraConfiguration;
    [SerializeField] CameraConfiguration targetCameraConfiguration;
    [SerializeField] Camera camera;

    public float speed = 10.0f;

    public static CameraController _instance;

    List<AView> activesView = new List<AView>();

    public void AddView(AView view)
    {
        activesView.Add(view);
    }
    public void RemoveView(AView view)
    {
        activesView.Remove(view);
    }

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
        /*camera.transform.position = cameraConfiguration.GetPosition();
        camera.transform.rotation = cameraConfiguration.GetRotation();

        camera.fieldOfView = cameraConfiguration.fieldOfView;*/

        SetCameraPosition(GetActivesViewConfiguration());

        //Smoothing();
    }

    public void SetCameraPosition(CameraConfiguration config)
    {
        camera.transform.position = config.GetPosition();
        camera.transform.rotation = config.GetRotation();
        camera.fieldOfView = config.fieldOfView;
    }


    public void Smoothing()
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

    public CameraConfiguration GetActivesViewConfiguration()
    {
        if (activesView.Count == 0)
            return cameraConfigurationDefault;
        CameraConfiguration res = new CameraConfiguration();
        float sumWeight = 0;

        for (int i = 0; i < activesView.Count; i++)
        {
            CameraConfiguration view = activesView[i].GetConfiguration();
            res.distance += (view.distance * activesView[i].Weight);
            res.yaw += (view.yaw * activesView[i].Weight);
            res.pitch += (view.pitch * activesView[i].Weight);
            res.roll += (view.roll * activesView[i].Weight);
            res.pivot += (view.pivot * activesView[i].Weight);
            res.fieldOfView += (view.fieldOfView * activesView[i].Weight);
            sumWeight += activesView[i].Weight;
        }
        if (sumWeight == 0)
            return cameraConfigurationDefault;

        res.distance /= sumWeight;
        res.yaw /= sumWeight;
        res.pitch /= sumWeight;
        res.roll /= sumWeight;
        res.pivot /= sumWeight;
        res.fieldOfView /= sumWeight;
        return res;
    }
}
