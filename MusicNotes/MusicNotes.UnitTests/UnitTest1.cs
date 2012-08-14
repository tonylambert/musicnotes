using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace MusicNotes.UnitTests
{
   [TestClass]
   public class GameTests
   {
      [TestMethod]
      public void ScorePointsForPressingButtonAtTheRightTime()
      {
         int currentNoteLocation = 10;
         var userEnteredNote = NoteType.C;

         int scorerXPosition = 10;
         var noteType = NoteType.C;

         int pointsScored = ScoreUserInput( userEnteredNote, currentNoteLocation, scorerXPosition, noteType );

         Assert.AreEqual( 1, pointsScored );
      }

      private static int ScoreUserInput( NoteType userEnteredNote, int currentNoteLocation, int scorerXPosition, NoteType noteType )
      {

         int pointsScored = 0;

         if ( userEnteredNote == noteType && currentNoteLocation == scorerXPosition )
         {
            pointsScored = 1;
         }
         else
         {
            pointsScored = -1;
         }

         return pointsScored;
      }

      [TestMethod]
      public void LosePointsForPressingButtonAtTheWrongTime()
      {
         int currentNoteLocation = 10;
         int scorerXPosition = 12;
         var noteType = NoteType.C;

         var userEnteredNote = NoteType.C;

         int pointsScored = ScoreUserInput( userEnteredNote, currentNoteLocation, scorerXPosition, noteType );

         Assert.AreEqual( -1, pointsScored );
      }
   }
}
