using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicNotes
{
   /// <summary>
   /// An empty page that can be used on its own or navigated to within a Frame.
   /// </summary>
   public sealed partial class MainPage : Page
   {
      public MainPage()
      {
         this.InitializeComponent();
         DataContext = new Note( 600 );
      }

      /// <summary>
      /// Invoked when this page is about to be displayed in a Frame.
      /// </summary>
      /// <param name="e">Event data that describes how this page was reached.  The Parameter
      /// property is typically used to configure the page.</param>
      protected override void OnNavigatedTo( NavigationEventArgs e )
      {
      }
   }

   /*
   public class Game
   {
      public Note Note { get; set; }
      private DispatcherTimer _timer;
      private TimeSpan _speedOfAnimation = TimeSpan.FromMilliseconds( 333 );

      public Game( int boardWidth )
      {
         Note = new Note( 500 ) { XPosition = (int) boardWidth, YPosition = 0 };
         _timer = new DispatcherTimer();
         _timer.Interval = _speedOfAnimation;
         _timer.Tick += new EventHandler<object>( timer_Tick );
         _timer.Start();
      }

      private void timer_Tick( object sender, object e )
      {
         Note.XPosition -= 1;
      }
   }
   */

   public class Note : INotifyPropertyChanged
   {
      public int YPosition { get; set; }
      private TimeSpan _speedOfAnimation = TimeSpan.FromMilliseconds( 333 );
      private DispatcherTimer _timer;

      private int _xPosition;
      public int XPosition
      {
         get
         {
            return _xPosition; 
         }
         set
         {
            if ( value != _xPosition )
            {
               _xPosition = value;
               OnPropertyChanged( "XPosition" );
            }
         }
      }

      public Note( int boardWidth )
      {
         XPosition = boardWidth;
         _timer = new DispatcherTimer();
         _timer.Interval = _speedOfAnimation;
         _timer.Tick += new EventHandler<object>( timer_Tick );
         _timer.Start();
      }

      private void timer_Tick( object sender, object e )
      {
         XPosition -= 1;
      }

      public virtual void RaisePropertyChanged( string propertyName )
      {
         OnPropertyChanged( propertyName );
      }

      protected void OnPropertyChanged( string name )
      {
         PropertyChangedEventHandler handler = PropertyChanged;
         if ( handler != null )
         {
            handler( this, new PropertyChangedEventArgs( name ) );
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
