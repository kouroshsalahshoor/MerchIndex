﻿namespace MerchIndex.Auto.Components.Pages.Demos.StateContainer
{
    public class StateContainer
    {
        private string? savedString;

        public string Property
        {
            get => savedString ?? string.Empty;
            set
            {
                savedString = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
