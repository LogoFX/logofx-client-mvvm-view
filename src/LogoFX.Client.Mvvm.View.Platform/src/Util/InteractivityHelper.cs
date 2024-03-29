﻿/*                          Sample Usage
 *      
        <TextBlock Grid.Column="1" x:Name="_tblock" Text="Default"></TextBlock>
        <ListBox ItemsSource="{Binding DataSource}" Grid.Column="0">
            <ListBox.ItemContainerStyle>                
                <Style TargetType="ListBoxItem">
                    <Setter Property="Interactivity:InteractivityItems.Template">
                        <Setter.Value>
                            <Interactivity:InteractivityTemplate>
                                <Interactivity:InteractivityItems>
                                    <Interactivity:InteractivityItems.Behaviors>
                                        <Utils:FlipOnHover></Utils:FlipOnHover>
                                    </Interactivity:InteractivityItems.Behaviors>
                                    <Interactivity:InteractivityItems.Triggers>
                                        <Interactivity1:EventTrigger EventName="MouseMove">
                                            <Interactivity2:TargetedSetPropertyAction PropertyName="Text" TargetObject="{Binding ElementName=_tblock}" Value="{Binding}">
                                            </Interactivity2:TargetedSetPropertyAction>
                                        </Interactivity1:EventTrigger>
                                    </Interactivity:InteractivityItems.Triggers>
                                </Interactivity:InteractivityItems>
                            </Interactivity:InteractivityTemplate>
                        </Setter.Value>
                    </Setter>
                </Style>
            </ListBox.ItemContainerStyle>
        </ListBox>
 * 
 */
using System.Collections.Generic;
#if WINDOWS_UWP || NETFX_CORE
using Windows.UI.Interactivity;
using Windows.UI.Xaml;
using TriggerBase = Windows.UI.Interactivity.TriggerBase;
using TriggerCollection = Windows.UI.Interactivity.TriggerCollection;
#endif

namespace System.Windows.Interactivity
{
    /// <summary>
    /// Holder for interactivity items
    /// </summary>
    public class InteractivityItems : FrameworkElement
    {
        private List<Behavior> _behaviors;
        private List<TriggerBase> _triggers;

        /// <summary>
        /// Storage for triggers
        /// </summary>
        public new List<TriggerBase> Triggers
        {
            get { return _triggers ?? (_triggers = new List<TriggerBase>()); }
        }

        /// <summary>
        /// Storage for Behaviors
        /// </summary>
        public List<Behavior> Behaviors
        {
            get { return _behaviors ?? (_behaviors = new List<Behavior>()); }
        }

        #region Template attached property

        /// <summary>
        /// Gets the interactivity template.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static InteractivityTemplate GetTemplate(DependencyObject obj)
        {
            return (InteractivityTemplate)obj.GetValue(TemplateProperty);
        }

        /// <summary>
        /// Sets the interactivity template.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetTemplate(DependencyObject obj, InteractivityTemplate value)
        {
            obj.SetValue(TemplateProperty, value);
        }

        /// <summary>
        /// Defines the interactivity template property.
        /// </summary>
        public static readonly DependencyProperty TemplateProperty =
            DependencyProperty.RegisterAttached("Template", typeof(InteractivityTemplate), typeof(InteractivityItems),
                                                new PropertyMetadata(default(InteractivityTemplate), OnTemplateChanged));

        private static void OnTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject target = d;
            if (e.OldValue != null)
            {
                InteractivityTemplate interactivityTemplate = (InteractivityTemplate)e.OldValue;
                InteractivityItems interactivityItems = (InteractivityItems)interactivityTemplate.LoadContent();
                BehaviorCollection behaviorCollection = Interaction.GetBehaviors(target);
                TriggerCollection triggerCollection = Interaction.GetTriggers(target);

                foreach (Behavior behavior in interactivityItems.Behaviors)
                    behaviorCollection.Remove(behavior);

                foreach (TriggerBase trigger in interactivityItems.Triggers)
                    triggerCollection.Remove(trigger);
            }

            if (e.NewValue != null)
            {
                InteractivityTemplate interactivityTemplate = (InteractivityTemplate)e.NewValue;
#if NET || NETCORE || NETFRAMEWORK
                interactivityTemplate.Seal();
#endif
                InteractivityItems interactivityItems = (InteractivityItems)interactivityTemplate.LoadContent();
                BehaviorCollection behaviorCollection = Interaction.GetBehaviors(target);
                TriggerCollection triggerCollection = Interaction.GetTriggers(target);

                foreach (Behavior behavior in interactivityItems.Behaviors)
                    behaviorCollection.Add(behavior);

                foreach (TriggerBase trigger in interactivityItems.Triggers)
                    triggerCollection.Add(trigger);
            }
        }

        #endregion
    }
}
