using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawShape : MonoBehaviour
{
    private bool inProcces = false;
    private bool started = false;
    private bool onetime = true;
    private int coordX = 0;
    private int coordY = 0;
    private int curIndex = 0;
    private int curListIndex = 0;
    private int curNumberCharacters = 20;
    public static int numberOfCharacters = 20;
    private Vector3 difference;
    List<int> memoryX = new List<int>();
    List<int> memoryY = new List<int>();
    List<GameObject> activeCharacters = new List<GameObject>();


    public Text textScore;
    [SerializeField] private int textureWidth;
    [SerializeField] private int textureHeight;
    [SerializeField] private TextureWrapMode textureWrapMode;
    [SerializeField] private FilterMode filterMode;
    [SerializeField] private Texture2D textureDrawn;
    [SerializeField] private Material material;
    [SerializeField] private int brushSize;
    [SerializeField] private Camera cameraFollow;
    [SerializeField] private GameObject objToFollow;
    [SerializeField] public GameObject[] characters = new GameObject[30];
    [SerializeField] private ParticleSystem[] fireworks = new ParticleSystem[6];


    void Start()
    {
        difference = cameraFollow.transform.position - objToFollow.transform.position;

        if (textureDrawn == null)
            textureDrawn = new Texture2D(textureWidth, textureHeight);

        textureDrawn.wrapMode = textureWrapMode;
        textureDrawn.filterMode = filterMode;
        material.mainTexture = textureDrawn;
        textureDrawn.Apply();
    }

    void Update()
    {
        textScore.text = "Score: " + CharacterMove.score.ToString();

        if (numberOfCharacters != curNumberCharacters)
        {
            while (numberOfCharacters != curNumberCharacters)
            {
                for (int i = 20; i < CharacterMove.index + 21; i++)
                {
                    if (CharacterMove.charactersAded[i - 20] != null && characters[i] != CharacterMove.charactersAded[i - 20])
                    {
                        characters[i] = CharacterMove.charactersAded[i - 20];
                        curNumberCharacters++;
                    }
                }
            }
        }

        if (started && onetime)
        {
            objToFollow.transform.position += Vector3.forward * 5f * Time.deltaTime;
            cameraFollow.transform.position = objToFollow.transform.position + difference;

            for (int i = 0; i < numberOfCharacters; i++)
            {
                if (characters[i].tag != "Died" && characters[i].tag != "Untagged" && characters[i].transform.position.z < 409f)
                    characters[i].transform.position += Vector3.forward * 5f * Time.deltaTime;
                else if (characters[i].transform.position.z > 409f && onetime)
                {
                    for (int j = 0; j < fireworks.Length; j++)
                        fireworks[j].Play();
                    onetime = false;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {

            inProcces = true;
            coordX = (int)Input.mousePosition.x - 165;
            coordY = (int)Input.mousePosition.y - 10;

            if (coordX > 763)
                coordX = 763;
            if (coordX < 0)
                coordX = 0;
            if (coordY > 376)
                coordY = 376;
            if (coordY < 0)
                coordY = 0;

            memoryX.Add(coordX);
            memoryY.Add(coordY);
            curIndex++;

            for (int y = 0; y < brushSize; y++)
            {
                for (int x = 0; x < brushSize; x++)
                {
                    float x2 = Mathf.Pow(x - brushSize / 2, 2);
                    float y2 = Mathf.Pow(y - brushSize / 2, 2);
                    float r2 = Mathf.Pow(brushSize / 2 - 0.5f, 2);

                    if (x2 + y2 < r2)
                    {
                        int pixelX = coordX + x - brushSize / 2;
                        int pixelY = coordY + y - brushSize / 2;

                        if (pixelX >= 0 && pixelX < textureWidth && pixelY >= 0 && pixelY < textureHeight)
                        {
                            textureDrawn.SetPixel(pixelX, pixelY, Color.black);
                        }
                    }
                }
            }
            textureDrawn.Apply();
        }

        if (inProcces && !Input.GetMouseButton(0))
        {
            int k = 0;
            for (int i = 0; i < 30; i++)
            {
                if (characters[i] != null && !characters[i].CompareTag("Died"))
                {
                    activeCharacters.Add(characters[i]);
                    k++;
                }
            }

            int dif = curIndex / k;
            if (dif == 0)
                dif = 1;

            for (int j = 0; j < k; j++)
            {
                if (!activeCharacters[j].CompareTag("Died"))
                {
                    activeCharacters[j].transform.position = new Vector3(371f + 4 * memoryX[curListIndex] / 379f, activeCharacters[j].transform.position.y, objToFollow.transform.position.z + 4 * memoryY[curListIndex] / 364f);
                    curListIndex += Mathf.RoundToInt(dif);
                }
            }

            started = true;
            Color newColor = new(205 / 255f, 205 / 255f, 205 / 255f);
            inProcces = false;

            for (int y = 0; y < textureHeight; y++)
            {
                for (int x = 0; x < textureWidth; x++)
                {
                    textureDrawn.SetPixel(x, y, newColor);
                }
            }

            textureDrawn.Apply();
            memoryX.Clear();
            memoryY.Clear();
            activeCharacters.Clear();
            curListIndex = 0;
            curIndex = 0;
        }

    }
}


