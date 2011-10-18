using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace UriAgassi.Isobars.Algo
{
    public class IsobarPoint
    {
        public Isobar Parent { get; set; }

        public Point Location { get; set; }

        public int Value { get; set; }

        public Coordinate Coordinate { get; set; }

        public IsobarDirection Direction { get; set; }

        public IsobarPoint FindNext(IEnumerable<IsobarPoint>[][] vgrid, IEnumerable<IsobarPoint>[][] hgrid)
        {
            IEnumerable<IsobarPoint> candidates = null;
            switch (Direction)
            {
                case IsobarDirection.East:
                    if (Coordinate.Y == vgrid[0].Length - 1)
                    {
                        return null;
                    }
                    candidates = (vgrid[Coordinate.X][Coordinate.Y + 1]).Where(x => x.Direction == IsobarDirection.East);
                    if (Coordinate.Y < hgrid[Coordinate.X].Length)
                    {
                        if (Coordinate.X < hgrid.Length)
                        {
                            candidates = candidates
                                .Concat(
                                    (hgrid[Coordinate.X + 1][Coordinate.Y]).Where(
                                        x => x.Direction == IsobarDirection.South));
                        }

                        candidates =
                            candidates.Concat(
                                (hgrid[Coordinate.X][Coordinate.Y].Where(x => x.Direction == IsobarDirection.North)));
                    }
                    break;
                case IsobarDirection.West:
                    if (Coordinate.Y == 0)
                    {
                        return null;
                    }
                    candidates = (vgrid[Coordinate.X][Coordinate.Y - 1]).Where(x => x.Direction == IsobarDirection.West);
                    if (Coordinate.X < hgrid.Length)
                    {
                        candidates = candidates
                            .Concat((hgrid[Coordinate.X + 1][Coordinate.Y - 1]).Where(x => x.Direction == IsobarDirection.South));
                    }
                    candidates =
                        candidates.Concat(
                            (hgrid[Coordinate.X][Coordinate.Y - 1].Where(x => x.Direction == IsobarDirection.North)));
                    break;
                case IsobarDirection.North:
                    if (Coordinate.X == 0)
                    {
                        return null;
                    }
                    candidates = hgrid[Coordinate.X - 1][Coordinate.Y].Where(x => x.Direction == IsobarDirection.North);
                    if (Coordinate.X > 0)
                    {
                        candidates =
                            candidates.Concat(
                                (vgrid[Coordinate.X - 1][Coordinate.Y].Where(
                                    x => x.Direction == IsobarDirection.West)));
                        if (Coordinate.Y < vgrid[Coordinate.X - 1].Length + 1)
                        {
                            candidates =
                                candidates.Concat((vgrid[Coordinate.X - 1][Coordinate.Y + 1]).Where(
                                    x => x.Direction == IsobarDirection.East));

                        }
                        
                    }
                    break;
                case IsobarDirection.South:
                    if (Coordinate.X == hgrid.Length - 1)
                    {
                        return null;
                    }
                    candidates = hgrid[Coordinate.X + 1][Coordinate.Y].Where(x => x.Direction == IsobarDirection.South);
                    if (Coordinate.X < vgrid.Length)
                    {
                        candidates =
                            candidates.Concat((vgrid[Coordinate.X][Coordinate.Y + 1]).Where(
                                x => x.Direction == IsobarDirection.East));

                        candidates =
                            candidates.Concat(
                                (vgrid[Coordinate.X][Coordinate.Y].Where(
                                    x => x.Direction == IsobarDirection.West)));
                    }
                    break;
            }
            if (candidates == null)
            {
                return null;
            }
            return candidates
                .Where(x => x.Parent == null && x.Value == Value).FirstOrDefault();
        }
    }
}