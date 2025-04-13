using UnityEngine;

public class BackgroundPattern : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private Vector2 moveDirection = new Vector2(0, -1);

    // texture that will be used for the background pattern
    [SerializeField] private Texture2D shopTexture;
    [SerializeField] private Texture2D meronpanTexture;

    [SerializeField] private int meronpan_xtile = 20;
    [SerializeField] private int meronpan_ytile = 12;

    [SerializeField] private int shop_xtile = 1;
    [SerializeField] private int shop_ytile = 1;

    private bool isShopTexture = false;

    void Start()
    {
        // get current texture
        GetComponent<Renderer>().material.mainTexture = meronpanTexture;
        GetComponent<Renderer>().material.mainTextureScale = new Vector2(meronpan_xtile, meronpan_ytile);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShopTexture)
        {
            MoveUVTexture();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShopTexture = !isShopTexture;
            if (isShopTexture)
            {
                GetComponent<Renderer>().material.mainTexture = shopTexture;
                GetComponent<Renderer>().material.mainTextureScale = new Vector2(shop_xtile, shop_ytile);
                resetOffset();
            }
            else
            {
                GetComponent<Renderer>().material.mainTexture = meronpanTexture;
                GetComponent<Renderer>().material.mainTextureScale = new Vector2(meronpan_xtile, meronpan_ytile);
                resetOffset();
            }
        }
    }

    // move UV texture
    void MoveUVTexture()
    {
        // move UV texture, use delta time
        GetComponent<Renderer>().material.mainTextureOffset += new Vector2(moveSpeed * moveDirection.x, moveSpeed * moveDirection.y) * Time.deltaTime;
    }

    void resetOffset()
    {
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, 0);
    }
}
