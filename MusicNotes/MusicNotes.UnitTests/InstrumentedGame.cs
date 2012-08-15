using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicNotes.UnitTests
{
   public class InstrumentedGame : Game
   {
      public InstrumentedGame( GameConfig gameConfig, int currentNoteLocation ) : base( gameConfig )
      {
         _currentNoteLocation = currentNoteLocation;
      }
   }
}
