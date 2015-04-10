using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleColor : MonoBehaviour {

    Text myTitleText;
    public GameObject myTextEditor;
	// Use this for initialization

    string[] colors;
    char[] letters;

    string colorHTMLBegin = "<color=#";
    string colorHTMLMiddle = ">";
    string colorHTMLEnd = "</color>";

    int startingColor;

    int updateCounter;

	void Start () {
        colors = new string[7];
        colors[0] = "f00";
        colors[1] = "f80";
        colors[2] = "ff0";
        colors[3] = "0f0";
        colors[4] = "0ff";
        colors[5] = "f0f";
        colors[6] = "00f";
        letters = new char[7];
        letters[0] = 'T';
        letters[1] = 'U';
        letters[2] = 'R';
        letters[3] = 'N';
        letters[4] = 'T';
        letters[5] = 'U';
        letters[6] = 'P';

        startingColor = 0;
        updateCounter = 0;

        myTitleText = myTextEditor.GetComponent<Text>();
       // myTitleText.text = "Yo";
	}


    void FixedUpdate ()
    {
        if (updateCounter > 2)
        {
            string updatedTitle = getTitle();
            myTitleText.text = updatedTitle;
            updateCounter = 0;
        }
        updateCounter++;
    }

    private string getTitle()
    {
        char currentLetter;
        // Use StringBuilder for concatenation in tight loops.
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for(int i = 0; i < 7; i++)
        {
            sb.Append(colorHTMLBegin);
            sb.Append(colors[((i + startingColor) % 7)]);
            sb.Append(colorHTMLMiddle);
            sb.Append(letters[i]);
            sb.Append(colorHTMLEnd);
            if (i == 4) { sb.Append(" "); }
        }
        startingColor = ((startingColor + 1) % 7);
        return sb.ToString();
    }
}
