﻿namespace Blorc.PatternFly.Components.Dropdown
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Blorc.Components;
    using Blorc.StateConverters;
    using Core;
    using Microsoft.AspNetCore.Components;
    using Table;

    public class DropdownComponent : UniqueComponentBase, IToggleComponent
    {
        public DropdownComponent()
        {
            Position = DropdownPosition.Left;
            Direction = DropdownDirection.Down;

            ToggleId = GenerateUniqueId("pf-toggle-id");

            CreateConverter()
                .Fixed("pf-c-dropdown")
                .If(() => Direction == DropdownDirection.Up, "pf-m-top")
                .If(() => IsOpen, "pf-m-expanded")
                .Watch(() => Direction)
                .Watch(() => IsOpen)
                .Update(() => Class);

            CreateConverter()
                .Fixed("pf-c-dropdown__menu")
                .If(() => Position == DropdownPosition.Right, "pf-m-align-right")
                .Watch(() => Position)
                .Update(() => PopupClass);

            CreateConverter()
                .If(() => !IsOpen, "display: none")
                .Watch(() => IsOpen)
                .Update(() => OpenState);
        }

        public override string ComponentName => "toggle";

        internal DropdownToggleComponent DropDownToggle
        {
            get { return GetPropertyValue<DropdownToggleComponent>(nameof(DropDownToggle)); }
            set { SetPropertyValue(nameof(DropDownToggle), value); }
        }

        public string Class { get; set; }

        public string PopupClass { get; set; }

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
        public bool IsGrouped
        {
            get { return GetPropertyValue<bool>(nameof(IsGrouped)); }
            set { SetPropertyValue(nameof(IsGrouped), value); }
        }

        [Parameter]
        public bool IsPlain
        {
            get { return GetPropertyValue<bool>(nameof(IsPlain)); }
            set { SetPropertyValue(nameof(IsPlain), value); }
        }

        [Parameter]
        public DropdownPosition Position
        {
            get { return GetPropertyValue<DropdownPosition>(nameof(Position)); }
            set { SetPropertyValue(nameof(Position), value); }
        }

        [Parameter]
        public DropdownDirection Direction
        {
            get { return GetPropertyValue<DropdownDirection>(nameof(Direction)); }
            set { SetPropertyValue(nameof(Direction), value); }
        }

        [Parameter]
        public RenderFragment Toggle { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public RenderFragment Items { get; set; }

        [Parameter]
        public EventHandler<EventArgs> SelectionChanged { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if (!ToggleComponentContainer.Components.Contains(this))
            {
                ToggleComponentContainer.Components.Add(this);
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == nameof(DropDownToggle))
            {
                var toggle = DropDownToggle;
                if (toggle != null)
                {
                    toggle.Toggled += OnDropDownToggled;
                }
            }
        }

        private void OnDropDownToggled(object sender, EventArgs e)
        {
            IsOpen = DropDownToggle.IsOpen;
            if (ToggleComponentContainer != null && IsOpen)
            {
                ToggleComponentContainer.SetActiveToggleComponent(this);
            }
        }

        public void Close()
        {
            IsOpen = false;
            //// TODO: This can be removed after a binding system fix.
            DropDownToggle?.Close();
        }

        [CascadingParameter]
        public IToggleComponentContainer ToggleComponentContainer { get; set; }
    }
}