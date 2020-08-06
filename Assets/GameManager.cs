using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Barrier barrier;
    Slider slider;
    public Image fill;

    private void Start()
    {
        barrier = GameObject.FindGameObjectWithTag("Barrier").GetComponent<Barrier>();
        slider = GameObject.Find("Slider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(barrier != null)
        {
            if (barrier.IsDestroyed())
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.buildIndex);
            }

            if(slider != null)
            {
                float percent = barrier.GetHealthPercentage();
                Color newColor = new Color(1 - percent, percent, 0);
                slider.value = percent;
                fill.color = newColor;
            }
        }
    }

    
    
}
