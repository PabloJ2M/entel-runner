using UnityEngine;

public class RandomAnimationClip : StateMachineBehaviour
{
    [SerializeField] private string _stateMachine;
    [SerializeField] private AnimationClip[] _variations = new AnimationClip[1];

    private const string _baseLayer = "Base Layer";
    private string Path => $"{_baseLayer}.{_stateMachine}";

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int random = Random.Range(0, _variations.Length);
        animator.CrossFade($"{Path}.{_variations[random].name}", 0.05f, 0, 0f);
    }
}