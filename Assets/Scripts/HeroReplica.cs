using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroReplica : MonoBehaviour
{
    public static HeroReplica instance;

    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _replicaText;
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowReplica(string _replica)
    {
        _replicaText.text = _replica;
        StartCoroutine(AnimateReplica());
    }

    IEnumerator AnimateReplica()
    {
        _animator.SetTrigger("showReplica");
        yield return new WaitForSeconds(3f);
        _animator.SetTrigger("hideReplica");
        yield return null;

    }

}
