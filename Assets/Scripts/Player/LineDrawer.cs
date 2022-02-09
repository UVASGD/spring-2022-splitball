using UnityEngine;
using System.Collections;

public class LineDrawer : MonoBehaviour
{

    public GameObject gameObject1;          // Reference to the first GameObject
    public GameObject gameObject2;          // Reference to the second GameObject

    private LineRenderer line;                           // Line Renderer

    // Use this for initialization
    void Start()
    {
        // Add a Line Renderer to the GameObject
        line = this.gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the GameObjects are not null
        if (gameObject1 != null && gameObject2 != null)
        {
            // Update position of the two vertex of the Line Renderer
            line.SetPosition(0, gameObject1.transform.position);
            line.SetPosition(1, gameObject2.transform.position);


            transform.position = Vector3.Lerp(gameObject1.GetComponent<Rigidbody2D>().position, gameObject2.GetComponent<Rigidbody2D>().position, 0.5f);
            // transform.position.x = gameObject1.position.x + (gameObject2.position.x - gameObject1.position.x) / 2;
            // transform.position.y = gameObject1.position.y + (gameObject2.position.y - gameObject1.position.y) / 2;
        }
    }
}