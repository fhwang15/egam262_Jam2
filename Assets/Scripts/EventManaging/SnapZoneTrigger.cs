using UnityEngine;

public class SnapZoneTrigger : MonoBehaviour
{
    public SequencedSnapZone linkedZone;
    public DialoguePopUp dialogueSystem;

    [TextArea(2, 4)]
    public string dialogueMessage; // SnapZone마다 다르게 설정 가능!

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
