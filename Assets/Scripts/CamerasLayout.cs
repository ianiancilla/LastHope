using UnityEngine;

public class CamerasLayout : MonoBehaviour
{
    [SerializeField] int cameraRows;
    [SerializeField] int cameraColumns;
    [SerializeField] float cameraPadding;
    [SerializeField] float bottomMargin;

    private Sector[] sectors;

    private void Start()
    {
        sectors = FindObjectsByType<Sector>(FindObjectsSortMode.None);

        if (sectors.Length > cameraColumns * cameraRows)
        {
            Debug.Log("Too many sectors for current camera layout!");
            return;
        }

        SetCamerasLayout();
    }

    private void SetCamerasLayout()
    {
        float cameraW = (1f - cameraPadding) / ((float)cameraColumns + cameraPadding);
        float cameraH = (1f - cameraPadding - bottomMargin) / ((float)cameraRows + cameraPadding);

        int currentCol = 0;
        int currentRow = 0;

        foreach (Sector sector in sectors)
        {
            if (currentCol >= cameraColumns) { currentCol = 0; }
            if (currentRow >= cameraRows) { currentRow = 0; }

            float camX = cameraPadding + (currentCol * (cameraW + cameraPadding));
            float camY = cameraPadding + bottomMargin + (currentRow * (cameraH + cameraPadding));

            sector.SetCameraViewportRect(camX, camY, cameraW, cameraH);

            currentCol++;
            if (currentCol >= cameraColumns) { currentRow++; }
            //Debug.Log($"Placed sector {sector.gameObject.name}");
        }
    }
}
