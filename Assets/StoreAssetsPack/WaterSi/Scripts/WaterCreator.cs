﻿using UnityEngine;
using System.Collections;
struct Wave
{
    public float freq;  // 2*PI / wavelength
    public float amp;   // amplitude
    public float phase; // speed * 2*PI / wavelength
    public Vector2 dir;

    public Wave(float f, float a, float p, Vector2 d)
    {
        this.freq = f;
        this.amp = a;
        this.phase = p;
        this.dir = d;
    }
};
public class WaterCreator : MonoBehaviour {
    public bool createGrid = false;
    public int m_width;
    public int m_height;
    public int m_resolutionX;
    public int m_resolutionY;
    public Material oceanMat;
    public Transform sun;
    // Here are variables that describe the ways. Each entry in the vector4 corresponds to the entry for one way
    // example: Amplitude of wave1 = m_amplitude.x, wave2 = m_amplitude.y etc.
    public float m_repeatTime;
    public int m_numWaves;
    public float m_medianAmplitude;
    public float m_gravity = 9.81f;
    public Vector2 m_windDirection;
    public float m_windDeviation;
    public float m_medianWaveLength;
    public Vector4 m_amplitudes; 
    public Vector4 m_frequencies;
    public Vector4 m_steepness;
    public Vector4 m_speed;
    public Vector4 m_direction12;
    public Vector4 m_direction34;
    public float m_bias = 2.0f; //A higher number will push more of the mesh verts closer to center of grid were player is. Must be >= 1
    

    private Matrix4x4 m_waves = Matrix4x4.zero;
    private ComputeBuffer m_waveBuffer;
    private float w0;
    private GameObject m_grid;
	// Use this for initialization
	void Start () {
        if (createGrid)
        {
            float far = Camera.main.farClipPlane;
            Mesh mesh = CreateUniformGrid();
            //Mesh mesh = CreateRadialGrid(m_resolutionX, m_resolutionY);
            m_grid = new GameObject("Ocean Grid");
            m_grid.AddComponent<MeshFilter>();
            m_grid.AddComponent<MeshRenderer>();
            m_grid.GetComponent<Renderer>().material = oceanMat;
            m_grid.GetComponent<MeshFilter>().mesh = mesh;
            m_grid.transform.position = this.transform.position;
           // m_grid.transform.localScale = new Vector3(far, 1, far);//Make radial grid have a radius equal to far plane
            //m_grid.transform.localScale = new Vector3(1, 1, 1);//Make radial grid have a radius equal to far plane
        }
       

        w0 = 2 * Mathf.PI / m_repeatTime;
        PutValuesInMatrix();
        //oceanMat.SetMatrix("_Waves", CreateWaves());
        //oceanMat.SetMatrix("_Waves2", CreateWaves());
        //oceanMat.SetMatrix("_Waves3", CreateWaves());
        //oceanMat.SetMatrix("_Waves4", CreateWaves());

        //oceanMat.SetMatrix("_Waves5", CreateWaves());
        //oceanMat.SetMatrix("_Waves6", CreateWaves());
        //oceanMat.SetMatrix("_Waves7", CreateWaves());
        //oceanMat.SetMatrix("_Waves8", CreateWaves());

        //oceanMat.SetMatrix("_Waves", CreateDifferentWaves(0.1f));
        //oceanMat.SetMatrix("_Waves2", CreateDifferentWaves(0.12f));
        //oceanMat.SetMatrix("_Waves3", CreateDifferentWaves(0.45f));
        //oceanMat.SetMatrix("_Waves4", CreateDifferentWaves(0.35f));

        //oceanMat.SetMatrix("_Waves5", CreateDifferentWaves(0.6f));
        //oceanMat.SetMatrix("_Waves6", CreateDifferentWaves(1.4f));
        //oceanMat.SetMatrix("_Waves7", CreateDifferentWaves(0.9f));
        //oceanMat.SetMatrix("_Waves8", CreateDifferentWaves(1.5f));

        int WaveSize = sizeof(float)*5;
        Wave[] waves = new Wave[m_numWaves];
        m_waveBuffer = new ComputeBuffer(32, WaveSize);
        
        for(int i = 0; i < m_numWaves; i++)
        {
            float medianAmplitude = Random.Range(0.15f, 1.5f);
            waves[i] = CreateWave(medianAmplitude);
        
        }
        m_waveBuffer.SetData(waves);
        oceanMat.SetBuffer("waveBuffer", m_waveBuffer);
        oceanMat.SetInt("_NumCreatedWaves", m_numWaves);

	}

