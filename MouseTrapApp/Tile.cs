using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseTrapApp
{
    public class Tile
    {
        public int PositionY {  get; set; }
        public int PositionX { get; set; }
        public int TileTypeId { get; set; }
        public int? ItemId { get; set; }
        public int? UserId { get; set; }
    }
}
