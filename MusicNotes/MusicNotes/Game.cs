using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicNotes
{
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
