using Microsoft.Phone.Shell;
using System.Linq;

namespace VitaminD.Helper
{
   public class TileHelper
    {
       public TileHelper()
       {

       }


       public void ClearTiles()
       {
           ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault();
           var data = new StandardTileData();
           data.Count = 0;
           data.BackContent = string.Empty;
           tile.Update(data);
       }
    }
}
