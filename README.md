# 🗺️ A* Pathfinding in C#

> A from-scratch implementation of the A\* search algorithm on a 2D grid — navigating around walls using heuristic-guided search and reconstructing the optimal path via parent-pointer backtracking.

---

## 📌 What Is This?

This project implements the **A\* (A-star) pathfinding algorithm** in C# without any external libraries. Given a 10×10 grid with a wall obstacle, the algorithm finds the **shortest path** from a start node `(0,0)` to a target node `(9,9)`, prints the result to the console, and visually marks the path on the grid.

```
..........| 0
..........| 1
..*#......| 2
..*#......| 3
..*#......| 4
..*#......| 5   ← wall blocked columns 2–7 at row 5
...*#.....| 6
....*#....| 7
.....*....| 8
......****| 9
```

---

## ⚙️ Technical Depth

### The A\* Algorithm

A\* is a **best-first graph search** algorithm. At each step it expands the node with the lowest **F score**, where:

```
F = G + H
```

| Variable | Meaning |
|---|---|
| `G` | Cost from the **start node** to the current node (exact, accumulated) |
| `H` | Estimated cost from current node to the **target** (heuristic) |
| `F` | Total estimated cost of the cheapest path through this node |

### Heuristic: Manhattan Distance

Because movement is restricted to 4 cardinal directions (no diagonals), the heuristic used is **Manhattan distance**:

```csharp
static int GetDistance(int x1, int y1, int x2, int y2)
{
    return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
}
```

This is **admissible** — it never overestimates the true cost — which guarantees A\* finds the optimal path.

### Direction Array Pattern

Rather than four separate `if` blocks for each neighbour (as seen in the `PathfindingV1` draft), the final implementation uses a **direction array** to DRY up the neighbour expansion loop:

```csharp
int[] dx = { -1, 1, 0, 0 };
int[] dy = {  0, 0,-1, 1 };

for (int i = 0; i < 4; i++)
{
    int newX = current.X + dx[i];
    int newY = current.Y + dy[i];
    // bounds check + wall check + visited check
}
```

### Path Reconstruction via Parent Pointers

Each `Node` stores a reference to the node it was reached from (`Parent`). Once the target is found, the path is reconstructed by **walking the parent chain backwards**:

```csharp
Node temp = current;
while (temp != null)
{
    map[temp.X, temp.Y] = 7; // mark path
    temp = temp.Parent;
}
```

### Visited State via Grid Values

Instead of a separate `HashSet<Node>`, the map's own cell values encode state:

| Value | Meaning |
|---|---|
| `0` | Unvisited ground |
| `1` | Wall (impassable) |
| `2` | Already visited (closed set) |
| `7` | Final path |

This is a compact in-place closed-set implementation.

---

## 🚀 Installation & Usage

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (6.0 or later)
- Any IDE: Visual Studio, Rider, or VS Code with C# extension

### Run It

```bash
git clone https://github.com/77natsu77/astar-pathfinding-csharp.git
cd astar-pathfinding-csharp
dotnet run
```

### Expected Output

The console will print the grid, with:
- `#` — wall tiles
- `.` — open/visited ground
- `*` — the optimal path (highlighted green in terminal)

### Customising the Map

Edit `Main()` to change the grid, wall positions, start, or target:

```csharp
int[,] map = new int[10, 10];

// Add walls manually:
map[3, 5] = 1;
map[4, 5] = 1;

Node start  = new Node(0, 0);
Node target = new Node(9, 9);
```

---

## 🧠 Learning Outcomes

Building this project developed the following skills:

- **Algorithm design** — Understanding why A\* outperforms Dijkstra (adds heuristic) and BFS (ignores cost) for single-target pathfinding
- **Heuristic reasoning** — Choosing an admissible, consistent heuristic and understanding its impact on optimality guarantees
- **Object-oriented modelling** — Designing a `Node` class with a computed property (`F => G + H`) to encapsulate algorithm state cleanly
- **Iterative refactoring** — Evolving from a verbose, repetitive `V1` implementation to a clean, generalised version using direction arrays
- **Grid-as-graph thinking** — Representing a 2D map as an implicit graph where nodes are cells and edges are valid moves
- **In-place state tracking** — Using the map array itself as a visited/closed set to avoid auxiliary data structures

---

## 🏷️ GitHub Topics

```
astar  pathfinding  csharp  dotnet  graph-search  heuristic  
algorithm  grid  game-ai  computer-science
```