    // Update is called once per frame
    void Update()
    {
 
  
        oceanMat.SetVector("_SunDir", sun.transform.forward*-1.0f);
        oceanMat.SetVector("_SunColor", sun.GetComponent<Light>().GetComponent<Light>().color);
        
	}

    /// <summary>
    /// Put all variable data into matrix form.
    /// Row 1 contains all amplitude data
    /// Row 2 contains all frequency data
    /// Row 3 contains all steepness data
    /// Row 4 contains all speed data
    /// </summary>
    void PutValuesInMatrix()
    {
        m_waves.SetRow(0, m_amplitudes);
        m_waves.SetRow(1, m_frequencies);
        m_waves.SetRow(2, m_steepness);
        m_waves.SetRow(3, m_speed);
    }

    Matrix4x4 CreateWaves()
    {
        Matrix4x4 waves = new Matrix4x4();
        for (int i = 0; i < 4; i++)
        { 
            float dA = m_medianAmplitude/m_numWaves;
            float amp = Random.Range(m_medianAmplitude - dA, m_medianAmplitude + dA);
            float waveLength = (2 * Mathf.PI * amp ) / (m_steepness.x);	// wavelength
            
            float k = 2 * Mathf.PI / waveLength;
            float w2;
            if (waveLength < 0.01)
                w2 = m_gravity * k * (1 + (k * k) * (waveLength * waveLength));
            else
                w2 = m_gravity * k;
            float w = Mathf.Sqrt(w2);
            int f = (int)(w/w0);
            w = f * w0;

            Vector2 dir = GetRandomDirection();
            dir *= k;
            Vector4 wave = new Vector4(w, amp, dir.x, dir.y);
            waves.SetRow(i, wave);

        }

        return waves;
    }

    Matrix4x4 CreateDifferentWaves(float mAmp)
    {
        Matrix4x4 waves = new Matrix4x4();
        for (int i = 0; i < 4; i++)
        {
            float dA = mAmp / 32;
            float amp = Random.Range(mAmp - dA, mAmp + dA);
            float kA = Random.Range(1.0f/32, 1.0f/8);
            //float waveLength = (2 * Mathf.PI ) / (amp * 32);	// wavelength
            float waveLength = (2 * Mathf.PI * amp) / (kA);
            float k = 2 * Mathf.PI / waveLength;
            float w2;
            if (waveLength < 0.01)
                w2 = m_gravity * k * (1 + (k * k) * (waveLength * waveLength));
            else
                w2 = m_gravity * k;
            float w = Mathf.Sqrt(w2);
            int f = (int)(w / w0);
            w = f * w0;

            Vector2 dir = GetRandomDirection();
            dir *= k;
            Vector4 wave = new Vector4(w, amp, dir.x, dir.y);
            waves.SetRow(i, wave);

        }

        return waves;
    }

