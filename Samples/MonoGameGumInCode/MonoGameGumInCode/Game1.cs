﻿using Gum.Converters;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum.GueDeriving;
using RenderingLibrary;
using RenderingLibrary.Graphics;
using System;

namespace MonoGameGumInCode
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            SystemManagers.Default = new SystemManagers();
            SystemManagers.Default.Initialize(_graphics.GraphicsDevice, fullInstantiation: true);


            // uncomment one of these to create a layout. Only have one uncommented or else UI overlaps
            CreateMixedLayout();
            //CreateTextLayout();
            //CreateInvisibleLayout();

            base.Initialize();
        }

        private void CreateInvisibleLayout()
        {
            GraphicalUiElement.CanvasWidth = 800;
            GraphicalUiElement.CanvasHeight = 600;

            GraphicalUiElement parentContainer = new(new InvisibleRenderable(), null!)
            {
                X = 5,
                Y = 5,
                Width = 40,
                Height = 0,

                HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToChildren,
                ChildrenLayout = Gum.Managers.ChildrenLayout.LeftToRightStack,
                WrapsChildren = true
            };

            for (int i = 0; i < 10; i++)
            {
                GraphicalUiElement buttonWrapper = new(new InvisibleRenderable(), null!)
                {
                    WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute,
                    Width = 20,
                    XOrigin = RenderingLibrary.Graphics.HorizontalAlignment.Left,
                    XUnits = Gum.Converters.GeneralUnitType.PixelsFromSmall,
                    X = 0,

                    HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute,
                    Height = 1
                };

                parentContainer.Children.Add(buttonWrapper);
            }

            parentContainer.UpdateLayout();
        }

        private void CreateTextLayout()
        {
            var container = new ContainerRuntime();
            container.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            container.HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            // Give it 2 pixels on each side so text doesn't bump up against the edge of the screen
            container.X = 2;
            container.Y = 2;
            container.Width = -4;
            container.Height = -4;
            container.ChildrenLayout = Gum.Managers.ChildrenLayout.TopToBottomStack;
            container.StackSpacing = 4;
            container.AddToManagers();

            var textRuntime = new TextRuntime();
            textRuntime.Text = "Hi, I'm default text";
            container.Children.Add(textRuntime);

            var withOutline = new TextRuntime();
            withOutline.Text = "I am text that has an outline.";
            (withOutline.Component as RenderingLibrary.Graphics.Text).RenderBoundary = true;
            container.Children.Add(withOutline);
        }

        private static void CreateMixedLayout()
        {
            var container = new ContainerRuntime();
            container.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            container.HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            container.Width = 0;
            container.Height = 0;
            container.ChildrenLayout = Gum.Managers.ChildrenLayout.TopToBottomStack;
            container.AddToManagers();

            AddText(container, "This is a colored rectangle:");

            var coloredRectangleInstance = new ColoredRectangleRuntime();
            coloredRectangleInstance.X = 10;
            coloredRectangleInstance.Y = 10;
            coloredRectangleInstance.Width = 120;
            coloredRectangleInstance.Height = 24;
            coloredRectangleInstance.Color = Color.White;
            container.Children.Add(coloredRectangleInstance);

            AddText(container, "This is a (line) rectangle:");

            var lineRectangle = new RectangleRuntime();
            lineRectangle.X = 10;
            lineRectangle.Y = 10;
            lineRectangle.Width = 120;
            lineRectangle.Height = 24;
            lineRectangle.Color = Color.Pink;
            container.Children.Add(lineRectangle);


            AddText(container, "This is a sprite:");

            var sprite = new SpriteRuntime();
            sprite.X = 10;
            sprite.Y = 10;
            sprite.SourceFileName = "BearTexture.png";
            container.Children.Add(sprite);

            AddText(container, "This is a NineSlice:");
            var nineSlice = new NineSliceRuntime();
            nineSlice.X = 10;
            nineSlice.Y = 10;
            nineSlice.SourceFileName = "Frame.png";
            nineSlice.Width = 256;
            nineSlice.Height = 48;
            container.Children.Add(nineSlice);

            var customText = new TextRuntime();
            customText.Width = 300;
            customText.UseCustomFont = true;
            customText.CustomFontFile = "WhitePeaberryOutline/WhitePeaberryOutline.fnt";
            customText.Text = "Hello, I am using a custom font.\nPretty cool huh?";
            container.Children.Add(customText);

            AddText(container, "This is a polygon:");
            var polygon = new PolygonRuntime();
            polygon.Name = "PolygonRuntime";
            polygon.X = 10;
            polygon.Y = 10;
            polygon.Color = Color.Red;

            var size = 30;

            polygon.Width = size;
            polygon.Height = size;
            polygon.SetPoints(new System.Numerics.Vector2[]
            {
                new System.Numerics.Vector2(0, 0),
                new System.Numerics.Vector2(0, size),
                new System.Numerics.Vector2(size, size),
                new System.Numerics.Vector2(size, 0),
                new System.Numerics.Vector2(0, 0),
            });
            container.Children.Add(polygon);

        }

        private static void AddText(ContainerRuntime container, string text)
        {
            var textInstance = new TextRuntime();

            // adds a gap between this text and the item above
            textInstance.Y = 8;

            textInstance.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToChildren;
            textInstance.Width = 0;

            textInstance.HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToChildren;

            // Makes it so the item below appears closer to the text:
            textInstance.Height = -8;

            textInstance.Text = text;
            container.Children.Add(textInstance);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);

            bool moveCameraWithMouse = false;
            if(moveCameraWithMouse)
            {
                MoveCameraWithMouse();

            }

            base.Update(gameTime);
        }

        private static void MoveCameraWithMouse()
        {
            var camera = SystemManagers.Default.Renderer.Camera;
            var mouseState = Mouse.GetState();
            camera.X = mouseState.X;
            camera.Y = mouseState.Y;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SystemManagers.Default.Draw();


            base.Draw(gameTime);
        }
    }
}
