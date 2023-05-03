using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Extensions;
using MudBlazor.Utilities;

namespace SimTECH.Components
{
    // Straight up a copy of MudProgressLinear
    public partial class BobProgressLinear : MudComponentBase
    {
        protected string DivClassname =>
            new CssBuilder("mud-progress-linear")
                .AddClass("mud-progress-linear-rounded", Rounded)
                .AddClass($"mud-progress-linear-striped", Striped)
                .AddClass($"mud-progress-linear-{Size.ToDescriptionString()}")
                .AddClass($"mud-progress-linear-color-{Color.ToDescriptionString()}")
                .AddClass("horizontal", !Vertical)
                .AddClass("vertical", Vertical)
                .AddClass("mud-flip-x-rtl")
                .AddClass(Class)
                .Build();

        /// <summary>
        /// The color of the component. It supports the theme colors.
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.Default;

        /// <summary>
        /// The color of the component. It supports the theme colors.
        /// </summary>
        [Parameter]
        public Size Size { get; set; } = Size.Small;

        /// <summary>
        /// If true, border-radius is set to the themes default value.
        /// </summary>
        [Parameter]
        public bool Rounded { get; set; } = false;

        /// <summary>
        /// Adds stripes to the filled part of the linear progress.
        /// </summary>
        [Parameter]
        public bool Striped { get; set; } = false;

        /// <summary>
        /// If true, the progress bar  will be displayed vertically.
        /// </summary>
        [Parameter]
        public bool Vertical { get; set; } = false;

        /// <summary>
        /// Child content of component.
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// The minimum allowed value of the linear progress. Should not be equal to max.
        /// </summary>
        [Parameter]
        public double Min
        {
            get => _min;
            set
            {
                _min = value;
                UpdatePercentages();
            }
        }

        /// <summary>
        /// The maximum allowed value of the linear progress. Should not be equal to min.
        /// </summary>
        [Parameter]
        public double Max
        {
            get => _max;
            set
            {
                _max = value;
                UpdatePercentages();
            }
        }

        private double _min = 0.0;
        private double _max = 100.0;

        private double _value;

        /// <summary>
        /// The maximum allowed value of the linear progress. Should not be equal to min.
        /// </summary>
        [Parameter]
        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdatePercentages();
            }
        }

        protected double ValuePercent { get; set; }

        protected void UpdatePercentages()
        {
            ValuePercent = GetValuePercent();
            StateHasChanged();
        }

        private double GetPercentage(double input)
        {
            var total = Math.Abs(_max - _min);
            if (NumericConverter<double>.AreEqual(0, total))
            {  // numeric instability!
                return 0.0;
            }
            var value = Math.Max(0, Math.Min(total, input - _min));
            return value / total * 100.0;
        }

        public double GetValuePercent() => GetPercentage(_value);

        public string GetStyleBarTransform() =>
            Vertical == true ? $"transform: translateY({(int)Math.Round(100 - ValuePercent)}%);" : $"transform: translateX(-{(int)Math.Round(100 - ValuePercent)}%);";
    }
}
