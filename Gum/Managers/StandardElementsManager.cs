﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gum.DataTypes;
using Gum.DataTypes.Variables;
using RenderingLibrary.Graphics;

using Microsoft.Xna.Framework;
using Gum.Wireframe;
using Gum.RenderingLibrary;

#if GUM
using Gum.Plugins;
using Gum.PropertyGridHelpers.Converters;
#endif

namespace Gum.Managers
{
    #region Enums

    public enum TextureAddress
    {
        EntireTexture,
        Custom,
        DimensionsBased
    }

    public enum ChildrenLayout
    {
        Regular,
        TopToBottomStack,
        LeftToRightStack

    }

    #endregion

    public class StandardElementsManager
    {
        #region Enums

        enum DimensionVariableAction
        {
            ExcludeFileOptions,
            AllowFileOptions,
            DefaultToPercentageOfFile
        }

        #endregion

        #region Fields

        Dictionary<string, StateSave> mDefaults;

        static StandardElementsManager mSelf;

        #endregion

        #region Properties

        public IEnumerable<string> DefaultTypes
        {
            get
            {
                foreach (var kvp in mDefaults)
                {
                    yield return kvp.Key;
                }
            }
        }

        public static StandardElementsManager Self
        {
            get
            {
                if (mSelf == null)
                {
                    mSelf = new StandardElementsManager();
                }
                return mSelf;
            }
        }

        public string DefaultType
        {
            get
            {
                return "Container";
            }
        }

        public Dictionary<string, StateSave> DefaultStates
        {
            get
            {
                return mDefaults;
            }
        }

        #endregion

        public void Initialize()
        {
            RefreshDefaults();

        }

        public void RefreshDefaults()
        {
            mDefaults = new Dictionary<string, StateSave>();

            // Eventually this would get read from somewhere like an XML file
            // or a CSV file, but for
            // now we'll just use hard values.


            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                                                     Text                                                           //
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var stateSave = new StateSave();
                stateSave.Name = "Default";
                AddPositioningVariables(stateSave);
                AddDimensionsVariables(stateSave, 100, 50, DimensionVariableAction.ExcludeFileOptions);
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "bool", Value = true, Name = "Visible" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "string", Value = "Hello", Name = "Text", Category = "Text" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "HorizontalAlignment", Value = HorizontalAlignment.Left, Name = "HorizontalAlignment", Category = "Text" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "VerticalAlignment", Value = VerticalAlignment.Top, Name = "VerticalAlignment", Category = "Text" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "int?", Value = null, Name = "MaxLettersToShow", Category = "Text" });


                // font:
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "bool", Value = false, Name = "UseCustomFont", Category = "Font" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "string", Value = "Arial", Name = "Font", IsFont = true, Category = "Font" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "int", Value = 18, Name = "FontSize", Category = "Font" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "int", Value = 0, Name = "OutlineThickness", Category = "Font" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "bool", Value = true, Name = "UseFontSmoothing", Category = "Font" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "string", Value = "", Name = "CustomFontFile", Category = "Font", IsFile = true });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "float", Value = 1.0f, Name = "Font Scale", Category = "Font" });

                stateSave.Variables.Add(new VariableSave { Type = "float", Value = 0.0f, Category = "Flip and Rotation", Name = "Rotation" });
                AddEventVariables(stateSave);

                AddStateVariable(stateSave);

                AddColorVariables(stateSave, includeAlpha: true);

#if GUM
                PluginManager.Self.ModifyDefaultStandardState("Text", stateSave);
