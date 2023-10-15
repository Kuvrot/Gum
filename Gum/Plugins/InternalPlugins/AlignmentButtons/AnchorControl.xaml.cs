﻿using Gum.DataTypes.Variables;
using Gum.Managers;
using Gum.ToolStates;
using Gum.Undo;
using System.Windows.Controls;
using static Gum.Plugins.AlignmentButtons.CommonControlLogic;

namespace Gum.Plugins.AlignmentButtons
{
    /// <summary>
    /// Interaction logic for AnchorControl.xaml
    /// </summary>
    public partial class AnchorControl : UserControl
    {
        StateSave CurrentState
        {
            get
            {
                if(SelectedState.Self.SelectedStateSave != null)
                {
                    return SelectedState.Self.SelectedStateSave;
                }
                else
                {
                    return SelectedState.Self.SelectedElement?.DefaultState;
                }
            }
        }

        public AnchorControl()
        {
            InitializeComponent();
        }

        private void TopLeftButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (UndoManager.Self.RequestLock())
            {

                SetXValues(
                global::RenderingLibrary.Graphics.HorizontalAlignment.Left,
                PositionUnitType.PixelsFromLeft);

                SetYValues(
                    global::RenderingLibrary.Graphics.VerticalAlignment.Top,
                    PositionUnitType.PixelsFromTop);

                RefreshAndSave();
            }
        }


        private void TopButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (UndoManager.Self.RequestLock())
            {

                SetXValues(
                global::RenderingLibrary.Graphics.HorizontalAlignment.Center,
                PositionUnitType.PixelsFromCenterX);

                SetYValues(
                    global::RenderingLibrary.Graphics.VerticalAlignment.Top,
                    PositionUnitType.PixelsFromTop);

                RefreshAndSave();
            }
        }

        private void TopRightButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (UndoManager.Self.RequestLock())
            {

                SetXValues(
                global::RenderingLibrary.Graphics.HorizontalAlignment.Right,
                PositionUnitType.PixelsFromRight);

                SetYValues(
                    global::RenderingLibrary.Graphics.VerticalAlignment.Top,
                    PositionUnitType.PixelsFromTop);

                RefreshAndSave();
            }
        }

        private void MiddleLeftButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (UndoManager.Self.RequestLock())
            {

                SetXValues(
                global::RenderingLibrary.Graphics.HorizontalAlignment.Left,
                PositionUnitType.PixelsFromLeft);

                SetYValues(
                    global::RenderingLibrary.Graphics.VerticalAlignment.Center,
                    PositionUnitType.PixelsFromCenterY);

                RefreshAndSave();
            }
        }

        private void MiddleMiddleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (UndoManager.Self.RequestLock())
            {
                SetXValues(
                global::RenderingLibrary.Graphics.HorizontalAlignment.Center,
                PositionUnitType.PixelsFromCenterX);

                SetYValues(
                    global::RenderingLibrary.Graphics.VerticalAlignment.Center,
                    PositionUnitType.PixelsFromCenterY);

                RefreshAndSave();
            }
        }

        private void MiddleRightButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (UndoManager.Self.RequestLock())
            {

                SetXValues(
                global::RenderingLibrary.Graphics.HorizontalAlignment.Right,
                PositionUnitType.PixelsFromRight);

                SetYValues(
                    global::RenderingLibrary.Graphics.VerticalAlignment.Center,
                    PositionUnitType.PixelsFromCenterY);

                RefreshAndSave();
            }
        }

        private void BottomLeftButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (UndoManager.Self.RequestLock())
            {

                SetXValues(
                global::RenderingLibrary.Graphics.HorizontalAlignment.Left,
                PositionUnitType.PixelsFromLeft);

                SetYValues(
                    global::RenderingLibrary.Graphics.VerticalAlignment.Bottom,
                    PositionUnitType.PixelsFromBottom);

                RefreshAndSave();
            }
        }

        private void BottomMiddleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (UndoManager.Self.RequestLock())
            {

                SetXValues(
                global::RenderingLibrary.Graphics.HorizontalAlignment.Center,
                PositionUnitType.PixelsFromCenterX);

                SetYValues(
                    global::RenderingLibrary.Graphics.VerticalAlignment.Bottom,
                    PositionUnitType.PixelsFromBottom);

                RefreshAndSave();
            }
        }

        private void BottomRightButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (UndoManager.Self.RequestLock())
            {

                SetXValues(
                global::RenderingLibrary.Graphics.HorizontalAlignment.Right,
                PositionUnitType.PixelsFromRight);

                SetYValues(
                    global::RenderingLibrary.Graphics.VerticalAlignment.Bottom,
                    PositionUnitType.PixelsFromBottom);

                RefreshAndSave();
            }
        }
    }
}
