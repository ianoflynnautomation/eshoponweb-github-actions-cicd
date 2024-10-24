
using Microsoft.Playwright;

namespace Playwright.DotNet.Components;

/// <summary>
/// Represents an image on the page
/// </summary>
public class Image : Component
{
    public override Type ComponentType => GetType();

    public virtual void Click(LocatorClickOptions options = null)
    {
        DefaultClick(options);
    }

    public virtual void Hover()
    {
        Hover();
    }

    //public virtual string Src => HttpUtility.HtmlDecode(HttpUtility.UrlDecode(GetAttribute("src")));

    public virtual string LongDesc => string.IsNullOrEmpty(GetAttribute("longdesc")) ? null : GetAttribute("longdesc");

    public virtual string Alt => string.IsNullOrEmpty(GetAttribute("alt")) ? null : GetAttribute("alt");

    public virtual string SrcSet => string.IsNullOrEmpty(GetAttribute("srcset")) ? null : GetAttribute("srcset");

    public virtual string Sizes => string.IsNullOrEmpty(GetAttribute("sizes")) ? null : GetAttribute("sizes");

    public virtual int? Height => GetHeightAttribute();

    public virtual int? Width => GetWidthAttribute();
}