#endif

                ApplySortValuesFromOrderInState(stateSave);

                mDefaults.Add("Text", stateSave);
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }




            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                                                     Sprite                                                         //
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var stateSave = new StateSave();
                stateSave.Name = "Default";
                AddPositioningVariables(stateSave);
                AddDimensionsVariables(stateSave, 100, 100, DimensionVariableAction.DefaultToPercentageOfFile);
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "string", Value = "", Name = "SourceFile", IsFile = true });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "bool", Value = true, Name = "Visible" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "bool", Value = false, Category = "Animation", Name = "Animate" });

                stateSave.Variables.Add(new VariableSave { Type = "float", Value = 0.0f, Category = "Flip and Rotation", Name = "Rotation" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "bool", Value = false, Category = "Flip and Rotation", Name = "FlipHorizontal" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "bool", Value = false, Category = "Flip and Rotation", Name = "FlipVertical" });


                //stateSave.Variables.Add(new VariableSave { Type = "bool", Value = false, Name = "Custom Texture Coordinates", Category="Source" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "TextureAddress", Value = Gum.Managers.TextureAddress.EntireTexture, Name = "Texture Address", Category = "Source" });

                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "int", Value = 0, Name = "Texture Left", Category = "Source" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "int", Value = 0, Name = "Texture Top", Category = "Source" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "int", Value = 0, Name = "Texture Width", Category = "Source" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "int", Value = 0, Name = "Texture Height", Category = "Source" });

                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "float", Value = 0.0f, Name = "Texture Width Scale", Category = "Source" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "float", Value = 0.0f, Name = "Texture Height Scale", Category = "Source" });

                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "bool", Value = false, Name = "Wrap", Category = "Source" });

                AddColorVariables(stateSave);

                AddEventVariables(stateSave);

                AddStateVariable(stateSave);


                List<string> list = new List<string>();
                stateSave.VariableLists.Add(new VariableListSave<string> { Type = "string", Value = list, Category = "Animation", Name = "AnimationFrames" });
#if GUM
                PluginManager.Self.ModifyDefaultStandardState("Sprite", stateSave);
#endif
                ApplySortValuesFromOrderInState(stateSave);


                mDefaults.Add("Sprite", stateSave);
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }

            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                                                   Container                                                        //
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var stateSave = new StateSave();
                stateSave.Name = "Default";


                AddPositioningVariables(stateSave);

                AddDimensionsVariables(stateSave, 150, 150, DimensionVariableAction.ExcludeFileOptions);

                stateSave.Variables.Add(new VariableSave { SetsValue = true, Category = "Children", Type = "string", Value = null, Name = "Contained Type"
#if GUM
                    , CustomTypeConverter = new AvailableContainedTypeConverter()
#endif
                });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Category = "Children", Type = "ChildrenLayout", Value = ChildrenLayout.Regular, Name = "Children Layout" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Category = "Children", Type = "bool", Value = false, Name = "Wraps Children" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Category = "Children", Type = "bool", Value = false, Name = "Clips Children" });

                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "bool", Value = true, Name = "Visible" });

                stateSave.Variables.Add(new VariableSave { Type = "float", Value = 0.0f, Category = "Flip and Rotation", Name = "Rotation" });

                AddEventVariables(stateSave, defaultHasEvents: true);

#if GUM
                PluginManager.Self.ModifyDefaultStandardState("Container", stateSave);
#endif
                ApplySortValuesFromOrderInState(stateSave);

                mDefaults.Add("Container", stateSave);
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }

            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                                               ColoredRectangle                                                     //
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var stateSave = new StateSave();
                stateSave.Name = "Default";

                AddPositioningVariables(stateSave);

                AddDimensionsVariables(stateSave, 50, 50, DimensionVariableAction.ExcludeFileOptions);

                stateSave.Variables.Add(new VariableSave { Type = "float", Value = 0.0f, Category = "Flip and Rotation", Name = "Rotation" });


                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "bool", Value = true, Name = "Visible" });
                AddColorVariables(stateSave, true);

                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "Blend", Value = Blend.Normal, Name = "Blend", Category = "Rendering" });

                AddEventVariables(stateSave);

                AddStateVariable(stateSave);

#if GUM
                PluginManager.Self.ModifyDefaultStandardState("ColoredRectangle", stateSave);
#endif
                ApplySortValuesFromOrderInState(stateSave);

                mDefaults.Add("ColoredRectangle", stateSave);
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }




            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                                               Circle                                                               //
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var stateSave = new StateSave();
                stateSave.Name = "Default";

                AddPositioningVariables(stateSave);

                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "float", Value = 16.0f, Name = "Radius" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "float", Value = 16.0f, Name = "Width", IsHiddenInPropertyGrid = true });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "float", Value = 16.0f, Name = "Height", IsHiddenInPropertyGrid = true });

                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "bool", Value = true, Name = "Visible" });
                AddColorVariables(stateSave, true);

                // Although rotating a circle about its center does nothing we add rotation because you can rotate it about a different origin
                stateSave.Variables.Add(new VariableSave { Type = "float", Value = 0.0f, Category = "Flip and Rotation", Name = "Rotation" });


                AddEventVariables(stateSave);

                AddStateVariable(stateSave);

