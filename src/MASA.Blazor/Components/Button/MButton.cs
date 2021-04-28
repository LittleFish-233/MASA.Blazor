﻿using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MButton : BButton
    {
        [Parameter]
        public bool Depressed { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        protected override void SetComponentClass()
        {
            _btnContentClass = "m-btn__content";
            _btnLoaderClass = "m-btn__loader";

            CssBuilder
                .Add("m-btn")
                .AddIf("m-btn--disabled", () => Disabled)
                .AddIf("m-btn--has-bg", () => !(Icon || Plain || Outlined || Text))
                .AddIf("m-btn--is-elevated", () => !(Depressed || Icon || Plain || Outlined || Text))
                .AddIf("m-btn--round", () => Fab || Icon)
                .AddIf("m-btn--rounded", () => Rounded)
                .AddIf("m-btn--block", () => Block)
                .AddIf("m-btn--loading", () => Loading)
                .AddIf("m-btn--fab", () => Fab)
                .AddIf("m-btn--icon", () => Icon)
                .AddIf("m-btn--outlined", () => Outlined)
                .AddIf("m-btn--plain", () => Plain)
                .AddIf("m-btn--text", () => Text)
                .AddIf("m-btn--tile", () => Tile)
                .AddFirstIf(
                    ("m-size--x-large", () => XLarge),
                    ("m-size--large", () => Large),
                    ("m-size--small", () => Small),
                    ("m-size--x-small", () => XSmall),
                    ("m-size--default", () => true)
                    )
                .AddTheme(Dark);

            StyleBuilder
                .AddIf(() => Height.Value.Match(
                         str => $"height: {str}",
                         num => $"height: {num}px"),
                       () => Height.HasValue)
                .AddIf(() => Width.Value.Match(
                         str => $"width: {str}",
                         num => $"width: {num}px"),
                       () => Width.HasValue)
                .AddIf(() => MinWidth.Value.Match(
                         str => $"min-width: {str}",
                         num => $"min-width: {num}px"),
                       () => MinWidth.HasValue)
                .AddIf(() => MaxWidth.Value.Match(
                         str => $"max-width: {str}",
                         num => $"max-width: {num}px"),
                       () => MaxWidth.HasValue)
                .AddIf(() => MinHeight.Value.Match(
                         str => $"min-height: {str}",
                         num => $"min-height: {num}px"),
                       () => MinHeight.HasValue)
                .AddIf(() => MaxHeight.Value.Match(
                         str => $"max-height: {str}",
                         num => $"max-height: {num}px"),
                       () => MaxHeight.HasValue);

            if (!string.IsNullOrWhiteSpace(Color))
            {
                if (Color.StartsWith("#"))
                {
                    if (Icon || Outlined || Plain || Text)
                    {
                        StyleBuilder.Add($"color:{Color}");
                    }
                    else
                    {
                        StyleBuilder.Add($"background-color: {Color}");
                    }
                }
                else
                {
                    if (Icon || Outlined || Plain || Text)
                    {
                        CssBuilder.Add($"{Color}--text");
                    }
                    else
                    {
                        CssBuilder.Add(Color);
                    }
                }
            }
        }
    }
}