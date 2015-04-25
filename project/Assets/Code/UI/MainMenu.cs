using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public Texture backgroundTexture;
    public GUIStyle playButtonTexture;
    public GUIStyle upgradeButtonTexture;
    public GUIStyle creditButtonTexture;
    public GUIStyle exitButtonTexture;
    public float menuStartX, menuStartY, menuWidth, menuHeight, menuOptionDistance;
    private string[] buttonNames = { "Play Stage 1","Play Stage 2", "Upgrade","select a file", "Credit", "Exit" };
    public int selectedIndex;
    bool[] buttons;


    // Use this for initialization
    //void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    // Use this for initialization
    void Start()
    {
        buttons = new bool[buttonNames.Length];
        selectedIndex = 0;
        menuStartX = 0.02f;
        menuStartY = 0.37f;
        menuWidth = 0.32f;
        menuHeight = 0.09f;
        menuOptionDistance = 0.146f;
    }

    int menuSelection(string[] menuItems, int selectedItem, string direction)
    {
        if (direction == "up")
        {
            if (selectedItem == 0)
            {
                selectedItem = menuItems.Length - 1;
            }
            else
            {
                selectedItem -= 1;
            }
        }

        if (direction == "down")
        {
            if (selectedItem == menuItems.Length - 1)
            {
                selectedItem = 0;
            }
            else
            {
                selectedItem += 1;
            }
        }

        return selectedItem;
    }

	FileBrowser file;

    void OnGUI()
    {
		if (file != null) {
			file.OnGUI ();
		} else {
			// display background
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);


			for (int i = 0; i < buttonNames.Length; i++) {
				GUI.SetNextControlName (buttonNames [i]);
				buttons [i] = GUI.Button (new Rect (Screen.width * menuStartX, Screen.height * (menuStartY + menuOptionDistance * i), Screen.width * menuWidth, Screen.height * menuHeight), buttonNames [i]);
				// to use buttons with images enable line below
				//buttons[i] = GUI.Button(new Rect(Screen.width * menuStartX, Screen.height * (menuStartY + menuOptionDistance * i), Screen.width * menuWidth, Screen.height * menuHeight), buttonNames[i], playButtonTexture);
			}

			// Using button with enter key
			if (Input.GetKeyUp (KeyCode.Return)) {
				// When the use key is pressed, the selected button will activate
				buttons [selectedIndex] = true;
			}

			//button actions
			if (buttons [0]) {
				Application.LoadLevel ("LoadingScreen");
				//Application.LoadLevel("Colors");
				GameManager.nextLevel = "Colors";
			}
			if (buttons [1]) {
				print ("clicked upgrade");
			}
			if (buttons [2]) {
				Application.LoadLevel ("LoadingScreen");
				//Application.LoadLevel("FFBoss2");
				GameManager.nextLevel = "FFBoss2";
				//print("clicked credit");//
			}
			if (buttons [3] == true) {
				file = new FileBrowser (
					new Rect (100, 100, 600, 260),
					"Choose Text File",
					FileSelectedCallback
				);

				//FileBrowser myfiles = new FileBrowser(null, "Pick File", new FileBrowser.FinishedCallback() );
		
			}
			if (buttons [4] == true) {
				Application.Quit ();
			}
			GUI.FocusControl (buttonNames [selectedIndex]);
		}
	}

	protected void FileSelectedCallback(string path) {
		file = null;
		Debug.Log (path);
		//m_fileBrowser = null;
		//m_textPath = path;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            selectedIndex = menuSelection(buttonNames, selectedIndex, "up");
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            selectedIndex = menuSelection(buttonNames, selectedIndex, "down");
        }
    }
}
