﻿using System.Linq;
using OpenHAB.Core.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OpenHAB.Windows.Converters
{
    /// <summary>
    /// TemplateSelector that determines what widget needs to be shown in the UI
    /// </summary>
    public class WidgetTemplateSelector : DataTemplateSelector
    {
        /// <inheritdoc/>
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var widget = item as OpenHABWidget;
            var uiElement = container as UIElement;

            if (widget == null)
            {
                return null;
            }

            var itemType = GetItemViewType(widget);
            switch (itemType)
            {
                case "group":
                    return PageLinkTemplate;
                case "frame":
                    VariableSizedWrapGrid.SetRowSpan(uiElement, widget.Children.Count + 1);
                    return FrameTemplate;
                case "switch":
                    return SwitchTemplate;
                case "rollershutter":
                    return RollershutterTemplate;
                case "slider":
                    return SliderTemplate;
                case "datetime":
                case "text":
                    return TextTemplate;
                //case "color":
                //    return ColorTemplate;
                case "image":
                    return ImageTemplate;
                case "setpoint":
                    return SetpointTemplate;
                case "selection":
                    return SelectionTemplate;
                case "sectionswitch":
                    return SectionSwitchTemplate;
                case "video_mjpeg":
                case "video":
                    return MjpegTemplate;
                case "chart":
                    return ChartTemplate;
                default:
                    return FrameTemplate;
            }
        }

        /// <summary>
        /// Gets or sets the template for a Frame control
        /// </summary>
        public DataTemplate FrameTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for a Selection control
        /// </summary>
        public DataTemplate SelectionTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for a MJPEG video control
        /// </summary>
        public DataTemplate MjpegTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for a rollershutter control
        /// </summary>
        public DataTemplate RollershutterTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for a Pagelink control
        /// </summary>
        public DataTemplate PageLinkTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for a Switch control
        /// </summary>
        public DataTemplate SwitchTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for a Slider control
        /// </summary>
        public DataTemplate SliderTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for a Text control
        /// </summary>
        public DataTemplate TextTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for a Image control
        /// </summary>
        public DataTemplate ImageTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for a section switch control
        /// </summary>
        public DataTemplate SectionSwitchTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for a color picker control
        /// </summary>
        public DataTemplate ColorTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for a setpoint control
        /// </summary>
        public DataTemplate SetpointTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for a chart control
        /// </summary>
        public DataTemplate ChartTemplate { get; set; }

        private string GetItemViewType(OpenHABWidget openHABWidget)
        {
            if (openHABWidget.Type.Equals("Frame"))
            {
                return "frame";
            }

            if (openHABWidget.Type.Equals("Group"))
            {
                return "group";
            }

            if (openHABWidget.Type.Equals("Switch"))
            {
                if (openHABWidget.Mappings != null && openHABWidget.Mappings.Any())
                {
                    return "sectionswitch";
                }

                if (openHABWidget.Item != null)
                {
                    if (openHABWidget.Item.Type != null)
                    {
                        // RollerShutterItem changed to RollerShutter in later builds of OH2
                        if ("RollershutterItem".Equals(openHABWidget.Item.Type) ||
                            "Rollershutter".Equals(openHABWidget.Item.Type) ||
                            "Rollershutter".Equals(openHABWidget.Item.GroupType))
                        {
                            return "rollershutter";
                        }

                        return "switch";
                    }

                    return "switch";
                }

                return "switch";
            }

            if (openHABWidget.Type.Equals("Text"))
            {
                return "text";
            }

            if (openHABWidget.Type.Equals("Slider"))
            {
                return "slider";
            }

            if (openHABWidget.Type.Equals("Image"))
            {
                return "image";
            }

            if (openHABWidget.Type.Equals("Selection"))
            {
                return "selection";
            }

            if (openHABWidget.Type.Equals("Setpoint"))
            {
                return "setpoint";
            }

            if (openHABWidget.Type.Equals("Chart"))
            {
                return "chart";
            }

            if (openHABWidget.Type.Equals("Video"))
            {
                if (openHABWidget.Encoding != null)
                {
                    if (openHABWidget.Encoding.Equals("mjpeg"))
                    {
                        return "video_mjpeg";
                    }

                    return "video";
                }

                return "video";
            }

            if (openHABWidget.Type.Equals("Webview"))
            {
                return "web";
            }

            if (openHABWidget.Type.Equals("Colorpicker"))
            {
                return "color";
            }

            return "generic";
        }
    }
}