#if GUM
                PluginManager.Self.ModifyDefaultStandardState("Circle", stateSave);
#endif
                ApplySortValuesFromOrderInState(stateSave);

                mDefaults.Add("Circle", stateSave);
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }

            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                                               Rectangle                                                            //
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var stateSave = new StateSave();
                stateSave.Name = "Default";

                AddPositioningVariables(stateSave);

                AddDimensionsVariables(stateSave, 16, 16, DimensionVariableAction.ExcludeFileOptions);

                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "bool", Value = true, Name = "Visible" });
                AddColorVariables(stateSave, true);

                stateSave.Variables.Add(new VariableSave { Type = "float", Value = 0.0f, Category = "Flip and Rotation", Name = "Rotation" });


                AddEventVariables(stateSave);

                AddStateVariable(stateSave);

#if GUM
                PluginManager.Self.ModifyDefaultStandardState("Rectangle", stateSave);
#endif
                ApplySortValuesFromOrderInState(stateSave);

                mDefaults.Add("Rectangle", stateSave);
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }

            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                                                     Polygon                                                        //
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var stateSave = new StateSave();
                stateSave.Name = "Default";

                AddPositioningVariables(stateSave, addOriginVariables:false);

                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "bool", Value = true, Name = "Visible" });
                AddColorVariables(stateSave, true);

                stateSave.Variables.Add(new VariableSave { Type = "float", Value = 0.0f, Category = "Flip and Rotation", Name = "Rotation" });

                var pointsVariable = new VariableListSave<Vector2>()
                { Name = "Points", Category = "Points" };

                pointsVariable.Value.Add(new Vector2(-32, -32));
                pointsVariable.Value.Add(new Vector2(32, -32));
                pointsVariable.Value.Add(new Vector2(32, 32));
                pointsVariable.Value.Add(new Vector2(-32, 32));
                // close it:
                pointsVariable.Value.Add(new Vector2(-32, -32));

                stateSave.VariableLists.Add(pointsVariable);

                AddStateVariable(stateSave);

#if GUM
                PluginManager.Self.ModifyDefaultStandardState("Polygon", stateSave);
#endif

                ApplySortValuesFromOrderInState(stateSave);

                mDefaults.Add("Polygon", stateSave);
            }


            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                                                    NineSlice                                                       //
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var stateSave = new StateSave();
                stateSave.Name = "Default";
                AddPositioningVariables(stateSave);
                AddDimensionsVariables(stateSave, 64, 64, DimensionVariableAction.ExcludeFileOptions);
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "string", Value = "", Name = "SourceFile", IsFile = true });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "bool", Value = true, Name = "Visible" });

                AddColorVariables(stateSave);
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "Blend", Value = Blend.Normal, Name = "Blend", Category = "Rendering" });

                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "TextureAddress", Value = Gum.Managers.TextureAddress.EntireTexture, Name = "Texture Address", Category = "Source" });

                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "int", Value = 0, Name = "Texture Left", Category = "Source" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "int", Value = 0, Name = "Texture Top", Category = "Source" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "int", Value = 0, Name = "Texture Width", Category = "Source" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "int", Value = 0, Name = "Texture Height", Category = "Source" });




                AddEventVariables(stateSave);

                AddStateVariable(stateSave);

                stateSave.Variables.Add(new VariableSave { Type = "float", Value = 0.0f, Category = "Flip and Rotation", Name = "Rotation" });


#if GUM

                PluginManager.Self.ModifyDefaultStandardState("NineSlice", stateSave);
#endif
                ApplySortValuesFromOrderInState(stateSave);

                mDefaults.Add("NineSlice", stateSave);
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }

            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                                                     Component                                                      //
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var stateSave = new StateSave();
                stateSave.Name = "Default";
#if GUM
                PluginManager.Self.ModifyDefaultStandardState("Component", stateSave);
#endif