    Wave CreateWave(float mAmp)
    {
        float dA = mAmp / 32;
        float amp = Random.Range(mAmp - dA, mAmp + dA);
        float kA = Random.Range(1.0f / 32, 1.0f / 8);
        //float waveLength = (2 * Mathf.PI ) / (amp * 32);	// wavelength
        float waveLength = (2 * Mathf.PI * amp) / (kA);
        float k = 2 * Mathf.PI / waveLength;
        float w2;
        if (waveLength < 0.01)
            w2 = m_gravity * k * (1 + (k * k) * (waveLength * waveLength));
        else
            w2 = m_gravity * k;
        float w = Mathf.Sqrt(w2);
        int f = (int)(w / w0);
        w = f * w0;

        Vector2 dir = GetRandomDirection();
        dir *= k;
        float phase = 0.10f;
        return new Wave(w, amp, phase, dir);
     

    }

    Matrix4x4 CreateMockWaves()
    {
        Matrix4x4 waves = Matrix4x4.zero;
        float w0 = 2 * Mathf.PI / m_repeatTime;						// base frequency 
        float amp = 3.25f;							// amplitude wave 1
        float wl = (2 * Mathf.PI * amp * m_numWaves) / (m_steepness.x);	// wavelength
        float s = 10f;								// speed that does not do anything
        float k = 2 * Mathf.PI / wl;							// wavenumber. k*amp must be < 1 so that waves dont roll over into intself
        float freq = Mathf.Sqrt(9.81f * k);				// dispersion relation
        float w1 = (freq / w0) * w0;					// frequency for wave 1 around base frequency
        Vector2 dir = new Vector2(-1.8f, 1.12f);
        dir *= k;
        waves.SetRow(0, new Vector4(w1, amp, dir.x, dir.y));

        float amp2 = 5.50f;							// amplitude wave 1
        float wl2 = (2 * Mathf.PI * amp2 * m_numWaves) / (m_steepness.x);	// wavelength
        float k2 = 2 * Mathf.PI / wl2;							// wavenumber. k*amp must be < 1 so that waves dont roll over into intself
        float freq2 = Mathf.Sqrt(9.81f * k2);				// dispersion relation
        float w2 = (freq2 / w0) * w0;					// frequency for wave 1 around base frequency
        Vector2 dir2 = new Vector2(2.30f, -0.6f);
        dir2 *= k2;
        waves.SetRow(1, new Vector4(w2, amp2, dir2.x, dir2.y));
        

        float amp3 = 4.60f;							// amplitude wave 1
        float wl3 = (2 * Mathf.PI * amp3 * m_numWaves) / (m_steepness.x);	// wavelength
        float k3 = 2 * Mathf.PI / wl3;							// wavenumber. k*amp must be < 1 so that waves dont roll over into intself
        float freq3 = Mathf.Sqrt(9.81f * k3);				// dispersion relation
        float w3 = (freq3 / w0) * w0;					// frequency for wave 1 around base frequency
        Vector2 dir3 = new Vector2(1.60f, 1.2f);
        dir2 *= k3;
        waves.SetRow(2, new Vector4(w3, amp3, dir3.x, dir3.y));

        float amp4 = 1.750f;							// amplitude wave 1
        float wl4 = (2 * Mathf.PI * amp4 * m_numWaves) / (m_steepness.x);	// wavelength
        float k4 = 2 * Mathf.PI / wl4;							// wavenumber. k*amp must be < 1 so that waves dont roll over into intself
        float freq4 = Mathf.Sqrt(9.81f * k4);				// dispersion relation
        float w4 = (freq4 / w0) * w0;					// frequency for wave 1 around base frequency
        Vector2 dir4 = new Vector2(-2.0f, -0.2f);
        dir4 *= k4;
        waves.SetRow(3, new Vector4(w4, amp4, dir4.x, dir4.y));
        Debug.Log(waves);
        return waves;

    }
    Vector2 GetRandomDirection()
    { 
        float theta = Vector2.Angle(m_windDirection, Vector2.right);
        float minTheta = Mathf.Deg2Rad * (theta - m_windDeviation);
        float maxTheta = Mathf.Deg2Rad * (theta + m_windDeviation);
        theta = Random.Range(minTheta, maxTheta);
        //theta = Random.Range(0, 359);
        return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));

    }

    Mesh CreateUniformGrid()
    {
        int segmentsX = m_resolutionX + 1;
        int segmentsY = m_resolutionY + 1;
        float dx = m_width / m_resolutionX;
        float dy = m_height/ m_resolutionY;
        float du = 1.0f / m_resolutionX;
        float dv = 1.0f / m_resolutionY;
        Vector3[] vertices = new Vector3[segmentsX * segmentsY];
        Vector3[] normals = new Vector3[segmentsX * segmentsY];
        Vector4[] tangents = new Vector4[segmentsX * segmentsY];
        Vector2[] texcoords = new Vector2[segmentsX * segmentsY]; //not used atm

        for (int x = 0; x < segmentsX; x++)
        {
            float px = x * dx;
            for (int y = 0; y < segmentsY; y++)
            {
                normals[x + y * segmentsX] = new Vector3(0, 1, 0);
                tangents[x + y * segmentsX] = new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
                Vector3 vertexPos = new Vector3(px, 0.0f, y * dy);
                vertices[x + y * segmentsX] = vertexPos;
                texcoords[x + y * segmentsX] = new Vector2(x * du, y * dv);
            }
        }

        int[] indices = new int[segmentsX * segmentsY * 6];

        int num = 0;
        for (int x = 0; x < segmentsX - 1; x++)
        {
            for (int y = 0; y < segmentsY - 1; y++)
            {
                indices[num++] = x + y * segmentsX;
                indices[num++] = x + (y + 1) * segmentsX;
                indices[num++] = (x + 1) + y * segmentsX;

                indices[num++] = x + (y + 1) * segmentsX;
                indices[num++] = (x + 1) + (y + 1) * segmentsX;
                indices[num++] = (x + 1) + y * segmentsX;

            }
        }

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = texcoords;
        mesh.normals = normals;
        mesh.triangles = indices;
        mesh.tangents = tangents;

        return mesh;

    }

    Mesh CreateRadialGrid(int segmentsX, int segmentsY)
    {

        Vector3[] vertices = new Vector3[segmentsX * segmentsY];
        Vector3[] normals = new Vector3[segmentsX * segmentsY];
        Vector2[] texcoords = new Vector2[segmentsX * segmentsY]; //not used atm
        Vector4[] tangents = new Vector4[segmentsX * segmentsY];

        float TAU = Mathf.PI * 2.0f;
        float r;
        for (int x = 0; x < segmentsX; x++)
        {
            for (int y = 0; y < segmentsY; y++)
            {
                r = (float)x / (float)(segmentsX - 1);
                r = Mathf.Pow(r, m_bias);

                normals[x + y * segmentsX] = new Vector3(0, 1, 0);

                vertices[x + y * segmentsX].x = r * Mathf.Cos(TAU * (float)y / (float)(segmentsY - 1));
                vertices[x + y * segmentsX].y = 0.0f;
                vertices[x + y * segmentsX].z = r * Mathf.Sin(TAU * (float)y / (float)(segmentsY - 1));
                tangents[x + y * segmentsX] = new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
            }
        }

        int[] indices = new int[segmentsX * segmentsY * 6];

        int num = 0;
        for (int x = 0; x < segmentsX - 1; x++)
        {
            for (int y = 0; y < segmentsY - 1; y++)
            {
                indices[num++] = x + y * segmentsX;
                indices[num++] = x + (y + 1) * segmentsX;
                indices[num++] = (x + 1) + y * segmentsX;

                indices[num++] = x + (y + 1) * segmentsX;
                indices[num++] = (x + 1) + (y + 1) * segmentsX;
                indices[num++] = (x + 1) + y * segmentsX;

            }
        }

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = texcoords;
        mesh.normals = normals;
        mesh.triangles = indices;
        mesh.tangents = tangents;

        return mesh;

    }

    void OnDestroy()
    {
        m_waveBuffer.Release();
    }
}
