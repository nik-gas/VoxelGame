using MyTerraria.Item;
using MyTerraria.Tile;
using SFML.Graphics;

namespace MyTerraria
{
    class Lighting
    {
        private int tileLightStep = 5;
        private byte light;
        private TileDrawble tile;
        private TileDrawble tileStep;

        private TileDrawble tileLeft = null;
        private TileDrawble tileRight = null;

        public void UpdateLightTile(TileDrawble tile)
        {
            int X = (int)tile.Position.X / InfoTile.TileSize;
            int Y = (int)tile.Position.Y / InfoTile.TileSize;

            TileDrawble tileUp = tile;

            if(tile.RightTile == null)
                for (int x = 0; x < tileLightStep; x++)
                {
                    tileRight = tile;

                    tileStep = Game.World.GetTile(X - x, Y);

                    if(tileStep != null)
                    {
                        if(tileStep.RightTile == null)
                            tileRight = tileStep;

                        int dist = (int)MathHelper.GetDistance(tileStep.Position, tileRight.Position);
                        int c = byte.MaxValue - (dist / InfoTile.TileSize) * byte.MaxValue / tileLightStep;

                        if(c < 0)
                            c = 0;
                        if(c > byte.MaxValue)
                            c = byte.MaxValue;

                        light = (byte)c;

                        //if(tileStep.Color.R < light)
                            tileStep.Color = new Color(light, light, light);
                    }
                }

            if(tile.LeftTile == null)
                for (int x = 0; x < tileLightStep; x++)
                {
                    tileLeft = tile;

                    tileStep = Game.World.GetTile(X + x, Y);

                    if(tileStep != null)
                    {
                        if(tileStep.LeftTile == null)
                            tileLeft = tileStep;

                        int dist = (int)MathHelper.GetDistance(tileStep.Position, tileLeft.Position);
                        int c = byte.MaxValue - (dist / InfoTile.TileSize) * byte.MaxValue / tileLightStep;

                        if(c < 0)
                            c = 0;
                        if(c > byte.MaxValue)
                            c = byte.MaxValue;

                        light = (byte)c;

                        //if(tileStep.Color.R < light)
                            tileStep.Color = new Color(light, light, light);
                    }
                }

            if(tile.UpTile == null)
                for (int y = 0; y < tileLightStep; y++)
                {
                    tileStep = Game.World.GetTile(X, Y + y);
                    
                    if(tileStep != null)
                    {
                        if(tileStep.LeftTile == null && tileStep.RightTile == null && (tileStep.UpTile == null || tileStep.UpTile != null))
                            tileUp = tileStep;

                        int dist = (int)MathHelper.GetDistance(tileStep.Position, tileUp.Position);
                        int c = byte.MaxValue - (dist / InfoTile.TileSize) * byte.MaxValue / tileLightStep;

                        if(c < 0)
                            c = 0;
                        if(c > byte.MaxValue)
                            c = byte.MaxValue;

                        light = (byte)c;

                        if(tileStep.Color.R < light)
                           tileStep.Color = new Color(light, light, light);
                        if(tileStep.Color.R > light * 1.5f)
                           tileStep.Color = new Color(light, light, light);
                    }
                }
                //if(tileLeft != null)
                    //DebugRender.AddText(Content.font, tileLeft.GetPosition() + new SFML.System.Vector2f(5,0) * 16, (tileLeft.GetPosition().X / 16).ToString());

                if(tileStep != null && Y > tileStep.Position.Y / 16 && tileLeft != null && X >= tileLeft.Position.X / 16 + 4 && tileRight != null && X <= tileRight.Position.X / 16 - 4)
                    tile.Color = Color.Black;

                if(tileStep != null && Y > tileStep.Position.Y / 16 && tileLeft == null )
                    tile.Color = Color.Black;

        }
    }
}