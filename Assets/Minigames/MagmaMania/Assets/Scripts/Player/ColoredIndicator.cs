using UnityEngine;

public class ColoredIndicator : MonoBehaviour
{
    Vector3 position;
    float plattformRadius = 7.75f;

    // Update is called once per frame
    void Update()
    {
        position = new Vector3(transform.position.x, 0.3f, transform.position.z);
        transform.position = position;

        if(Vector3.Distance(new Vector3(0,0,0), transform.position) > plattformRadius)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
