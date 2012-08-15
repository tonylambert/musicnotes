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
      public void WillScorePoints_WhenPressingButtonAtTheRightTime()
      {
         int currentNoteLocation = 10;
         var userEnteredNote = NoteType.C;
         int scorerXPosition = 10;
         var noteType = NoteType.C;

         Game game = new InstrumentedGame( currentNoteLocation );
         int pointsScored = game.ScoreUserInput( userEnteredNote, scorerXPosition, noteType );

         Assert.AreEqual( 1, pointsScored );
      }

      [TestMethod]
      public void WillLosePoints_WhenPressingButtonAtTheWrongTime()
      {
         int currentNoteLocation = 10;
         int scorerXPosition = 12;
         var noteType = NoteType.C;

         var userEnteredNote = NoteType.C;

         Game game = new InstrumentedGame( currentNoteLocation );
         int pointsScored = game.ScoreUserInput( userEnteredNote, scorerXPosition, noteType );

         Assert.AreEqual( -1, pointsScored );
      }

      [TestMethod]
      public void WillLosePoints_WhenPressingWrongButtonAtRightTime()
      {
         int currentNoteLocation = 10;
         int scorerXPosition = 10;
         var noteType = NoteType.C;

         var userEnteredNote = NoteType.D;

         Game game = new InstrumentedGame( currentNoteLocation );
         int pointsScored = game.ScoreUserInput( userEnteredNote, scorerXPosition, noteType );

         Assert.AreEqual( -1, pointsScored );
      }

      [TestMethod]
      public void WillExtraLoseExtraPoints_WhenPressingWrongButtonAtWrongTime()
      {
         int currentNoteLocation = 10;
         int scorerXPosition = 5;
         var noteType = NoteType.C;

         var userEnteredNote = NoteType.D;

         Game game = new InstrumentedGame( currentNoteLocation );
         int pointsScored = game.ScoreUserInput( userEnteredNote, scorerXPosition, noteType );

         Assert.AreEqual( -3, pointsScored );
      }

      [TestMethod]
      public void NoteWillMoveLocation_WhenTimeElapses()
      {
         int currentNoteLocation = 5;
         int speedOfNote = 3; // pixels/second

         TimeSpan elapsedTime = TimeSpan.FromSeconds( 3 );

         Game game = new InstrumentedGame( currentNoteLocation, speedOfNote );
         currentNoteLocation = game.MoveNote( elapsedTime );

         Assert.AreEqual( 14, currentNoteLocation );
      }
   }

   public class Game
   {
      protected int _currentNoteLocation = 0;
      protected int _speedOfNote = 3; // pixels/second

      private int GetSpeedOfNote()
      {
         return _speedOfNote;
      }

      public int MoveNote( TimeSpan elapsedTime )
      {
         _currentNoteLocation += (int) (elapsedTime.TotalSeconds * GetSpeedOfNote());
         return _currentNoteLocation;
      }

      public int ScoreUserInput( NoteType userEnteredNote, int scorerXPosition, NoteType noteType )
      {
         return ScoreUserInput( userEnteredNote, _currentNoteLocation, scorerXPosition, noteType );
      }

      public static int ScoreUserInput( NoteType userEnteredNote, int currentNoteLocation, int scorerXPosition, NoteType noteType )
      {
         int pointsScored = (userEnteredNote == noteType && currentNoteLocation == scorerXPosition) ? 1 : -1;
         if ( userEnteredNote != noteType && currentNoteLocation != scorerXPosition )
         {
            pointsScored = -3;
         }

         return pointsScored;
      }
   }
}

