using UnityEngine;

public class PointDirection : MonoBehaviour
{
    private Vector3 target;
    private Vector3 difference;
    public GameObject cursor;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        facePointer(difference);
    }

    private void facePointer(Vector3 difference)
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        cursor.transform.position = new Vector2(target.x, target.y);
        difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }
}
