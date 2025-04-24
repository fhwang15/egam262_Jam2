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

        // 선행 조건이 없으면 자동으로 열림
        if (prerequisiteZones.Length == 0)
        {
            isUnlocked = true;
        }
        else
        {
            isUnlocked = false; // 꼭 명시해줘야 함!
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
            Vector3 spawnPos = transform.position + new Vector3(0, 1f, 0); // SnapZone 위쪽
            spawnedButton = Instantiate(interactButtonPrefab, spawnPos, Quaternion.identity);

            // 부모 Canvas 안에 넣고 싶다면:
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
                spawnedButton.transform.SetParent(canvas.transform, false);

            // 이 버튼이 나를 조작하도록 연결 (예: SnapZoneTrigger에 링크 걸기)
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

        Debug.Log($"{gameObject.name} 퍼즐 해결!");
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