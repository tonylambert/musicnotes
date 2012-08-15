using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicNotes
{
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
            return new GameConfig
            {
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
}
