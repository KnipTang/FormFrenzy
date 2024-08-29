using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] _AudienceMembers;
    private List<Animator> _AnimatorMembers;

    [SerializeField] private float _PlayOffset = 0.25f;
    void Start()
    {
        _AudienceMembers = GameObject.FindGameObjectsWithTag("AudienceMember");

        _AnimatorMembers = new List<Animator>();
        foreach (var audienceMember in _AudienceMembers)
        {
            if (audienceMember.GetComponentInChildren<Animator>() != null)
            {
                _AnimatorMembers.Add(audienceMember.GetComponentInChildren<Animator>());
            }
        }
    }

    public IEnumerator PlayPositiveAudienceAnimation()
    {
        foreach (var animators in _AnimatorMembers)
        {
            if (animators != null)
            {
                yield return new WaitForSeconds(Random.Range(0, _PlayOffset));
                animators.SetTrigger("cheering");
                animators.SetInteger("rand", Random.Range(0, 3));
            }
        }
    }

    public IEnumerator PlayNegativeAudienceAnimation()
    {
        foreach (var animators in _AnimatorMembers)
        {
            if (animators != null)
            {
                yield return new WaitForSeconds(Random.Range(0, _PlayOffset));
                animators.SetTrigger("booing");
                animators.SetInteger("rand", Random.Range(0, 3));
            }
        }
    }
}
