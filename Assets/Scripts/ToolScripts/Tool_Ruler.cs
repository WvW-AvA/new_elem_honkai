using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
public class Tool_Ruler : MonoBehaviour
{
    public Material lineMat;
    public Color lineColor = new Color(1, 1, 1, 0.5f);
    [Range(1, 100)]
    public int gridSize = 1;

    public GameObject followCamera;
    private Camera m_camera;
    private void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }
    private void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    private void OnEndCameraRendering(ScriptableRenderContext arg1, Camera arg2)
    {
        OnPostRender();
    }
    private void OnPostRender()
    {
        DrawGrid();
    }
    private void DrawGrid()
    {
        if (lineMat == null)
            return;

        Vector2 camera_pos = m_camera.transform.position;
        int w = m_camera.pixelWidth;
        int h = m_camera.pixelHeight;

        Vector2 leftUp = m_camera.ScreenToWorldPoint(new Vector2(0, h));
        Vector2 rightDown = m_camera.ScreenToWorldPoint(new Vector2(w, 0));

        Debug.Log(leftUp);
        Debug.Log(rightDown);
        float t_w = rightDown.x - leftUp.x;
        float t_h = leftUp.y - rightDown.y;

        int x = (int)t_w / (2 * gridSize);
        int y = (int)t_h / (2 * gridSize);

        float offset_x = camera_pos.x % gridSize;
        float offset_y = camera_pos.y % gridSize;


        for (int i = -x; i < x; i++)
        {
            Vector2 pos1 = new Vector2(i * gridSize + camera_pos.x, leftUp.y);
            Vector2 pos2 = new Vector2(i * gridSize + camera_pos.x, rightDown.y);
            DrawLine(pos1, pos2, lineColor);
        }

        for (int i = -y; i < y; i++)
        {
            Vector2 pos1 = new Vector2(leftUp.x, i * gridSize + camera_pos.y);
            Vector2 pos2 = new Vector2(rightDown.x, i * gridSize + camera_pos.y);
            DrawLine(pos1, pos2, lineColor);
        }


    }

    private void DrawLine(Vector2 pos1, Vector2 pos2, Color color)
    {
        GL.Begin(GL.LINES);
        lineMat.SetPass(0);
        GL.Color(color);
        GL.Vertex3(pos1.x, pos1.y, 0);
        GL.Vertex3(pos2.x, pos2.y, 0);
        GL.End();
        GL.Begin(GL.LINES);
    }
}
