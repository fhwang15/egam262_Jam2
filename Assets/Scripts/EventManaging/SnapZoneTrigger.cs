using UnityEngine;

public class SnapZoneTrigger : MonoBehaviour
{
    public SequencedSnapZone linkedZone;
    public DialoguePopUp dialogueSystem;

    [TextArea(2, 4)]
    public string dialogueMessage; // SnapZone���� �ٸ��� ���� ����!

    public void OnButtonClick()
    {
        if (linkedZone != null)
        {
            if (!linkedZone.CanSnap())
            {
                if (dialogueSystem != null)
                {
                    dialogueSystem.ShowDialogue(dialogueMessage);
                }
                return;
            }

            linkedZone.InteractAndSolve();
            Destroy(gameObject);
        }
    }
}
