// Copyright © CerDak

using Microsoft.AspNetCore.Components;

namespace Caarro.Shared;

public abstract class ApplicationComponentBase : ComponentBase, IDisposable
{
    private CancellationTokenSource? cancellationTokenSource;
    protected CancellationToken CancellationToken => (cancellationTokenSource ??= new()).Token;
    
    public void Dispose()
    {
        if (cancellationTokenSource is null)
        {
            return;
        }

        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();
        cancellationTokenSource = null;
    }
}
