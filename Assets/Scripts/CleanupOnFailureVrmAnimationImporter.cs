using System;
using System.Threading.Tasks;
using UniGLTF;
using UniVRM10;
using UnityEngine;

internal sealed class CleanupOnFailureVrmAnimationImporter : VrmAnimationImporter
{
    public CleanupOnFailureVrmAnimationImporter(VrmAnimationData data) : base(data)
    {
    }

    public override async Task<RuntimeGltfInstance> LoadAsync(
        IAwaitCaller awaitCaller,
        Func<string, IDisposable> measureTime = null
    )
    {
        try
        {
            return await base.LoadAsync(awaitCaller, measureTime);
        }
        catch
        {
            foreach (var node in Nodes)
            {
                if (node != null && node.parent == null && node.gameObject != Root)
                {
                    node.gameObject.SetActive(false);
                    UnityEngine.Object.Destroy(node.gameObject);
                }
            }

            if (Root != null)
            {
                Root.SetActive(false);
                UnityEngine.Object.Destroy(Root);
            }

            throw;
        }
    }
}
