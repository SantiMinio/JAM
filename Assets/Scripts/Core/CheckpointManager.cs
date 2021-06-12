using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] Checkpoint[] checkpoints = new Checkpoint[0];

    public bool CheckIfCheckpoint(Vector3 pos)
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i].transform.position == pos) return true;
        }

        return false;
    }
}
