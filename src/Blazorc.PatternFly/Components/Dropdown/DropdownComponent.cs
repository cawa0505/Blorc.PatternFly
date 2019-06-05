﻿namespace Blazorc.PatternFly.Components.Dropdown
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Components;

    public class DropdownComponent : UniqueComponentBase
    {
        public DropdownComponent()
        {
            Position = DropdownPosition.Left;
            Direction = DropdownDirection.Down;

            ToggleId = GenerateUniqueId("pf-toggle-id");

            CreateStateConverterContainer<bool>()
                .If("display: none", () => !IsOpen)
                .Watch(() => IsOpen)
                .Update(() => OpenState);
        }

        public override string ComponentName => "toggle";

        public string Class
        {
            get
            {
                var items = new List<string>();

                //if (IsRead)
                //{
                //    items.Add("pf-m-read");
                //}
                //else
                //{
                //    items.Add("pf-m-unread");
                //}

                return string.Join(" ", items);
            }
        }

        public string OpenState { get; set; }

        [Parameter]
        public string ToggleId { get; set; }

        [Parameter]
        public bool IsOpen
        {
            get { return GetPropertyValue<bool>(nameof(IsOpen)); }
            set { SetPropertyValue(nameof(IsOpen), value); }
        }

        [Parameter]
        public bool IsPlain { get; set; }

        [Parameter]
        public DropdownPosition Position { get; set; }

        [Parameter]
        public DropdownDirection Direction { get; set; }

        [Parameter]
        public RenderFragment<IToggle> Toggle { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public RenderFragment Items { get; set; }

        [Parameter]
        public EventHandler<EventArgs> SelectionChanged { get; set; }
    }
}