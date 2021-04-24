using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] mySprites;
    public SpriteRenderer mySpriteRenderer;
    public float framesPerSecond;
    public float secondsPerFrame;
    public int currentFrame;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        secondsPerFrame = 1/framesPerSecond;
        Invoke("NextFrame", secondsPerFrame);
    }

    void NextFrame()
    {
        currentFrame = (currentFrame + 1) % mySprites.Length;
        mySpriteRenderer.sprite = mySprites[currentFrame];
        Invoke("NextFrame", secondsPerFrame);
    }

    // Update is called once per frame
    /*
    void Update()
    {
        
    }
    */
}
