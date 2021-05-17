﻿using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MSwitchInputSlot : BInputSlot
    {
        [Parameter]
        public bool IsDisabled { get; set; }

        [Parameter]
        public bool IsActive { get; set; }

        [Parameter]
        public string ValidationState { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--selection-controls__input");
                })
                .Apply("ripple", cssBuilder =>
                 {
                     cssBuilder
                         .Add("m-input--selection-controls__ripple")
                         .AddIf($"{ValidationState}--text",()=>IsActive);
                 })
                .Apply("track", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--switch__track")
                        .AddIf($"{ValidationState}--text", () => IsActive)
                        .AddTheme(Dark);
                })
                .Apply("thumb", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--switch__thumb")
                        .AddIf($"{ValidationState}--text", () => IsActive)
                        .AddTheme(Dark);
                });
        }
    }
}