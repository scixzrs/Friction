using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    public GameObject[] spriteList;
    float spawnedTime;
    float nextSpawnTime;
    int spawned = 0;
    GameObject gb;
    Camera mainCamera;
    Vector2 screenBounds;
    
    private void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
     
    }

    private void Update()
    {
        if (spawned > -1 && spawned < spriteList.Length)
        {
            gb = SpawnSprite();
            InvokeRepeating("SpawnSprite",  1f, 2f);
            spawned++;
        }

        if (spawned > spriteList.Length || Time.time - spawnedTime > 4 || gb ==null ||
            gb.transform.position.x < -screenBounds.x ||
            gb.transform.position.x > screenBounds.x ||
            gb.transform.position.y < -screenBounds.y ||
            gb.transform.position.y > screenBounds.y)
        {

            //Destroy(this.gameObject, 4f);
            spawned--;
        }
        //spawned--;
    }

    GameObject SpawnSprite()
    {
        spawnedTime = Time.time;
        spawned++;
        int randomInt = Random.Range(0, spriteList.Length);
        GameObject newSprite = Instantiate(spriteList[randomInt],RandomPosition(), Quaternion.identity);
        //newSprite.GetComponent<Rigidbody2D>().simulated = false;
        newSprite.transform.localScale = new Vector3(2,2,0);

        SpriteRenderer spriteRenderer = newSprite.GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = new Color(1f, 1f, 1f, 0f);
        StartCoroutine(FadeInSprite(spriteRenderer));
        StartCoroutine("SpawnedForTime");
        StartCoroutine(FadeOutSprite(spriteRenderer));
        return newSprite;
    }

    Vector2 RandomPosition()
    {
        float randomX = Random.Range(-screenBounds.x, screenBounds.x);
        float randomY = Random.Range(-screenBounds.y, screenBounds.y);
        
        return new Vector2(randomX, randomY);
    }

    IEnumerator SpawnedForTime()
    {
        yield return new WaitForSeconds(4f);
    }

    IEnumerator FadeInSprite(SpriteRenderer sprite)
    {
        float elapsedTime = 0f;
        float fadeDuration = 1f; // Adjust the fade duration as desired
        while (elapsedTime < fadeDuration)
        {
            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            sprite.material.color = new Color(1f,1f, 1f, newAlpha);
            elapsedTime += Time.deltaTime;
        }
        yield return new WaitForSeconds(5f);
    } 
    IEnumerator FadeOutSprite(SpriteRenderer sprite)
    {
        float elapsedTime = 0f;
        float fadeDuration = 1f; // Adjust the fade duration as desired
        while (elapsedTime < fadeDuration)
        {
            float newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            sprite.material.color = new Color(1f,1f, 1f, newAlpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (sprite.gameObject != null) { 
            Destroy(sprite.gameObject,3f);
        }
    }
 
}

