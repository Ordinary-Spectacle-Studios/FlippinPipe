import json
import random
import sys



def generate(amt: int,length: int, variability: int) -> list:
    """amt:  amount of puzzles, length:  length of puzzle, variability: number of unique characters"""
    chr = ["a", "b", "c", "d", "e", "f", "g", "h"][:variability]
    out = []
    for _ in range(amt):
        out += [''.join([random.choice(chr) for _ in range(length)])]
    return out

def solvedict(puzzles: list) -> dict:
    ranked = {}
    for p in puzzles:
        t = process(p)
        if t not in ranked:
            ranked[t] = [p]
        else:
            ranked[t] += [p]
    return ranked

def didyouwin(test) -> bool:
    key = set(list(test))
    for k in key:
        count = test.count(k)
        if k * count in test:
            continue
        else:
            return False
    return True

def flipatindex(i: int, test: str) -> str:
    p1 = test[:i]
    p2 = test[i:]
    p2 = p2[::-1]
    test = p1 + p2
    return test

def process(test: str) -> int:
    turns = 0
    for i in range(len(test) -1):
        if test[i] == test[i+1]:
            continue
        else:
            for j in range(i+1, len(test)):
                if test[j] == test[i]:
                    if test[j] != test[j-1]:
                        test = flipatindex(j, test)
                        test = flipatindex(i+1, test)
                        turns += 2
    return turns

def johnformat(solv: dict) -> dict:
    """My little pony."""
    out = []
    for k,v in solv.items():
        out += [{"solveCount": k, "puzzle" : v}]
    return out


if __name__ == "__main__":
    a, l, v = [int(x) for x in sys.argv[1:]]
    puzz = generate(a, l, v)
    solv = johnformat(solvedict(puzz))

    with open(f"{a}_{l}_{v}_puzzles.json", "w", encoding="utf-8") as jf:
        json.dump(sorted(solv, key=lambda x: x["solveCount"]), jf, ensure_ascii=False, indent=4)
