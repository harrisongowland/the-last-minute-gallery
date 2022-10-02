using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCanvas : MonoBehaviour
{

    [SerializeField] private Camera m_Camera;
    [SerializeField] private int m_PenSize;
    [SerializeField] private Color m_CurrentColor = Color.black;
    [SerializeField] private Color[] m_AvailableColors; 

    [SerializeField] private Texture2D m_DefaultTex;
    [SerializeField] private MeshRenderer m_Renderer;

    public Texture2D m_RecordedTex;
    
    [SerializeField] private bool m_CanPaint;

    [SerializeField] private AudioSource m_LoopingPaintingSound;
    
    public bool CanPaint
    {
        get => m_CanPaint;
        set => m_CanPaint = value;
    }

    void Start()
    {
        ResetCanvas();
    }

    public void ResetCanvas()
    {
        Texture2D resetTex = new Texture2D(1024, 512, TextureFormat.RGBA32, false);
        m_Renderer.sharedMaterial.mainTexture = resetTex; 
    }
    
    // Update is called once per frame
    void Update()
    {
        //Reset canvas check
        if (Input.GetMouseButtonDown(1))
        {
            ResetCanvas();
            m_LoopingPaintingSound.Stop(); 
            return; 
        }
        
        //Change color check
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_CurrentColor = m_AvailableColors[0];
            m_LoopingPaintingSound.Stop(); 
            return; 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_CurrentColor = m_AvailableColors[1];
            m_LoopingPaintingSound.Stop(); 
            return; 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_CurrentColor = m_AvailableColors[2];
            m_LoopingPaintingSound.Stop(); 
            return; 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            m_CurrentColor = m_AvailableColors[3];
            m_LoopingPaintingSound.Stop(); 
            return; 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            m_CurrentColor = m_AvailableColors[4];
            m_LoopingPaintingSound.Stop(); 
            return; 
        }
        else if (!Input.GetMouseButton(0) || !CanPaint)
        {
            m_LoopingPaintingSound.Stop(); 
            return;
        }

        RaycastHit hit;
        if (!Physics.Raycast(m_Camera.ScreenPointToRay(Input.mousePosition), out hit))
        {
            Debug.Log("No hit");
            m_LoopingPaintingSound.Stop(); 
            return; 
        }

        Renderer renderer = hit.transform.GetComponent<Renderer>();
        MeshCollider collider = hit.collider as MeshCollider;

        if (renderer == null || renderer.sharedMaterial == null || renderer.sharedMaterial.mainTexture == null ||
            collider == null)
        {
            Debug.Log("No renderer || shared material || or main texture || collider");
            m_LoopingPaintingSound.Stop(); 
            return; 
        }

        m_RecordedTex = renderer.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= m_RecordedTex.width;
        pixelUV.y *= m_RecordedTex.height;

        int lowerBoundX = (int)pixelUV.x - (int)m_PenSize / 2;
        int upperBoundX = (int)pixelUV.x + (int)m_PenSize / 2;

        int lowerBoundY = (int)pixelUV.y - (int)m_PenSize / 2;
        int upperBoundY = (int)pixelUV.y + (int)m_PenSize / 2;

        for (int x = lowerBoundX; x < upperBoundX; x++)
        {
            for (int y = lowerBoundY; y < upperBoundY; y++)
            {
                m_RecordedTex.SetPixel(x, y, m_CurrentColor);
            }
        }
        m_RecordedTex.Apply();

        if (!m_LoopingPaintingSound.isPlaying)
        {
            m_LoopingPaintingSound.Play();
        }
    }
}
