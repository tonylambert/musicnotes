using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicNotes.UnitTests
{
   public class InstrumentedGame : Game
   {
      public InstrumentedGame( int currentNoteLocation, int speedOfNote )
      {
         _speedOfNote = speedOfNote;
         _currentNoteLocation = currentNoteLocation;
      }

      public InstrumentedGame( int currentNoteLocation )
      {
         _currentNoteLocation = currentNoteLocation;
      }
   }
}
