using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace UriAgassi.Isobars.Algo
{
    public class Isobar
    {
        public Isobar(IsobarPoint first, IsobarPoint[][][] vgrid, IsobarPoint[][][] hgrid, bool isClosed)
        {
            Value = first.Value;
            IsClosed = isClosed;
            Points = GetPoints(first, vgrid, hgrid).ToArray();
        }

        private IEnumerable<IsobarPoint> GetPoints(IsobarPoint first, IsobarPoint[][][] vgrid, IsobarPoint[][][] hgrid)
        {
            first.Parent = this;
            yield return first;
            IsobarPoint next, current = first;
            while ((next = current.FindNext(vgrid, hgrid)) != null && next != first)
            {
                next.Parent = this;
                yield return next;
                current = next;
            }
        }

        public IsobarPoint[] Points { get; private set; }

        public int Value { get; private set; }

        public bool IsClosed { get; private set; }

        public static IEnumerable<Isobar> CreateIsobars(double[][] data, out IsobarPoint[][][] hgrid, out IsobarPoint[][][] vgrid)
        {
            if (data == null)
            {
                hgrid = vgrid = null;
                return null;
            }
            hgrid = new IsobarPoint[data.Length][][];
            vgrid = new IsobarPoint[data.Length - 1][][];
            for (int x = 0; x < data.Length; x++)
            {
                if (x != data.Length - 1)
                {
                    vgrid[x] = new IsobarPoint[data[x].Length][];
                }

                hgrid[x] = new IsobarPoint[data[x].Length - 1][];
                for (int y = 0; y < data[x].Length; y++)
                {
                    var value = data[x][y];
                    if (x != 0)
                    {
                        vgrid[x - 1][y] = Enumerable.Range(Math.Min((int)value, (int)data[x - 1][y]) + 1, Math.Abs((int)data[x - 1][y] - (int)value))
                            .Select(v => new IsobarPoint
                            {
                                Coordinate = new Coordinate(x - 1, y),
                                Location = new 
                                    Point((x - (v - value) / (data[x - 1][y] - value)), y),
                                Direction = (value > data[x - 1][y]) ? IsobarDirection.East : IsobarDirection.West,
                                Value = v
                            }).ToArray();
                    }
                    if (y < data[x].Length - 1)
                    {
                        hgrid[x][y] = Enumerable.Range(Math.Min((int)value, (int)data[x][y + 1]) + 1, Math.Abs((int)data[x][y + 1] - (int)value))
                            .Select(v => new IsobarPoint
                            {
                                Coordinate = new Coordinate(x, y),
                                Location = new Point(x,  (y + (v - value) / (data[x][y + 1] - value))),
                                Direction = (value > data[x][y + 1]) ? IsobarDirection.South : IsobarDirection.North,
                                Value = v
                            }).ToArray();
                    }
                }
            }

            return GenerateIsobars(vgrid, hgrid);
        }

        private static IEnumerable<Isobar> GenerateIsobars(IsobarPoint[][][] vgrid, IsobarPoint[][][] hgrid)
        {
            // iterate the frame
            foreach (var l in vgrid)
            {
                foreach (var i in l[0].Where(i => i.Direction == IsobarDirection.East))
                {
                    yield return new Isobar(i, vgrid, hgrid, false);
                }
                foreach (var i in l.Last().Where(i => i.Direction == IsobarDirection.West))
                {
                    yield return new Isobar(i, vgrid, hgrid, false);
                }
            }
            foreach (var i in hgrid[0].SelectMany(l => l.Where(i => i.Direction == IsobarDirection.South)))
            {
                yield return new Isobar(i, vgrid, hgrid, false);
            }
            foreach (var i in hgrid.Last().SelectMany(l => l.Where(i => i.Direction == IsobarDirection.North)))
            {
                yield return new Isobar(i, vgrid, hgrid, false);
            }

            // find circles
            for (int y = 1; y < vgrid.Length - 1; y++)
            {
                for (int x = 1; x < vgrid[y].Length - 1; x++)
                {
                    foreach (var i in vgrid[y][x].Where(i => i.Parent == null))
                    {
                        yield return new Isobar(i, vgrid, hgrid, true);
                    }
                }
            }
        }

    }
}
