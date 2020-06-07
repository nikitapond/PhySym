using UnityEngine;
using UnityEditor;

public class NBodySystem : MonoBehaviour
{
    public static NBodySystem Instance;

    public GameObject GravityObjectPrefab;

    public float dt = 1;
    public float G = 1; //m^3 kg^-1 s^-2
    public float DistanceScale = 1; //How many metres 1 unit represents

    private void Awake()
    {
        Instance = this;
    }

    GravityObject[] Objects;
    private void Start()
    {
        Objects = GetComponentsInChildren<GravityObject>();
    }
    private void Update()
    {
        UpdateAllBodies();
    }

    public void UpdateAllBodies()
    {
        int n = Objects.Length;
        Debug.Log(n);
        Vector3[,] dirs = new Vector3[n, n];
        float[,] dists3 = new float[n, n];
        for(int i=0; i<n; i++)
        {
            for(int j=0; j<i; j++)
            {
                //i think this shouldn't happen, but just in case
                if (i == j)
                    continue;
                //We find the displacement from object i -> object j
                Vector3 disp_ij = Objects[j].transform.position - Objects[i].transform.position;
                dirs[i, j] = disp_ij;
                dirs[j, i] = -disp_ij;
                float mag = disp_ij.magnitude;
                float mag3 = mag* mag* mag;
                dists3[i, j] = mag3;
                dists3[j, i] = mag3;
            }
        }
        for(int i=0; i<n; i++)
        {
            Vector3 acc_i = Vector3.zero;
            for(int j=0; j<n; j++)
            {
                if (i == j)
                    continue;

                acc_i += G * Objects[j].Mass * dirs[i, j] / dists3[i, j];
            }
            Objects[i].SetTickAcceleration(acc_i, dt);


        }


    }


 
}