using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip openAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("Open");
    }

    private void PlayOpenChestSound()
    {
        audioSource.clip = openAudioClip;
        audioSource.Play();
    }
}
