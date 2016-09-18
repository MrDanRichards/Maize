// Prim Maze algorithm for the Maize game

using UnityEngine;
using System.Collections.Generic;

public class PrimWallGen : MonoBehaviour
{
    public Transform horzWall;
    public Transform vertWall;
    public int horzSize;
    public int vertSize;
    Cell[,] grid;
    System.Random rand;

    //public int maxSize;

    // Cell class for each spot on maze grid
    public class Cell
    {
        public int x; // x coordinate
        public int y; // y coordinate
        public bool visited; // has the maze used this spot?
        public bool U; // upper wall exists
        public bool D; // lower wall exists
        public bool L; // left wall exists
        public bool R; // right wall exists

        public Cell(int xcoord, int ycorrd, bool isVisited,
            bool up, bool down, bool left, bool right)
        {
            x = xcoord;
            y = ycorrd;
            visited = isVisited;
            U = up;
            D = down;
            L = left;
            R = right;
        }
    }

    public enum dirs
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NUM_DIRS,
        INVALID
    }

	void Start ()
    {
        // Start/seed random
        string randSeed = System.DateTime.Now.ToString();
        rand = new System.Random(randSeed.GetHashCode());
        
        grid = new Cell[horzSize, vertSize];
        generatePrimMaze();
        createWalls();
	}

    // Adds and removes walls to create a maze with the Prim algorithm
    void generatePrimMaze()
    {
        List<Cell> cellStack = new List<Cell>();
        Cell currentCell;
        dirs dirToMove;
        int consecutive = 0;
        int maxConsec = 10;
        
        // Initialize all cells as being unexplored,
        // having all walls, and with their coordintates
        for (int x = 0; x < horzSize; x++)
        {
            for (int y = 0; y < vertSize; y++)
            {
                grid[x, y] = new Cell(x, y, false, true, true, true, true);
            }
        }

        int startx = rand.Next(0, horzSize);
        int starty = rand.Next(0, vertSize);
        // Add origin point of maze, can make variable later
        cellStack.Add(grid[startx, starty]);
        grid[startx, starty].visited = true; // initial cell has been visted
        currentCell = grid[startx, starty];
        
        while (cellStack.Count > 0)
        {
            if (consecutive == maxConsec || currentCell==null)
            {
                // Focus on a random cell in the stack
                currentCell = cellStack[rand.Next(0, cellStack.Count)];
                consecutive = 0;
            }
            else
            {
                consecutive++;
            }

            // Find and execute a valid move, or remove cell from options
            dirToMove = findOpen(currentCell);
            switch (dirToMove)
            {
                case dirs.UP:
                    //print(currentCell.x + " " + currentCell.y + " " + "went UP\n");
                    currentCell.U = false; // delete top wall;
                    grid[currentCell.x, currentCell.y+1].D = false; // delete bottom wall of new
                    cellStack.Add(grid[currentCell.x, currentCell.y + 1]); // add new to stack
                    grid[currentCell.x, currentCell.y + 1].visited = true; // mark new as visted
                    currentCell = grid[currentCell.x, currentCell.y + 1];
                    break;
                case dirs.DOWN:
                    //print(currentCell.x + " " + currentCell.y + " " + "went DOWN\n");
                    currentCell.D = false; // delete lower wall;
                    grid[currentCell.x, currentCell.y - 1].U = false; // delete top wall of new
                    cellStack.Add(grid[currentCell.x, currentCell.y - 1]); // add new to stack
                    grid[currentCell.x, currentCell.y - 1].visited = true; // mark new as visted
                    currentCell = grid[currentCell.x, currentCell.y - 1];
                    break;
                case dirs.LEFT:
                    //print(currentCell.x + " " + currentCell.y + " " + "went LEFT\n");
                    currentCell.L = false; // delete left wall;
                    grid[currentCell.x - 1, currentCell.y].R = false; // delete right wall of new
                    cellStack.Add(grid[currentCell.x - 1, currentCell.y]); // add new to stack
                    grid[currentCell.x - 1, currentCell.y].visited = true; // mark new as visted
                    currentCell = grid[currentCell.x - 1, currentCell.y];
                    break;
                case dirs.RIGHT:
                    //print(currentCell.x + " " + currentCell.y + " " + "went RIGHT\n");
                    currentCell.R = false; // delete right wall;
                    grid[currentCell.x + 1, currentCell.y].L = false; // delete left wall of new
                    cellStack.Add(grid[currentCell.x + 1, currentCell.y]); // add new to stack
                    grid[currentCell.x + 1, currentCell.y].visited = true; // mark new as visted
                    currentCell = grid[currentCell.x + 1, currentCell.y];
                    break;
                default:
                    //print(currentCell.x + " " + currentCell.y + " " + "went invalid\n");
                    cellStack.Remove(currentCell); // no valid move, remove from options
                    currentCell = null;
                    break;
            }
            
        } // end while loop
        
    } // generatePrimMaze()

    // returns a direction for the maze to move.
    // returns dirs.INVALID if no direction to move
    dirs findOpen(Cell currentCell)
    {
        dirs openDir;
        List<dirs> validDirs = new List<dirs>();

        // Put together a list of the possible valid moves
        for (dirs dirTry = 0; dirTry < dirs.NUM_DIRS; dirTry++)
        {
            if (isDirValid(dirTry, currentCell))
            {
                validDirs.Add(dirTry);
            }
        }
        
        if (validDirs.Count == 0)
        {
            openDir = dirs.INVALID;
        }
        else
        {
            // select a random direction from the list of valids
            openDir = validDirs[rand.Next(0, validDirs.Count)];
        }

        return openDir;
    }

    // Tests to see if a direction is a valid move from the current cell
    bool isDirValid(dirs dirTry, Cell currentCell)
    {
        bool dirIsValid = true;

        if (dirs.UP == dirTry)
        {
            if (currentCell.y + 1 == vertSize) // out of bounds, invalid
            {
                dirIsValid = false;
            }
            else if (grid[currentCell.x, currentCell.y + 1].visited) // already visited, invalid
            {
                dirIsValid = false;
            }
        }
        else if (dirTry == dirs.DOWN)
        {
            if (currentCell.y - 1 < 0) // out of bounds, invalid
            {
                dirIsValid = false;
            }
            else if (grid[currentCell.x, currentCell.y - 1].visited) // already visited, invalid
            {
                dirIsValid = false;
            }
        }
        else if (dirTry == dirs.LEFT)
        {
            if (currentCell.x - 1 < 0) // out of bounds, invalid
            {
                dirIsValid = false;
            }
            else if (grid[currentCell.x - 1, currentCell.y].visited) // already visited, invalid
            {
                dirIsValid = false;
            }
        }
        else if (dirTry == dirs.RIGHT)
        {
            if (currentCell.x + 1 == vertSize) // out of bounds, invalid
            {
                dirIsValid = false;
            }
            else if (grid[currentCell.x + 1, currentCell.y].visited) // already visited, invalid
            {
                dirIsValid = false;
            }
        }
        else
        {
            dirIsValid = false; // not sure what would fall in here, but be safe for error catching?
        }

        return dirIsValid;
    } // isDirValid
	
    // Creates wall components based on the grid generated by the maze algorithm
    // Assumption is that we're on a grid with each cell being 1x1 units
    void createWalls()
    {
        Cell currentCell;

        for (int x = 0; x < horzSize; x++)
        {
            for (int y = 0; y < vertSize; y++)
            {
                currentCell = grid[x, y];
                if (currentCell.U)
                {
                    // Only create upper wall of cell if it's the top row.
                    // Otherwise, let the cell above take care of it.
                    if (y == vertSize - 1)
                    {
                        Instantiate(horzWall, new Vector3(x + .5f, .5f, y + 1), Quaternion.identity);
                    }
                }
                if (currentCell.D)
                {
                    Instantiate(horzWall, new Vector3(x + .5f, .5f, y), Quaternion.identity);
                }
                if (currentCell.L)
                {
                    Instantiate(vertWall, new Vector3(x, .5f, y + .5f), Quaternion.identity);
                }
                if (currentCell.R)
                {
                    // Only create right wall if it's the far right column.
                    // Otherwise, let the cell to the right take care of it.
                    if (x == horzSize - 1)
                    {
                        Instantiate(vertWall, new Vector3(x + 1, .5f, y + .5f), Quaternion.identity);
                    }
                }
            }
        }
    } // createWalls()

}
