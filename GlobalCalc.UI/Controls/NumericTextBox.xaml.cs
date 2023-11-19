using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GlobalCalc.UI.Controls;

public partial class NumericTextBox : UserControl
{
    public NumericTextBox()
    {
        InitializeComponent();
        plusButton.Click += (_, __) => Value++;
        minusButton.Click += (_, __) => Value--;

        textBox.Text = "0";
        textBox.LostFocus += TextBoxOnLostFocus;
        textBox.PreviewTextInput += TextBoxOnPreviewTextInput;
    }

    private void TextBoxOnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (e.Text.Any(ch => ch is < '0' or > '9' && ch != '-')) e.Handled = true;
    }

    private void TextBoxOnLostFocus(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(textBox.Text, out var result))
        {
            textBox.Text = Value.ToString();
            return;
        }

        if (result == Value)
            return;
        
        Value = result;
        textBox.Text = Value.ToString();
    }

    #region DependencyProperties

    public static readonly DependencyProperty ValueProperty = 
        DependencyProperty.Register(
            nameof(Value), 
            typeof(int), 
            typeof(NumericTextBox),
            new FrameworkPropertyMetadata(
                defaultValue: 0,
                propertyChangedCallback: ValueChangedCallback,
                coerceValueCallback: CoerceValueCallback));

    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    
    private static object CoerceValueCallback(DependencyObject d, object baseValue)
    {
        var control = (NumericTextBox)d;
        var value = (int)baseValue;
        var newValue = value;
        newValue = newValue < control.MinValue ? control.MinValue : newValue;
        newValue = newValue > control.MaxValue ? control.MaxValue : newValue;
        var binding = control.GetBindingExpression(ValueProperty);
        if (newValue != value && binding != null)
        {
            var bindingSource = binding.ResolvedSource;
            bindingSource.GetType()
                .GetProperty(binding.ResolvedSourcePropertyName)?
                .SetValue(bindingSource, newValue);
        }

        return newValue;
    }

    private static void ValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as NumericTextBox)?.OnValueChanged();
    }
    
    
    public static readonly DependencyProperty MinValueProperty = 
        DependencyProperty.Register(
            nameof(MinValue), 
            typeof(int), 
            typeof(NumericTextBox),
            new FrameworkPropertyMetadata(
                defaultValue: int.MinValue,
                propertyChangedCallback: MinValueChangedCallback));

    public int MinValue
    {
        get => (int)GetValue(MinValueProperty);
        set => SetValue(MinValueProperty, value);
    }

    private static void MinValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        d.CoerceValue(ValueProperty);
    }
    
    
    public static readonly DependencyProperty MaxValueProperty = 
        DependencyProperty.Register(
            nameof(MaxValue), 
            typeof(int), 
            typeof(NumericTextBox),
            new FrameworkPropertyMetadata(
                defaultValue: int.MaxValue,
                propertyChangedCallback: MaxValueChangedCallback));

    public int MaxValue
    {
        get => (int)GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }

    private static void MaxValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        d.CoerceValue(ValueProperty);
    }

    #endregion

    #region RoutedEvents

    public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged",
        RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(NumericTextBox));
    
    public event RoutedEventHandler ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    protected virtual void OnValueChanged()
    {
        textBox.Text = Value.ToString();
        
        RaiseEvent(new RoutedEventArgs(ValueChangedEvent, this));
    }

    #endregion
}