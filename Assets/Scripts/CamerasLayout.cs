using UnityEngine;

[ExecuteAlways]
public class CamerasLayout : MonoBehaviour
{
    [SerializeField] int cameraRows;
    [SerializeField] int cameraColumns;
    [SerializeField] float cameraPadding;
    [SerializeField] float bottomMargin;
    [SerializeField] float topMargin;
    [SerializeField] float sideMargin;

    private Sector[] sectors;
    private void Start()
    {
        sectors = GetComponent<SectorsManager>().Sectors;

        if (sectors.Length > cameraColumns * cameraRows)
        {
            Debug.Log("Too many sectors for current camera layout!");
            return;
        }
    
        SetCamerasLayout();
    }

    private void SetCamerasLayout()
    {
        float cameraW = (1f - cameraPadding - sideMargin * 2) 
                        / ((float)cameraColumns + cameraPadding)
                        - cameraPadding;
        float cameraH = (1f - cameraPadding - bottomMargin - topMargin)
                        / ((float)cameraRows + cameraPadding)
                        - cameraPadding;

        int currentCol = 0;
        int currentRow = 0;

        for (int i = 0; i < sectors.Length; i++)
        {
            if (currentCol >= cameraColumns) { currentCol = 0; }
            if (currentRow >= cameraRows) { currentRow = 0; }

            float camX = cameraPadding + sideMargin + (currentCol * (cameraW + cameraPadding));
            float camY = cameraPadding + bottomMargin + (currentRow * (cameraH + cameraPadding));

            sectors[i].SetCameraViewportRect(camX, camY, cameraW, cameraH);

            currentCol++;
            if (currentCol >= cameraColumns) { currentRow++; }
            //Debug.Log($"Placed sector {sectors[i].gameObject.name} as number {i}");
        }
    }
}
