using System;
using UnityEngine;
using UniVRM10;
using UniGLTF;
using System.Threading;
using System.Threading.Tasks;

public class VrmaPlaybackController : MonoBehaviour
{
    [SerializeField] private TextAsset modelData;
    [SerializeField] private TextAsset motionData;

    private Vrm10AnimationInstance motion;
    private Vrm10Instance character;

    private RuntimeGltfInstance motionInstance;

    private async void Start()
    {
        if (!ValidateInputs())
        {
            return;
        }

        var cancellationToken = destroyCancellationToken;
        bool playbackAccepted = false;

        try
        {
            bool characterReady = await PrepareCharacterAsync(cancellationToken);

            if (cancellationToken.IsCancellationRequested ||
                !characterReady)
            {
                return;
            }

            var motionAnimation = await PrepareMotionAsync(cancellationToken);

            if (cancellationToken.IsCancellationRequested ||
                motionAnimation == null)
            {
                return;
            }

            playbackAccepted = ConnectAndPlay(motionAnimation);
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {

        }
        catch (Exception exception)
        {
            Debug.LogException(exception, this);
        }
        finally
        {
            if (!playbackAccepted)
            {
                ReleaseLoadedObjects();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        ReleaseLoadedObjects();
    }



    private bool ValidateInputs()
    {
        if (modelData == null)
        {
            Debug.LogError("Assign Model Data.", this);
            return false;
        }

        if (motionData == null)
        {
            Debug.LogError("Assign Motion Data.", this);
            return false;
        }

        return true;
    }

    private async Task<bool> PrepareCharacterAsync(
        CancellationToken cancellationToken)
    {
        character = await Vrm10.LoadBytesAsync(
            modelData.bytes,
            canLoadVrm0X: false,
            controlRigGenerationOption: ControlRigGenerationOption.Generate,
            ct: cancellationToken
        );

        if (cancellationToken.IsCancellationRequested)
        {
            return false;
        }

        if (character == null)
        {
            Debug.LogError("Character loading returned null.", this);
            return false;
        }

        if (character.Runtime.ControlRig == null)
        {
            Debug.LogError("ControlRig was not generated.", this);
            return false;
        }

        Debug.Log("Character loaded; ControlRig is ready.", this);
        return true;
    }

    private async Task<Animation> PrepareMotionAsync(
        CancellationToken cancellationToken
    )
    {
        using (var data = new GlbBinaryParser(
            motionData.bytes, motionData.name).Parse())
        using (var loader = new CleanupOnFailureVrmAnimationImporter(
            new VrmAnimationData(data)))
        {
            motionInstance = await loader.LoadAsync(
                new RuntimeOnlyAwaitCaller()
            );
        }

        if (cancellationToken.IsCancellationRequested)
        {
            return null;
        }

        if (motionInstance == null)
        {
            Debug.LogError("Motion loading returned null.", this);
            return null;
        }

        motion = motionInstance.GetComponent<Vrm10AnimationInstance>();

        if (motion == null)
        {
            Debug.LogError("Loaded motion has no Vrm10AnimationInstance.", this);
            return null;
        }

        motion.ShowBoxMan(false);

        if (motion.ControlRig.Item1 == null ||
            motion.ControlRig.Item2 == null)
        {
            Debug.LogError("Motion pose providers are not ready.", this);
            return null;
        }

        var motionAnimation = motion.GetComponent<Animation>();

        if (motionAnimation == null)
        {
            Debug.LogError("Motion has no Animation component.", this);
            return null;
        }

        return motionAnimation;
    }

    private bool ConnectAndPlay(Animation motionAnimation)
    {
        character.Runtime.VrmAnimation = motion;
        motionAnimation.Stop();
        if (!motionAnimation.Play())
        {
            Debug.LogError("Could not start the default motion animation.", this);
            return false;
        }
        return true;
    }

    private void ReleaseLoadedObjects()
    {
        if (character != null)
        {
            character.enabled = false;
            Destroy(character.gameObject);
            character = null;
        }

        motion = null;

        if (motionInstance != null)
        {
            foreach (var node in motionInstance.Nodes)
            {
                if (node != null && node.parent == null)
                {
                    Destroy(node.gameObject);
                }
            }

            Destroy(motionInstance.gameObject);
            motionInstance = null;
        }
    }

}
