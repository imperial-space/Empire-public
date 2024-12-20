using Robust.Client.Graphics;
using Robust.Shared.Enums;
using Robust.Shared.Prototypes;

namespace Content.Client.Imperial.Pixelize;


public sealed class PixelizeOverlay : Overlay
{
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;

    public override OverlaySpace Space => OverlaySpace.WorldSpace;
    public override bool RequestScreenTexture => true;
    private readonly ShaderInstance _pixelizeShader;


    public PixelizeOverlay()
    {
        IoCManager.InjectDependencies(this);

        _pixelizeShader = _prototypeManager.Index<ShaderPrototype>("Pixelize").InstanceUnique();
    }

    protected override void Draw(in OverlayDrawArgs args)
    {
        if (ScreenTexture == null) return;

        var handle = args.WorldHandle;
        var viewport = args.WorldBounds;

        _pixelizeShader.SetParameter("SCREEN_TEXTURE", ScreenTexture);

        handle.UseShader(_pixelizeShader);

        handle.DrawRect(viewport, Color.White);

        handle.UseShader(null);
    }
}
