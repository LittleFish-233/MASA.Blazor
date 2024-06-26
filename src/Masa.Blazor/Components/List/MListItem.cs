using System.Reflection.Metadata;

namespace Masa.Blazor
{
    public partial class MListItem : BListItem, IThemeable
    {
        /// <summary>
        /// Lowers max height of list tiles
        /// </summary>
        [Parameter]
        public bool Dense { get; set; }

        /// <summary>
        /// If set, the list tile will not be rendered as a link even if it has to/href prop or @click handler
        /// </summary>
        [Parameter]
        public bool Inactive { get; set; }

        /// <summary>
        /// Allow text selection inside v-list-item. This prop uses user-select
        /// </summary>
        [Parameter]
        public bool Selectable { get; set; }

        /// <summary>
        /// Increases list-item height for two lines. This prop uses line-clamp and is not supported in all browsers.
        /// </summary>
        [Parameter]
        public bool TwoLine { get; set; }

        /// <summary>
        /// Increases list-item height for three lines. This prop uses line-clamp and is not supported in all browsers.
        /// </summary>
        [Parameter]
        public bool ThreeLine { get; set; }

        [Parameter]
        public bool Highlighted { get; set; }

        [Parameter]
        public bool Ripple { get; set; }

        private bool ComputedRipple => IsDirtyParameter(nameof(Ripple)) ? Ripple : (!Disabled && IsClickable);

        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

#if NET8_0_OR_GREATER
            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
#endif
            Attributes["ripple"] = ComputedRipple;
        }

        protected override RenderFragment GenAvatar(string? src)
        {
            return builder =>
            {
                builder.OpenComponent<MListItemAvatar>(0);
                builder.AddAttribute(1, nameof(MListItemAvatar.Size), (StringNumber?)null);
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(sub =>
                {
                    sub.OpenComponent<MImage>(0);
                    sub.AddAttribute(1, "Src", src);
                    sub.CloseComponent();
                }));

                builder.CloseComponent();
            };
        }

        protected override void SetComponentClass()
        {
            var prefix = "m-list-item";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--dense", () => Dense)
                        .AddIf($"{prefix}--disabled", () => Disabled)
                        .AddIf($"{prefix}--selectable", () => Selectable)
                        .AddIf($"{prefix}--two-line", () => TwoLine)
                        .AddIf($"{prefix}--three-line", () => ThreeLine)
                        .AddIf($"{prefix}--link", () => IsClickable && !Inactive)
                        .Add(Class)
                        .AddIf($"{prefix}--active {ComputedActiveClass}", () =>
                        {
                            if (InternalIsActive) return true;

                            if (!Link) return false;

                            return false;
                        })
                        .AddIf("m-list-item--highlighted", () => Highlighted)
                        .AddTextColor(Color)
                        .AddTheme(IsDark, IndependentTheme);
                });
            
            AbstractProvider
                .Apply<BListItemIcon, MListItemIcon>()
                .Apply<BIcon, MIcon>()
                .Apply<BListItemContent, MListItemContent>()
                .Apply<BListItemTitle, MListItemTitle>()
                .Apply<BListItemSubtitle, MListItemSubtitle>();
        }
    }
}
