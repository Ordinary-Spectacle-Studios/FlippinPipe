import enum
import json
import random
import sys

# mod descriptions
"""stuck -> (x) -> a tile that is immune to flipping and will not move.  You also cannot click this tile to initiate a flip"""
"""fire/ice -> {ice{ }fire} -> at the end of the puzzle, these two must be side by side to unlock them and win"""
"""ghost -> [x] -> You don't know what this tile is until you click one of the tiles that is the same type as it.  There will always be n-1 ghosts for a given tile type
    where n is the number of tiles in the puzzle of a given type."""
"""right tile -> >x> -> flips with it's neighbor to the right before flipping.  If no neighbor is to the right, has no effect"""
"""left tile -> <x< -> flips with it's neighbor to the left before flipping.  If no neighbor is to the left, has no effect"""

def generate(amt: int,length: int, variability: int, mod: str) -> list:
    """amt:  amount of puzzles, length:  length of puzzle, variability: number of unique characters"""
    chr = ["a", "b", "c", "d", "e", "f", "g", "h"][:variability]
    out = []
    for _ in range(amt):
        puzz = ''.join([random.choice(chr) for _ in range(length)])
        order = list(set(puzz))
        random.shuffle(order)
        order = ''.join(order)
        puzz = mods(puzz, order, mod)
        out += [order + ":" + puzz]
    return out

def mods(puzzle: str, order: str, mod: str) -> str:
    epuzz = list(enumerate(list(puzzle)))
    if mod == 'stuck':
        gcount = random.choice([1,2])
        d = {}
        for ord in order:
            d[ord] = puzzle.count(ord)
        solved = ''.join([(x * d[x]) for x in order])
        matches = [x for x in epuzz if solved[x[0]] == x[1]]
        while gcount > 0:
            for m in matches:
                if random.getrandbits(1):
                    epuzz[m[0]] = (m[0], "(" + m[1] + ")")
                    gcount -= 1
                    break
        return '|'.join([x[1] for x in epuzz]) 
    if mod == 'ghost':
        gcount = random.choice(range(1,5))
        while gcount > 0:
            for val in list(order):
                if puzzle.count(val) > 1 and random.getrandbits(1):
                    positions = [x[0] for x in epuzz if x[1] == val]
                    cpos = random.choice(positions)
                    epuzz[cpos] = (cpos, "[" + val + "]")
                    gcount -= 1
                    break
        return '|'.join([x[1] for x in epuzz])
    if mod == 'right':
        gcount = random.choice(range(1,5))
        while gcount > 0:
            pick = random.choice(epuzz)
            if ">" not in pick[1]:
                epuzz[pick[0]] = (pick[0], ">" + pick[1] + ">")
                gcount -=1
        return '|'.join([x[1] for x in epuzz])
    if mod == 'left':
        gcount = random.choice(range(1,5))
        while gcount > 0:
            pick = random.choice(epuzz)
            if "<" not in pick[1]:
                epuzz[pick[0]] = (pick[0], "<" + pick[1] + "<")
                gcount -=1
        return '|'.join([x[1] for x in epuzz])
    if mod == 'fireice':
        pass
    else:
        pass
    


if __name__ == "__main__":
    puzzs = generate(10, 12, 4, 'right')
    for puzz in puzzs:
        print(puzz)