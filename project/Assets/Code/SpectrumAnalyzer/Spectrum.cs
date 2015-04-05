using UnityEngine;
using System.Collections;

public class Spectrum : MonoBehaviour 
{

    public AudioSource asource;
    public GameObject spectrumParticle;
    public int channel;
    public int numSamples;

    float[] spectrum; 
    float[] volume;
    
    private float widthOfSp; //Width of spectrum particle
    
    // Use this for initialization
    void Start () 
    {
        volume = new float[numSamples];
        spectrum = new float[numSamples];
        widthOfSp = spectrumParticle.transform.localScale.x;
    
    	for (float i=1; i<255; i++)
    	{
            GameObject particleClone = Instantiate(spectrumParticle,new Vector3(-38 + (i * widthOfSp),0,0), transform.rotation) as GameObject ;
            particleClone.name = "sp"+i;
    	}
    
    }
    
    // Update is called once per frame
    void Update () 
    {
        asource.GetOutputData(volume, channel);
        asource.GetSpectrumData(spectrum, channel, FFTWindow.Rectangular);
    
        Color tempColor;
    
        for (int i=0; i<255; i++) 
        {
    	    string curSpName = "sp"+(i+1);
    	    GameObject curSpObj = GameObject.Find(curSpName);
    	    curSpObj.transform.localScale= new  Vector3(widthOfSp,400*spectrum[i],1);
            tempColor = new Color(100 * spectrum[i], 1f / (20 * spectrum[i]), 1f / (20 * spectrum[i]), 0.1f);
            curSpObj.GetComponent<MeshRenderer>().material.color = tempColor;
        }
    }
}
