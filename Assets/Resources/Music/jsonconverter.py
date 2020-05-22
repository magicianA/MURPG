import json
x = open("1.json")
y = open("2.json",'w')
str = x.read()
data = json.loads(str)

bpm = data["time_base"] * 30

def lerp(l,r):
    if abs(l['pos'] - r['pos']) < 0.1:
        return [l,r]
    if l['pos'] < r['pos']:
        d = 1
    else:
        d = -1
    res = [l]
    while abs(l['pos'] - r['pos']) > 0.1:
        l['pos'] += d * 0.1
        res.append(l)
    return res
def checktype(note):
    if note['type'] == 0:
        return "Click"
    if note['type'] == 5:
        return "Flick"
    if note['type'] == 3 or note['type'] == 4:
        return 'Normal'
    return 'Click'
notes = []
bnotes = data["note_list"]
normal_notes = []
for note in bnotes:
    t = {}
    t["id"] = note['id']
    t["pos"] = (note['x'] * 2 - 1)
    t["time"] = note['tick']
    t['size'] = 1.0
    t['type'] = checktype(note)
    if(t['type'] == 'Normal'):
        normal_notes.append(t)
    else:
        for i in range(len(normal_notes)):
            if i + 1 < len(normal_notes):
                notes.append(lerp(normal_notes[i],normal_notes[i+1]))
            else:
                notes.append(normal_notes[len(normal_notes) - 1])
        normal_notes = []
        notes.append(t)
for i in range(len(notes)):
    t = notes[i]
    t['id'] = i + 1
notecount = len(notes)

dest = {}
dest['bpm'] = bpm
dest['notecount'] = notecount
dest['notes'] = notes

res = json.dumps(dest)
y.write(res)
