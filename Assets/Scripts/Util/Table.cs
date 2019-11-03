using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ２次元のテーブル
/// </summary>
public class Table<Type> : IEnumerable<TableChip<Type>> {
    Type[, ] _table;
    Type _outer;

    public int Height {
        get {
            return _table.GetLength (1);
        }
    }

    public int Width {
        get {
            return _table.GetLength (0);
        }
    }

    public Table (int w, int h, Type outer, Type init) {
        _table = new Type[w, h];
        _outer = outer;
    }

    static List<Vector2Int> _adjacents;
    /// <summary>
    /// (+1,0),(-1,0),(0,+1),(0,-1)を返す
    /// </summary>
    /// <returns></returns>
    static List<Vector2Int> adjacents {
        get {
            if (_adjacents == null) {
                _adjacents = new List<Vector2Int> ();
                _adjacents.Add (new Vector2Int (1, 0));
                _adjacents.Add (new Vector2Int (-1, 0));
                _adjacents.Add (new Vector2Int (0, 1));
                _adjacents.Add (new Vector2Int (0, -1));
            }
            return _adjacents;
        }
    }

    /// <summary>
    /// 4近傍を返す
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    List<Vector2Int> GetAdjacentPoses (Vector2Int pos) {
        var poses = new List<Vector2Int> ();
        foreach (var a in adjacents) {
            poses.Add (pos + a);
        }
        return poses;
    }

    /// <summary>
    /// ４近傍のTableChipを返す
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    List<TableChip<Type>> GetAdjacentChips (Vector2Int pos) {
        var poses = GetAdjacentPoses (pos);

        var list = new List<TableChip<Type>> ();
        foreach (var p in poses) {
            list.Add (GetTableChip (p));
        }
        return list;
    }

    /// <summary>
    /// そのブロックが何個くっついてるかのTable
    /// </summary>
    /// <returns></returns>
    public Table<int> GetTableOfAdjacentBlock () {
        var ans = new Table<int> (Height, Width, -1, -1);
        var canvisit = new Table<bool> (Height, Width, false, true);
        this.GetList ();
        foreach (var chip in this) {
            if (canvisit[chip.Position]) {
                var same = _GetBoolTableOfSameBlock (chip.Content);
                var next = new Queue<TableChip<Type>> ();
                var visited = new List<TableChip<Type>> ();
                next.Enqueue (chip);
                canvisit[chip.Position] = false;
                visited.Add (chip);
                while (next.Count > 0) {
                    var now = next.Dequeue ();
                    var nextPoses = GetAdjacentPoses (now.Position);
                    foreach (var nextpos in nextPoses) {
                        if (canvisit[nextpos]) {
                            next.Enqueue (chip);
                            canvisit[chip.Position] = false;
                            visited.Add (chip);
                        }
                    }
                }
                int cnt = visited.Count;
                foreach (var v in visited) {
                    ans[v.Position] = cnt;
                }
            }
        }
        return ans;
    }

    public int GetAdjacentBlock (int x, int y) {
        throw new NotImplementedException ();
    }

    /// <summary>
    /// 指定したのと同じブロックならtrueとなるTableを返す
    /// </summary>
    /// <returns></returns>
    Table<bool> _GetBoolTableOfSameBlock (Type block) {
        var t = new Table<bool> (Width, Height, false, false);
        foreach (var c in this) {
            t[c.Position] = c.Content.Equals (block);
        }
        return t;
    }

    int _CountAdjacentBlock (int x, int y, Table<bool> same) {
        throw new NotImplementedException ();
    }

    TableChip<Type> GetTableChip (int x, int y) {
        var chip = new TableChip<Type> ();
        chip.X = x;
        chip.Y = y;
        chip.Content = _table[x, y];
        return chip;
    }

    TableChip<Type> GetTableChip (Vector2Int pos) {
        return GetTableChip (pos.x, pos.y);
    }

    public Type this [int i, int j] {
        get {
            if (0 <= i && i < Width &&
                0 <= j && j < Height) {
                return _table[i, j];
            } else {
                return _outer;
            }
        }
        set {
            if (0 <= i && i < Width &&
                0 <= j && j < Height) {
                _table[i, j] = value;
            }
        }
    }

    public Type this [Vector2Int vec] {
        get {
            return this [vec.x, vec.y];
        }
        set {
            this [vec.x, vec.y] = value;
        }
    }

    public IEnumerator<TableChip<Type>> GetEnumerator () {
        for (int h = 0; h < Height; ++h) {
            for (int w = 0; w < Width; ++w) {
                yield return GetTableChip (h, w);
            }
        }
    }
    IEnumerator IEnumerable.GetEnumerator () {
        return this.GetEnumerator ();
    }

    public List<TableChip<Type>> GetList () {
        var list = new List<TableChip<Type>> ();
        foreach (var chip in this) {
            list.Add (chip);
        }
        return list;
    }
}

public class TableChip<Type> {
    public int X {
        get;
        set;
    }
    public int Y {
        get;
        set;
    }
    public Type Content {
        get;
        set;
    }
    public Vector2Int Position {
        get {
            return new Vector2Int (X, Y);
        }
        set {
            X = value.x;
            Y = value.y;
        }
    }
}
