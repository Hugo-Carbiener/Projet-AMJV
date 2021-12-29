using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private static GameObject damagePopUpPrefab;
    [SerializeField]
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Camera cam;

    private void Awake()
    {
        if (!textMesh) textMesh = GetComponent<TextMeshPro>();
        cam = Camera.main;
    }

    public static DamagePopUp Create(Vector3 position, int damageAmount, bool isCriticalHit)
    {
        damagePopUpPrefab = Resources.Load("DamagePopUp") as GameObject;
        GameObject damagePopUpObject = Instantiate(damagePopUpPrefab, position, Quaternion.identity);
        DamagePopUp damagePopUp = damagePopUpObject.GetComponent<DamagePopUp>();
        damagePopUp.Setup(damageAmount, isCriticalHit);

        return damagePopUp;
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());
        if (!isCriticalHit) {
            // normal hit
            textMesh.fontSize = 20;
            textColor = new Color(255, 145, 0);
        }
        else
        {
            // critical hit
            textMesh.fontSize = 36;
            textColor = new Color(255, 0, 0);
        }
        textMesh.color = textColor; ;
        disappearTimer = 1f;
    }

    private void Update()
    {
        disappearTimer -= Time.deltaTime;
        float moveYSpeed = 5;
        transform.position += new Vector3(0, moveYSpeed, 0) * Time.deltaTime;

        if (disappearTimer < 0 )
        {
            float disappearSpeed = 20f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
