using System.Collections.Generic;

namespace ExamShipBattle.Model
{
    public class ShipStock
    {
        public List<Ship> ShipsModels { get; set; }

        public ShipStock()
        {
            this.ShipsModels = new List<Ship>();
            this.ShipsModels.Add(new Ship(false, 4, 0, 0));
            this.ShipsModels.Add(new Ship(false, 3, 0, 0));
            this.ShipsModels.Add(new Ship(false, 3, 0, 0));
            this.ShipsModels.Add(new Ship(false, 2, 0, 0));
            this.ShipsModels.Add(new Ship(false, 2, 0, 0));
            this.ShipsModels.Add(new Ship(false, 2, 0, 0));
            this.ShipsModels.Add(new Ship(false, 1, 0, 0));
            this.ShipsModels.Add(new Ship(false, 1, 0, 0));
            this.ShipsModels.Add(new Ship(false, 1, 0, 0));
            this.ShipsModels.Add(new Ship(false, 1, 0, 0));
        }
    }
}
