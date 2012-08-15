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
         var userEnteredNoteType = NoteType.C;
         var correctNoteType = NoteType.C;

         Game game = new InstrumentedGame( GameConfig.DefaultGameConfig, currentNoteLocation );
         game.ScoreUserInput( userEnteredNoteType, correctNoteType );

         Assert.AreEqual( 3, game.TotalScore );
      }

      [TestMethod]
      public void WillLosePoints_WhenPressingButtonAtTheWrongTime()
      {
         int currentNoteLocation = 5;
         var correctNoteType = NoteType.C;

         var userEnteredNoteType = NoteType.C;

         Game game = new InstrumentedGame( GameConfig.DefaultGameConfig, currentNoteLocation );
         game.ScoreUserInput( userEnteredNoteType, correctNoteType );

         Assert.AreEqual( -1, game.TotalScore );
      }

      [TestMethod]
      public void WhenPressingButtonAtTheWrongTime_WillReturnWrongTimeResult()
      {
         int currentNoteLocation = 4;
         var correctNoteType = NoteType.C;

         var userEnteredNoteType = NoteType.C;

         Game game = new InstrumentedGame( GameConfig.DefaultGameConfig, currentNoteLocation );
         ScoreResult result = game.ScoreUserInput( userEnteredNoteType, correctNoteType );

         Assert.AreEqual( ScoreResult.WrongTime, result );
      }

      [TestMethod]
      public void WillLosePoints_WhenPressingWrongButtonAtRightTime()
      {
         int currentNoteLocation = 10;
         var correctNoteType = NoteType.C;

         var userEnteredNoteType = NoteType.D;

         Game game = new InstrumentedGame( GameConfig.DefaultGameConfig, currentNoteLocation );
         game.ScoreUserInput( userEnteredNoteType, correctNoteType );

         Assert.AreEqual( -2, game.TotalScore );
      }

      [TestMethod]
      public void WhenPressingWrongButton_WillReturnWrongNoteResult()
      {
         int currentNoteLocation = 10;
         var correctNoteType = NoteType.C;

         var userEnteredNoteType = NoteType.D;

         Game game = new InstrumentedGame( GameConfig.DefaultGameConfig, currentNoteLocation );
         ScoreResult result = game.ScoreUserInput( userEnteredNoteType, correctNoteType );

         Assert.AreEqual( ScoreResult.WrongNote, result );
      }

      [TestMethod]
      public void WillExtraLoseExtraPoints_WhenPressingWrongButtonAtWrongTime()
      {
         int currentNoteLocation = 4;
         var correctNoteType = NoteType.C;

         var userEnteredNoteType = NoteType.D;

         Game game = new InstrumentedGame( GameConfig.DefaultGameConfig, currentNoteLocation );
         game.ScoreUserInput( userEnteredNoteType, correctNoteType );

         Assert.AreEqual( -3, game.TotalScore );
      }

      [TestMethod]
      public void WhenPressingWrongButtonAtWrongTime_WillReturnWrongTimeAndNoteResult()
      {
         int currentNoteLocation = 4;
         var correctNoteType = NoteType.C;

         var userEnteredNoteType = NoteType.D;

         Game game = new InstrumentedGame( GameConfig.DefaultGameConfig, currentNoteLocation );
         ScoreResult result = game.ScoreUserInput( userEnteredNoteType, correctNoteType );

         Assert.AreEqual( ScoreResult.WrongTimeAndNote, result );
      }

      [TestMethod]
      public void WillHaveNegativeTotalScore_WhenPressingWrongButtonAtWrongTimeManyTimes()
      {
         int currentNoteLocation = 4;
         var correctNoteType = NoteType.C;

         var userEnteredNote = NoteType.D;

         Game game = new InstrumentedGame( GameConfig.DefaultGameConfig, currentNoteLocation );

         game.ScoreUserInput( userEnteredNote, correctNoteType );
         game.ScoreUserInput( userEnteredNote, correctNoteType );
         game.ScoreUserInput( userEnteredNote, correctNoteType );

         Assert.AreEqual( -9, game.TotalScore );
      }

      [TestMethod]
      public void WillHaveProperTotalScore_WhenPressingGettingNoteRightAtRightTimeMultipleTimes()
      {
         int currentNoteLocation = 10;
         var correctNoteType = NoteType.C;

         var userEnteredNoteType = NoteType.C;

         Game game = new InstrumentedGame( GameConfig.DefaultGameConfig, currentNoteLocation );

         game.ScoreUserInput( userEnteredNoteType, correctNoteType );
         game.ScoreUserInput( userEnteredNoteType, correctNoteType );
         game.ScoreUserInput( userEnteredNoteType, correctNoteType );

         Assert.AreEqual( 9, game.TotalScore );
      }

      [TestMethod]
      public void NoteWillMoveLocation_WhenTimeElapses()
      {
         int currentNoteLocation = 5;

         TimeSpan elapsedTime = TimeSpan.FromSeconds( 3 );

         Game game = new InstrumentedGame( GameConfig.DefaultGameConfig, currentNoteLocation );
         currentNoteLocation = game.MoveNote( elapsedTime );

         Assert.AreEqual( 14, currentNoteLocation );
      }
   }

   public struct GameConfig
   {
      public int SpeedOfNote;
      public int PointsLostWhenWrongNote;
      public int PointsLostWhenWrongTime;
      public int PointsLostWhenWrongNoteAndTime;
      public int PointsGainedWhenCorrect;
      public int PositionOfTargetArea;

      public static GameConfig DefaultGameConfig
      {
         get
         {
            return new GameConfig {
               SpeedOfNote = 3,
               PointsGainedWhenCorrect = 3,
               PointsLostWhenWrongNote = -2,
               PointsLostWhenWrongNoteAndTime = -3,
               PointsLostWhenWrongTime = -1,
               PositionOfTargetArea = 10,
            };
         }
      }
   }

   public class Game
   {
      protected int _currentNoteLocation = 0;
      protected int _totalScore = 0;
      protected GameConfig _gameConfig;

      public Game( GameConfig gameConfig )
      {
         _gameConfig = gameConfig;
      }

      public int TotalScore
      {
         get
         {
            return _totalScore;
         }
      }

      private int GetSpeedOfNote()
      {
         return _gameConfig.SpeedOfNote;
      }

      public int MoveNote( TimeSpan elapsedTime )
      {
         _currentNoteLocation += (int) (elapsedTime.TotalSeconds * GetSpeedOfNote());
         return _currentNoteLocation;
      }

      public ScoreResult ScoreUserInput( NoteType userEnteredNote, NoteType noteType )
      {
         int pointsScored = _gameConfig.PointsGainedWhenCorrect;
         ScoreResult result = ScoreResult.Correct;

         if ( userEnteredNote != noteType && _currentNoteLocation != _gameConfig.PositionOfTargetArea )
         {
            pointsScored = _gameConfig.PointsLostWhenWrongNoteAndTime;
            result = ScoreResult.WrongTimeAndNote;
         }
         else if ( userEnteredNote != noteType )
         {
            pointsScored = _gameConfig.PointsLostWhenWrongNote;
            result = ScoreResult.WrongNote;
         }
         else if ( _currentNoteLocation != _gameConfig.PositionOfTargetArea )
         {
            pointsScored = _gameConfig.PointsLostWhenWrongTime;
            result = ScoreResult.WrongTime;
         }

         _totalScore += pointsScored;
         return result;
      }
   }
}