#if GUM
                // Victor Chelaru
                // August 21, 2014
                // Not sure why we have
                // this here.  Doing so would
                // create an endless loop...
                //stateSave.Variables.Add(new VariableSave { Type = "string", Value = "Default", Name = "State", CustomTypeConverter = new AvailableStatesConverter(null)});
                // The type used to be "string" but we want to differentiate it from actual strings so we use "State"
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "State", Value = null, Name = "State", CustomTypeConverter = new AvailableStatesConverter(null) });
#endif

                ApplySortValuesFromOrderInState(stateSave);

                // Not sure if component needs this - does it get values from container?
                //AddEventVariables(stateSave);

                mDefaults.Add("Component", stateSave);
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }


            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                                                    Screen                                                          //
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var stateSave = new StateSave();
                stateSave.Name = "Default";


#if GUM
                PluginManager.Self.ModifyDefaultStandardState("Screen", stateSave);
#endif

                ApplySortValuesFromOrderInState(stateSave);

                mDefaults.Add("Screen", stateSave);
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }

            // We shouldn't do this because states above may explicitly not want to set values - like the variable for state
            //foreach (var defaultState in mDefaults.Values)
            //{
            //    foreach (var variable in defaultState.Variables)
            //    {
            //        variable.SetsValue = true;
            //    }
            //}
        }

        private void AddEventVariables(StateSave stateSave, bool defaultHasEvents = false)
        {
            stateSave.Variables.Add(
                new VariableSave
                { SetsValue = true, Type = "bool", Value = defaultHasEvents, Name = "HasEvents", Category = "Behavior", CanOnlyBeSetInDefaultState = true , ExcludeFromInstances = true});
            stateSave.Variables.Add(
                new VariableSave
                { SetsValue = true, Type = "bool", Value = defaultHasEvents, Name = "ExposeChildrenEvents", Category = "Behavior", CanOnlyBeSetInDefaultState = true, ExcludeFromInstances = true });
        }

        private static void AddStateVariable(StateSave stateSave)
        {
            stateSave.Variables.Add(new VariableSave
            {
                // Don't want it to set the value...
                SetsValue = false, 
                Type = "State",
                Value = "Default",
                Name = "State"
#if GUM
,
                CustomTypeConverter = new AvailableStatesConverter(null)
#endif
            });
        }

        private void ApplySortValuesFromOrderInState(StateSave stateSave)
        {
            for (int i = 0; i < stateSave.Variables.Count; i++)
            {
                stateSave.Variables[i].DesiredOrder = i;
            }
        }

        private static void AddColorVariables(StateSave stateSave, bool includeAlpha = true)
        {
            if (includeAlpha)
            {
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "int", Value = 255, Name = "Alpha", Category = "Rendering" });
            }
            stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "int", Value = 255, Name = "Red", Category = "Rendering" });
            stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "int", Value = 255, Name = "Green", Category = "Rendering" });
            stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "int", Value = 255, Name = "Blue", Category = "Rendering" });
        }

        private static void AddDimensionsVariables(StateSave stateSave, float defaultWidth, float defaultHeight, DimensionVariableAction dimensionVariableAction)
        {
            stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "float", Value = defaultWidth, Name = "Width", Category = "Dimensions" });

            var defaultValue = DimensionUnitType.Absolute;

            if(dimensionVariableAction == DimensionVariableAction.DefaultToPercentageOfFile)
            {
                defaultValue = DimensionUnitType.PercentageOfSourceFile;
            }

            VariableSave variableSave = new VariableSave { SetsValue = true, Type = typeof(DimensionUnitType).Name, Value = defaultValue, Name = "Width Units", Category = "Dimensions" };
            if (dimensionVariableAction == DimensionVariableAction.ExcludeFileOptions)
            {
                variableSave.ExcludedValuesForEnum.Add(DimensionUnitType.PercentageOfSourceFile);
                variableSave.ExcludedValuesForEnum.Add(DimensionUnitType.MaintainFileAspectRatio);
            }
            stateSave.Variables.Add(variableSave);



            stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "float", Value = defaultHeight, Name = "Height", Category = "Dimensions" });

            variableSave = new VariableSave { SetsValue = true, Type = typeof(DimensionUnitType).Name, Value = defaultValue, Name = "Height Units", Category = "Dimensions" };
            if (dimensionVariableAction == DimensionVariableAction.ExcludeFileOptions)
            {
                variableSave.ExcludedValuesForEnum.Add(DimensionUnitType.PercentageOfSourceFile);
                variableSave.ExcludedValuesForEnum.Add(DimensionUnitType.MaintainFileAspectRatio);
            }
            stateSave.Variables.Add(variableSave);
        }

        private static void AddPositioningVariables(StateSave stateSave, bool addOriginVariables = true)
        {
            List<object> xUnitsExclusions = new List<object>();
            xUnitsExclusions.Add(PositionUnitType.PixelsFromTop);
            xUnitsExclusions.Add(PositionUnitType.PercentageHeight);
            xUnitsExclusions.Add(PositionUnitType.PixelsFromBottom);
            xUnitsExclusions.Add(PositionUnitType.PixelsFromCenterY);
            xUnitsExclusions.Add(PositionUnitType.PixelsFromCenterYInverted);

            List<object> yUnitsExclusions = new List<object>();
            yUnitsExclusions.Add(PositionUnitType.PixelsFromLeft);
            yUnitsExclusions.Add(PositionUnitType.PixelsFromCenterX);
            yUnitsExclusions.Add(PositionUnitType.PercentageWidth);
            yUnitsExclusions.Add(PositionUnitType.PixelsFromRight);


            stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "float", Value = 0.0f, Name = "X", Category = "Position" });
            stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = typeof(PositionUnitType).Name, Value = PositionUnitType.PixelsFromLeft, Name = "X Units", Category = "Position", ExcludedValuesForEnum = xUnitsExclusions });

            stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "float", Value = 0.0f, Name = "Y", Category = "Position" });
            stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = typeof(PositionUnitType).Name, Value = PositionUnitType.PixelsFromTop, Name = "Y Units", Category = "Position", ExcludedValuesForEnum = yUnitsExclusions });

            if(addOriginVariables)
            {
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "HorizontalAlignment", Value = HorizontalAlignment.Left, Name = "X Origin", Category = "Position" });
                stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "VerticalAlignment", Value = VerticalAlignment.Top, Name = "Y Origin", Category = "Position" });
            }

            stateSave.Variables.Add(new VariableSave { SetsValue = true, Type = "string", Value = null, Name = "Guide", Category = "Position" });
