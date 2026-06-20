# Hashing Karşılaştırma (Hashing Comparison)

**A console-based C# application that compares the performance of three different hash collision resolution strategies: LISCH (Linked List), Progressive Overflow (Linear Probing), and Linear Quotient (Double Hashing).**

## Features

- **Three Hashing Methods**: Side-by-side comparison of LISCH, Linear Probing, and Double Hashing
- **Collision Resolution**: Each method handles hash collisions differently
- **Average Step Counter**: Tracks average steps per insertion/search for each method
- **Insert & Search Modes**: Menu-driven interface for both operations
- **Fixed Table Size**: 11-entry hash table for all methods

## Project Structure

```
hashing-karsilastirma/
├── Hashing.sln
├── Hashing/
│   ├── Program.cs            # Main program: all hashing algorithms
│   └── Properties/
│       └── AssemblyInfo.cs
└── README.md
```

## Algorithm Comparison

```mermaid
flowchart TD
    A[Input: Number to insert/search] --> B[Compute H1 = key % table_size]
    
    subgraph LISCH[LISCH - Linked List Chaining]
        C{tablo[H] empty?}
        C -->|Yes| D[Store directly]
        C -->|No| E[Follow link chain]
        E --> F[Find last link or empty slot<br>at end of table]
        F --> D
    end
    
    subgraph PO[Progressive Overflow - Linear Probing]
        G{tablo[H] empty?}
        G -->|Yes| H[Store directly]
        G -->|No| I[H+1, H+2, H+3...<br>Linear probe forward]
        I --> J[Find first empty slot]
        J --> H
    end
    
    subgraph LQ[Linear Quotient - Double Hashing]
        K{tablo[H] empty?}
        K -->|Yes| L[Store directly]
        K -->|No| M[H+adr, H+2adr, H+3adr...<br>where adr = key / table_size]
        M --> N[Find first empty slot<br>with step = adr]
        N --> L
    end
    
    A --> B
    B --> LISCH
    B --> PO
    B --> LQ
```

## Core Concepts

### Hash Function (H1)

The primary hash function used by all three methods:

```
h1(key) = key % table_size
```

For this implementation: `h1(key) = key % 11`

### Method 1: LISCH (Linear Indexed Sequential CHaining)

A hybrid approach using both a hash table and a separate link table:
- **Table**: Stores key values
- **Link Table**: Stores indices of chained overflow entries

When a collision occurs at index `h`, the algorithm:
1. Follows the link chain from `linkTable[h]`
2. If `linkTable[h] == -1` (end of chain), places the new key at the next available slot scanning **from the end** of the table
3. Updates the link to point to the new slot

```
Hash Table:   [__, 15, __, 35, __, __, __, __, 25, __, __]
Link Table:   [-1,  8, -1, -1, -1, -1, -1, -1,  3, -1, -1]

Insert 45 → h1(45) = 1 (occupied by 15)
  → link[1] = 8 (occupied by 25)
  → link[8] = 3 (occupied by 35)
  → link[3] = -1, so scan from end → place at [10], link[3] = 10
```

### Method 2: Progressive Overflow (Linear Probing)

Simple open addressing with linear probing:
- Probe sequence: `h, h+1, h+2, ..., table_size-1, 0, 1, ...`
- Clusters build up over time (primary clustering problem)

```
Insert 15 → h1(15) = 4 → tablo[4] empty → store
Insert 26 → h1(26) = 4 → collision → probe 5,6... → store at next empty
```

### Method 3: Linear Quotient (Double Hashing)

A second hash function determines the step size, reducing clustering:

```
h1(key) = key % 11
h2(key) = max(1, key / 11)   ← quotient as step size

Probe sequence: h1, h1+h2, h1+2*h2, ... (mod table_size)
```

This ensures keys with the same `h1` but different quotients take different probe paths, significantly reducing secondary clustering.

## Output Example

```
1.Ekle
2.Ara

1
15
15 collision olmadan 1 adimda tabloya eklendi... (LISCH)
15 collision olmadan 1 adimda tabloya eklendi... (PO)
15 collision olmadan 1 adimda tabloya eklendi... (LQ)
Ortalama Adim Sayilari :
LISCH = 1
PO = 1
LQ = 1
```

## Performance Comparison

| Method | Collision Strategy | Clustering | Worst-Case Search |
|---|---|---|---|
| **LISCH** | Separate chaining with link table | Moderate | O(n) |
| **Progressive Overflow** | Linear probing | High (primary) | O(n) |
| **Linear Quotient** | Double hashing | Low | O(n) |

The application tracks **average steps per operation** for each method, allowing empirical comparison as the table fills up.

## How to Use

1. Run the console application
2. Select option **1** (Ekle/Insert) or **2** (Ara/Search)
3. Enter integer numbers
4. For **Insert**: Enter up to 11 numbers (until table is full)
5. For **Search**: Enter a number to find; uses the same average step counter
6. Compare the average steps displayed for each method

## Building

Open `Hashing.sln` in Visual Studio 2008+ (retarget .NET Framework if needed) and build. This is a console application, no GUI.
