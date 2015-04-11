using UnityEngine;
using System.Collections;

public class Spectrum : MonoBehaviour 
{

    public AudioSource asource;
    public GameObject spectrumParticle;
    public int channel;
    public int numSamples;


	private GameObject[] particles;
    float[] spectrum; 
    float[] volume;
    
    private float widthOfSp; //Width of spectrum particle
    
    // Use this for initialization
    void Start () 
    {
    	Vector3 startpos = transform.position;
    	transform.position = Vector3.zero;
    	
    	Quaternion startRot = transform.rotation;
    	transform.rotation = Quaternion.identity;
    
        volume = new float[numSamples];
        spectrum = new float[numSamples];
        widthOfSp = spectrumParticle.transform.localScale.x/2;
        
        particles = new GameObject[256];
    
    	for (int i=0; i<255; i++)
    	{
            particles[i] = Instantiate(spectrumParticle, new Vector3(i * widthOfSp,0,0), transform.rotation) as GameObject ;
			particles[i].name = "sp"+i;
			particles[i].transform.SetParent(gameObject.transform);
    	}
    	transform.position = startpos;
    	transform.rotation = startRot;
    
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
			try {
	    	    /*GameObject curSpObj = GameObject.Find(curSpName);
	    	    curSpObj.transform.localScale= new  Vector3(widthOfSp,400*spectrum[i],1);
	            tempColor = new Color(100 * spectrum[i], 1f / (20 * spectrum[i]), 1f / (20 * spectrum[i]), 0.1f);
	            curSpObj.GetComponent<MeshRenderer>().material.color = tempColor;*/
	            
				particles[i].transform.localScale= new  Vector3(widthOfSp,400*spectrum[i],1);
				tempColor = new Color(100 * spectrum[i], 1f / (20 * spectrum[i]), 1f / (20 * spectrum[i]), 0.1f);
				particles[i].GetComponent<MeshRenderer>().material.color = tempColor;
			} catch {
				
			}
		}
	}
}
