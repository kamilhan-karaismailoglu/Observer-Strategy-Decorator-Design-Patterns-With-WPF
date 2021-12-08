using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CursorType
{
    #region Decorator DP's Classes

    interface IComponent
    {
        public string Operation();
    }

    class Component : IComponent
    {
        public string Operation()
        {
            return "Welcome To Cursor Type Program ,";
        }
    }
    interface IDecorator : IComponent
    {
        public string Operation();
    }

    class DecoratorA : IDecorator
    {
        private readonly IComponent component;

        public DecoratorA(IComponent c)
        {
            component = c;
        }

        public string Operation()
        {
            string s = component.Operation();
            s += "\nYou Can Try Different Cursor Types In This Program.";
            return s;
        }
    }

    class DecoratorB : IDecorator
    {
        private readonly IComponent component;

        public DecoratorB(IComponent c)
        {
            component = c;
        }

        public string Operation()
        {
            string s = component.Operation();
            s += " While Doing This, You Can Also \nManage The Usage Area Of ​​The Cursor You Are Trying.";
            return s;
        }
    }

    #endregion

    #region Observer DP's Classes

    abstract class Subject
    {
        public abstract void Notify();
        public abstract void Attach(Observer observer);
        public abstract void Detach(Observer observer);
    }

    abstract class Observer
    {
        public abstract void Update(string name);
    }

    class CustomerSubject : Subject
    {
        private string name;
        private List<Observer> _observers = new List<Observer>();

        public string Name
        {
            get => name;
            set
            {
                if (value != name)
                {
                    name = value;
                    Notify();
                }
            }
        }

        public List<Observer> Observers { get => _observers; set => _observers = value; }

        public override void Attach(Observer observer)
        {
            Observers.Add(observer);
        }

        public override void Detach(Observer observer)
        {
            Observers.Remove(observer);
        }

        public override void Notify()
        {
            Observers.ForEach(o => { o.Update(this.name); });
        }
    }

    class CustomerObserver : Observer
    {
        private string message;
        public string Message { get => message; set => message = value; }

        public override void Update(string name)
        {
            MessageBox.Show("The Cursor has Changed. New Cursor is " + name + ".", "Info");
            Message = "Cursor is " + name;
        }
    }
    #endregion

    #region Strategy DP's Classes

    class Context
    {
        private bool cursorScopeElementOnly;
        public bool CursorScopeElementOnly { get => cursorScopeElementOnly; set => cursorScopeElementOnly = value; }

        public Context(bool b)
        {
            CursorScopeElementOnly = b;
        }
    }

    abstract class Strategy
    {
        public abstract bool Produce();
    }

    class Strategy1 : Strategy
    {
        private string message;
        public string Message { get => message; set => message = value; }

        public override bool Produce()
        {
            Message = "The new Scope of the cursor is Display Area Only.";
            MessageBox.Show(Message, "The scope of the cursor has changed");
            Context c = new Context(true);
            return c.CursorScopeElementOnly;
        }
    }

    class Strategy2 : Strategy
    {
        private string message;

        public string Message { get => message; set => message = value; }

        public override bool Produce()
        {
            Message = "The new Scope of the cursor is Entire Appliation.";
            MessageBox.Show(Message, "The scope of the cursor has changed");
            Context c = new Context(false);
            return c.CursorScopeElementOnly;
        }
    }

    class Producer
    {
        private bool b;
        public bool B { get => b; set => b = value; }

        public Producer(Strategy strategy)
        {
            B = strategy.Produce();
        }
    }

    #endregion 

    public partial class MainWindow : Window
    {
        private readonly Cursor _customCursor;
        private bool _cursorScopeElementOnly = true;
        readonly CustomerSubject subjectSelectedCursor = new CustomerSubject();
        readonly CustomerObserver customerObserver1 = new CustomerObserver();

        public MainWindow()
        {
            InitializeComponent();
            _customCursor = Cursors.Arrow;

            subjectSelectedCursor.Attach(customerObserver1);
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ((ComboBoxItem)CursorSelector.Items[0]).IsSelected = true;

            TitleLbl.Content = new DecoratorB(new DecoratorA(new Component())).Operation();
        }

        private void CursorTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is ComboBox source)
            {
                ComboBoxItem selectedCursor = source.SelectedItem as ComboBoxItem;

                if (selectedCursor.Content.ToString() != "Select New Cursor")
                {
                    subjectSelectedCursor.Name = selectedCursor.Content.ToString();
                    CursorLbl.Content = customerObserver1.Message;

                    switch (selectedCursor.Content.ToString())
                    {
                        case "AppStarting":
                            DisplayArea.Cursor = Cursors.AppStarting;
                            break;
                        case "ArrowCD":
                            DisplayArea.Cursor = Cursors.ArrowCD;
                            break;
                        case "Arrow":
                            DisplayArea.Cursor = Cursors.Arrow;
                            break;
                        case "Cross":
                            DisplayArea.Cursor = Cursors.Cross;
                            break;
                        case "HandCursor":
                            DisplayArea.Cursor = Cursors.Hand;
                            break;
                        case "Help":
                            DisplayArea.Cursor = Cursors.Help;
                            break;
                        case "IBeam":
                            DisplayArea.Cursor = Cursors.IBeam;
                            break;
                        case "No":
                            DisplayArea.Cursor = Cursors.No;
                            break;
                        case "None":
                            DisplayArea.Cursor = Cursors.None;
                            break;
                        case "Pen":
                            DisplayArea.Cursor = Cursors.Pen;
                            break;
                        case "ScrollSE":
                            DisplayArea.Cursor = Cursors.ScrollSE;
                            break;
                        case "ScrollWE":
                            DisplayArea.Cursor = Cursors.ScrollWE;
                            break;
                        case "SizeAll":
                            DisplayArea.Cursor = Cursors.SizeAll;
                            break;
                        case "SizeNESW":
                            DisplayArea.Cursor = Cursors.SizeNESW;
                            break;
                        case "SizeNS":
                            DisplayArea.Cursor = Cursors.SizeNS;
                            break;
                        case "SizeNWSE":
                            DisplayArea.Cursor = Cursors.SizeNWSE;
                            break;
                        case "SizeWE":
                            DisplayArea.Cursor = Cursors.SizeWE;
                            break;
                        case "UpArrow":
                            DisplayArea.Cursor = Cursors.UpArrow;
                            break;
                        case "WaitCursor":
                            DisplayArea.Cursor = Cursors.Wait;
                            break;
                        case "Custom":
                            DisplayArea.Cursor = _customCursor;
                            break;
                    }

                    if (_cursorScopeElementOnly == false)
                    {
                        Mouse.OverrideCursor = DisplayArea.Cursor;
                    }
                }
            }
            CursorSelector.SelectedIndex = 0;
        }

        int i = 0;
        private void CursorScopeSelected(object sender, RoutedEventArgs e)
        {
            if (e.Source is RadioButton source && i != 0)
            {
                if (source.Name == "rbScopeElement")
                {
                    Producer producer = new Producer(new Strategy1());
                    _cursorScopeElementOnly = producer.B;

                    Mouse.OverrideCursor = null;
                }
                else if (source.Name == "rbScopeApplication")
                {
                    Producer producer = new Producer(new Strategy2());
                    _cursorScopeElementOnly = producer.B;

                    Mouse.OverrideCursor = DisplayArea.Cursor;
                }
            }
            i =1;
        }
    }
}