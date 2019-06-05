﻿namespace Blazorc.PatternFly.Components.EmptyState
{
    using System;
    using Blazorc.PatternFly.Components.Icon;
    using Microsoft.AspNetCore.Components;

    public class EmptyStateIconComponent : BlazorcComponentBase
    {
        public EmptyStateIconComponent()
        {

        }

        [Parameter]
        public string Icon { get; set; }
    }
}