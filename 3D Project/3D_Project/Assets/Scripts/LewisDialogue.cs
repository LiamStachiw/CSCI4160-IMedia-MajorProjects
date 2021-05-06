using UnityEngine;

public class LewisDialogue : InteractableObject {
    //List of the dialogue lines
    [SerializeField]
    private string[] lines = {"There's been a disaster! You have to help. (Right-click to continue)",
                                               "Our generator is about to run out of gas, we will only last a few hours at this point. (Right-click to continue)",
                                               "You're going to have to go out into the crater and find some gas. (Right-click to continue)",
                                               "I heard a rumor of an abandoned campsite out in the middle of the crater. (Right-click to continue)",
                                               "There's only one issue, it's not so abandoned anymore. It's been overrun by mutants. (Right-click to continue)",
                                               "You'll stand no chance if they catch you. You'll have to go quiet. (Right-click to continue)",
                                               "There are some knives on the table in the next room, you should take those with you. (Right-click to continue)",
                                               "Use the targets in there to practice your aim. Just remember to pick them back up before you leave. (Right-click to continue).",
                                               "Now go, there isn't much time left. (Right-click to continue)"};

    [SerializeField] private string afterText = "Remember, you'll stand no chance if they catch you. Now hurry! There isn't much time left.";
    [SerializeField] private NPCMovement lewisMovement;

    private int lineNumber = 0;

    public override void Activate() {
        //if there are still lines to be said
        if (lineNumber < lines.Length) {
            lineNumber++;

            //if there are no more lines
            if (lineNumber == lines.Length) {
                //set Lewis to start walking
                lewisMovement.startWalking = true;
            }
        }
    }

    public override string GetInteractionText() {
        //if there are no more lines
        if (lineNumber == lines.Length) {
            return afterText;
        }
        else {
            //otherwise, return the current line
            return lines[lineNumber];
        }

    }

}
