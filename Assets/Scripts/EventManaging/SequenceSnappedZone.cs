using UnityEngine;
using UnityEngine.Events;

public class SequencedSnapZone : MonoBehaviour
{

    //Puzzles that needs to be solved before this one
    public SequencedSnapZone[] prerequisiteZones;


    //Event that will happen when this puzzle is solved
    public UnityEvent onSolved;

    //Solved? or unlocked?
    public bool isSolved = false;
    public bool isUnlocked = false;


    public GameObject interactButtonPrefab;
    private GameObject spawnedButton;


    public GameObject unlockIndicator;

    void Start()
    {

        // ���� ������ ������ �ڵ����� ����
        if (prerequisiteZones.Length == 0)
        {
            isUnlocked = true;
        }
        else
        {
            isUnlocked = false; // �� �������� ��!
        }

        if (interactButtonPrefab != null)
        {
            spawnedButton = null;
        }
    }

    public bool CanSnap()
    {
        return isUnlocked && !isSolved;
    }

    public void OnSnapped()
    {
        if (spawnedButton == null && interactButtonPrefab != null)
        {
            Vector3 spawnPos = transform.position + new Vector3(0, 1f, 0); // SnapZone ����
            spawnedButton = Instantiate(interactButtonPrefab, spawnPos, Quaternion.identity);

            // �θ� Canvas �ȿ� �ְ� �ʹٸ�:
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
                spawnedButton.transform.SetParent(canvas.transform, false);

            // �� ��ư�� ���� �����ϵ��� ���� (��: SnapZoneTrigger�� ��ũ �ɱ�)
            var trigger = spawnedButton.GetComponent<SnapZoneTrigger>();
            if (trigger != null)
                trigger.linkedZone = this;
        }
    }

    public void InteractAndSolve()
    {
        if (!isUnlocked || isSolved)
            return;

        isSolved = true;
        if (spawnedButton != null)
            Destroy(spawnedButton);

        Debug.Log($"{gameObject.name} ���� �ذ�!");
        onSolved.Invoke();
    }

    public void Unlock()
    {
        isUnlocked = true;

        if (unlockIndicator != null)
        {
            unlockIndicator.SetActive(true);
        }
    }
}