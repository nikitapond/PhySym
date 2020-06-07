using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Wave2D : MonoBehaviour
{

    public static int GridSize = 128;

	public bool ShouldCheckForBarriers;
	public bool FixedBoundry;

    private MeshFilter MeshFilter;

    private float[,] Psi_xy;

	private FloatArray2D theta_t, theta_nm1;
	private bool[,] Barriers;
	public int test;
	public float Freq = 1;


    private void Start()
    {
		if (ShouldCheckForBarriers)
		{
			CheckForBarriers();
		}
		MeshFilter = GetComponent<MeshFilter>();

        //Fill with 0s
        Psi_xy = new float[GridSize, GridSize];
		float[,] phi_nm1 = new float[GridSize, GridSize];
		int midX = GridSize / 2;
		int midY = GridSize-20;
		for(int x=0; x<GridSize; x++)
		{
			for (int y = 0; y < GridSize; y++)
			{
				float exp = (x - midX) * (x - midX) + (y - midY) * (y - midY);
				float val = 256 * Mathf.Exp(-exp / 100);
				Psi_xy[x, y] = val;
				float val2 = val * Mathf.Exp(-0.9f);
				phi_nm1[x, y] = val;

			}
		}
		theta_t = new FloatArray2D(Psi_xy);
		theta_nm1 = new FloatArray2D(phi_nm1);
		CreateMesh();

	}


	private void CheckForBarriers()
	{
		Barriers = new bool[GridSize, GridSize];
		for (int x = 0; x < GridSize; x++)
		{
			for (int y = 0; y < GridSize; y++)
			{
				RaycastHit hit;
				if(Physics.Raycast(new Vector3(x, 50, y), Vector3.down, out hit, 64))
				{
					if (hit.transform.gameObject.CompareTag("Barrier"))
					{
						Barriers[x, y] = true;
					}
				}
			}
		}


	}
	private FloatArray2D UpdateWave(FloatArray2D theta_n, FloatArray2D theta_nm1, float r)
	{

		FloatArray2D del_sqr = FloatArray2D.Roll(theta_n, 1, 0) + FloatArray2D.Roll(theta_n, -1, 0) +
							   FloatArray2D.Roll(theta_n, 1, 1) + FloatArray2D.Roll(theta_n, -1, 1) -
							   theta_n * 4;
		FloatArray2D theta_np1 = theta_n * 2 - theta_nm1 + del_sqr * (r * r);
		return theta_np1;


	}

	private void Update()
	{

		FloatArray2D theta_n = UpdateWave(theta_t, theta_nm1, 0.2f);
		theta_nm1 = theta_t;
		theta_t = theta_n;

		if (FixedBoundry)
		{

			for(int i=0; i<GridSize; i++)
			{
				theta_t[i,0] = 0;
				theta_t[i,-1] = 0;
				theta_t[0, i] = 0;
				theta_t[-1, i] = 0;
			}
		}
		if(Barriers != null)
		{
			for (int x = 0; x < GridSize; x++)
			{
				for (int y = 0; y < GridSize; y++)
				{
					if (Barriers[x, y])
						theta_t[x, y] = 0;
				}
			}
		}

		Psi_xy = theta_n.Vals;


		//Psi_xy[10, 10] = 50*test;
		CreateMesh();
	}
	/// <summary>
	/// Remaps the value of the psi_xy value such that it fits within 0 and 1
	/// </summary>
	/// <param name="psi_xy"></param>
	/// <returns></returns>
	private float Remap(float psi_xy, float min, float max)
	{		
		float val = psi_xy - min;
		float range = max - min;
		val /= range;
		return val;
	}

	private void CreateMesh()
    {

		Vector3[] vertices = new Vector3[(GridSize) * (GridSize)];
		Color[] colors = new Color[(GridSize) * (GridSize)];
		for (int i = 0, y = 0; y < GridSize; y++)
		{
			for (int x = 0; x < GridSize; x++, i++)
			{
				vertices[i] = new Vector3(x, Psi_xy[x,y]*0.1f, y);
				colors[i] = new Color(Remap(Psi_xy[x, y], -256, 256),0,0);
			}
		}
		MeshFilter.mesh = new Mesh();
		MeshFilter.mesh.vertices = vertices;
		MeshFilter.mesh.colors = colors;

		int[] triangles = new int[(GridSize-1) * (GridSize-1) * 6];
		for (int ti = 0, vi = 0, y = 0; y < GridSize-1; y++, vi++)
		{
			for (int x = 0; x < GridSize-1; x++, ti += 6, vi++)
			{
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + GridSize;
				triangles[ti + 5] = vi + GridSize + 1;
			}
		}
		MeshFilter.mesh.triangles = triangles;

		MeshFilter.mesh.RecalculateNormals();
	}


	private void OnDrawGizmos()
	{
		Vector3[] corns = new Vector3[] { Vector3.zero, new Vector3(1, 0, 0) * GridSize, new Vector3(1, 0, 1) * GridSize, new Vector3(0, 0, 1) * GridSize };
		for(int i=0;i<4; i++)
		{
			int ip1 = (i + 1) % 4;
			Gizmos.DrawLine(corns[i], corns[ip1]);
		}
	}


}
