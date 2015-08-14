using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

using Preference.Engine;
using Preference.Engine.Annotations;
using Preference.Engine.Rules;

namespace Preference.TestUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();

            mTimer.Interval = TimeSpan.FromSeconds(1d);
            mTimer.Tick += (sender, args) =>
            {
                ElapsedTime++;
            };

            DataContext = this;

            mGame = new Game(new SochiRules());
            mGame.Initialize();

            mGame.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "ActiveHand")
                {
                    mTimer.Stop();
                    ElapsedTime = 0;
                    mTimer.Start();
                }
            };

            mGame.RoundStarted += (sender, args) =>
            {
                mTimer.Stop();
            };
            
            mGame.RoundCompleted += (sender, args) =>
            {
                mTimer.Stop();
                mWaitHandle.WaitOne();
            };

            Hand1Cards.DataContext = mGame.Hands[0];
            Hand2Cards.DataContext = mGame.Hands[1];
            Hand3Cards.DataContext = mGame.Hands[2];            
            
            PlayedCard1.DataContext = mGame.Hands[0];
            PlayedCard2.DataContext = mGame.Hands[1];
            PlayedCard3.DataContext = mGame.Hands[2];          
            
            Tricks1.DataContext = mGame.Hands[0];
            Tricks2.DataContext = mGame.Hands[1];
            Tricks3.DataContext = mGame.Hands[2];           
            
            Timer1.DataContext = mGame.Hands[0];
            Timer2.DataContext = mGame.Hands[1];
            Timer3.DataContext = mGame.Hands[2];
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() => mGame.Run());
        }

        private void MyWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                mWaitHandle.Set();
        }

        private void MyWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mWaitHandle.Set();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public int ElapsedTime
        {
            get { return mElapsedTime; }
            set
            {
                mElapsedTime = value;
                OnPropertyChanged("ElapsedTime");
            }
        }

        private readonly Game mGame;
        private readonly AutoResetEvent mWaitHandle = new AutoResetEvent(false);
        private readonly DispatcherTimer mTimer = new DispatcherTimer();
        private int mElapsedTime;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