#if GUM
            AddParentVariables(stateSave);
#endif
        }

#if GUM
        private static void AddParentVariables(StateSave variables)
        {
            VariableSave variableSave = new VariableSave();
            variableSave.SetsValue = true;
            variableSave.Type = "string";
            variableSave.Name = "Parent";
            variableSave.CanOnlyBeSetInDefaultState = true;
            variableSave.CustomTypeConverter = new AvailableInstancesConverter() { IncludeScreenBounds = true };
            variables.Variables.Add(variableSave);
        }
#endif

        public StateSave GetDefaultStateFor(string type)
        {
            if (mDefaults == null)
            {
                throw new Exception("You must first call Initialize on StandardElementsManager before calling this function");
            }
            if(!mDefaults.ContainsKey(type))
            {
                throw new InvalidOperationException($"Could not get the default state for type {type}");
            }
            return mDefaults[type];

        }

        public bool IsDefaultType(string type)
        {
            return mDefaults.ContainsKey(type);
        }

        public void PopulateProjectWithDefaultStandards(GumProjectSave gumProjectSave)
        {
            if (mDefaults == null)
            {
                throw new Exception("You must first call Initialize on this StandardElementsManager");
            }


            foreach (KeyValuePair<string, StateSave> kvp in mDefaults)
            {

                string type = kvp.Key;

                if (type != "Screen")
                {
                    AddStandardElementSaveInstance(gumProjectSave, type);
                }
            }
        }

        public StandardElementSave AddStandardElementSaveInstance(GumProjectSave gumProjectSave, string type)
        {
            StandardElementSave elementSave = new StandardElementSave();
            elementSave.Initialize(mDefaults[type]);
            elementSave.Name = type;

            
            gumProjectSave.StandardElementReferences.Add( new ElementReference { Name = type, ElementType = ElementType.Standard});
            gumProjectSave.StandardElements.Add( elementSave);

            return elementSave;
        }

    }
}
