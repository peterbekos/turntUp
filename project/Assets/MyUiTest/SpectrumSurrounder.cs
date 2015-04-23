using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpectrumSurrounder : MonoBehaviour 
{

    //BY THE WAY, Octupus is just another way of saying
    //the spectrum analyzer surrounding a certain object
    #region OctopusEnums

    enum OctopusDraw
    {
        DrawRight,
        DrawDown,
        DrawLeft,
        DrawUp,
    };

    enum OctopusExpand
    {
        ExpandUp,
        ExpandRight,
        ExpandDown,
        ExpandLeft,
    };

    #endregion

    #region OctopusArm
    struct OctopusArm
    { 
        public float xPos;
        public float yPos;
        public GameObject arm;
        public OctopusExpand dirToExpand;
    }

    #endregion

    #region Fields

    public const int numOfSp = 255;
    OctopusArm[] arms;

    public AudioSource asource;
    public GameObject spectrumParticle;
    public int channel;
    public int numSamples;

    public float overallScale;

    float[] spectrum;
    float[] volume;

    private float widthOfSp; //Width of spectrum particle

    float anchorX, anchorY, width, height;

    bool inAButton = false;
    bool armsCreated = false;

    public Canvas mCanvas;
    private float canvasWidth;
    private float canvasHeight;

    public Animator mAnimator;

    #endregion

    public void Start()
    {
        arms = new OctopusArm[numOfSp];
        volume = new float[numSamples];
        spectrum = new float[numSamples];
        overallScale = 1;
        canvasWidth = mCanvas.pixelRect.width;
        canvasHeight = mCanvas.pixelRect.height;
    }

    public void EnterPlayButton(Button currButton)
    {
        Debug.Log("Entered the play button!");
        Transform myRect = (RectTransform)currButton.GetComponent("RectTransform");
        anchorX = (((RectTransform)myRect).anchoredPosition.x);
        anchorY = (((RectTransform)myRect).anchoredPosition.y);
        //anchorX = 0;
        //anchorY = 0;
        width = (((RectTransform)myRect).rect.width);
        height = (((RectTransform)myRect).rect.height);
        inAButton = true;
        
        




       /* var screenPoint = new Vector3(anchorX, anchorY, 0.5f);
        var worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        anchorX = screenPoint.x;
        anchorY = screenPoint.y;

        screenPoint = new Vector3(width, height, 0.5f);
        worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);

        width = worldPoint.x;
        height = worldPoint.y;*/
    }

    public void ExitPlayButton()
    {
        Debug.Log("Exiting the play button!");
        inAButton = false;
    }

    public void Update()
    {
        canvasWidth = mCanvas.pixelRect.width;
        canvasHeight = mCanvas.pixelRect.height;

        //Some debugging messages
        /*Debug.Log("anchorX = " + anchorX);
        Debug.Log("anchory = " + anchorY);
        Debug.Log("width = " + width);
        Debug.Log("height = " + height);
        Debug.Log("Canvaswidth = " + canvasWidth);
        Debug.Log("Canvasheight = " + canvasHeight); */

        if(inAButton)
        {
            drawRectangulerOctopus();
        }
        else
        {
            if(armsCreated)
            { 
                destroyAllArms();
                armsCreated = false;
            }
        }
    }

    //Draw the octopus around in a box-like fashion
    private void drawRectangulerOctopus()
    {
        float perimeter = (height * 2) + (width * 2);
        widthOfSp = perimeter / numOfSp;

        if(!armsCreated)
        {
            float xPos;//x position for every particle
            float yPos;//y position for every particle

            OctopusDraw currentDir = OctopusDraw.DrawRight;
            Vector2 topLeftCorner = new Vector2(anchorX - (width/2) , anchorY + (height/2));
            Vector2 topRightCorner = new Vector2(anchorX + (width/2), anchorY + (height/2));
            Vector2 bottomRightCorner = new Vector2(anchorX + (width/2), anchorY - (height/2));
            Vector2 bottomLeftCorner = new Vector2(anchorX - (width/2), anchorY - (height/2));
            xPos = topLeftCorner.x;
            yPos = topLeftCorner.y;

            for (int i = 0; i < numOfSp; i++)
            {
                arms[i].arm = Instantiate(spectrumParticle, new Vector3(xPos, yPos, 99.96f), transform.rotation) as GameObject;
                switch(currentDir)
                {
                    case OctopusDraw.DrawRight:
                        if(xPos < topRightCorner.x)
                        {
                            xPos += widthOfSp;
                            arms[i].dirToExpand = OctopusExpand.ExpandUp;
                        }
                        else
                        { 
                            currentDir = OctopusDraw.DrawDown;
                            yPos -= widthOfSp;
                            arms[i].dirToExpand = OctopusExpand.ExpandRight;
                        }
                        break;
                    case OctopusDraw.DrawDown:
                        if (yPos > bottomRightCorner.y)
                        {
                            yPos -= widthOfSp;
                            arms[i].dirToExpand = OctopusExpand.ExpandRight;
                        }
                        else
                        {
                            currentDir = OctopusDraw.DrawLeft;
                            xPos -= widthOfSp;
                            arms[i].dirToExpand = OctopusExpand.ExpandDown;
                        }
                        break;
                    case OctopusDraw.DrawLeft:
                        if (xPos > bottomLeftCorner.x)
                        {
                            xPos -= widthOfSp;
                            arms[i].dirToExpand = OctopusExpand.ExpandDown;
                        }
                        else
                        {
                            currentDir = OctopusDraw.DrawUp;
                            yPos += widthOfSp;
                            arms[i].dirToExpand = OctopusExpand.ExpandLeft;
                        }
                        break;
                    case OctopusDraw.DrawUp:
                            yPos += widthOfSp;
                            arms[i].dirToExpand = OctopusExpand.ExpandLeft;
                        break;
                default:
                    break;
                }
            }
            armsCreated = true;
        }

        asource.GetOutputData(volume, channel);
        asource.GetSpectrumData(spectrum, channel, FFTWindow.Rectangular);

        //Color tempColor;

        for (int i = 0; i < numOfSp; i++)
        {
            GameObject curSpObj = arms[i].arm;
            switch(arms[i].dirToExpand)
            { 
                case OctopusExpand.ExpandUp:
                    curSpObj.transform.localScale = new Vector3(1 , 500 * spectrum[i], 1);
                    break;
                case OctopusExpand.ExpandRight:
                    curSpObj.transform.localScale = new Vector3(500 * spectrum[i], 1 , 1);
                    break;
                case OctopusExpand.ExpandDown:
                    curSpObj.transform.localScale = new Vector3(1, 500 * spectrum[i], 1);
                    break;
                case OctopusExpand.ExpandLeft:
                    curSpObj.transform.localScale = new Vector3(500 * spectrum[i], 1 , 1);
                    break;
            //tempColor = new Color(100 * spectrum[i], 1f / (20 * spectrum[i]), 1f / (20 * spectrum[i]), 0.1f);
            //curSpObj.GetComponent<MeshRenderer>().material.color = tempColor;
            }
        }
    }

    private void destroyAllArms()
    {
        foreach(OctopusArm curArm in arms)
        {
            Destroy(curArm.arm);
        }
    }

}
