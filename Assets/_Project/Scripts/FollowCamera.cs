using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject _followingObgect;

    private void Update()
    {
        MoveCamera(_followingObgect.transform.position);
    }

    public void MoveCamera(Vector3 position)
    {
        transform.position = new Vector3(position.x, position.y,transform.position.z);
    }
}
