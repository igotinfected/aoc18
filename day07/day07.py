with open("input.txt") as inp:
	data = inp.read().splitlines()

data = [(x[5], x[36]) for x in data]

m = dict()

for x in data:
	m[x[0]] = set()
	m[x[1]] = set()

for a, b in data:
	m[b].add(a)

l = []
out = []

for key in m:
	if len(m[key]) == 0:
		l.append(key)

w = 5
t = [(None, 0) for i in range(w)]
ans = 0

while len(l) > 0 or sum(map(lambda x: x[1], t)) > 0:
	for i in range(len(t)):
		l.sort()
		# print(l, t[i])
		if t[i][1] == 0 and len(l) > 0:
			e = l[0]
			out += [e]
			l = l[1:]

			t[i] = (e, 60 + ord(e) - ord('A') + 1)

	# print(t, l)

	t = [(x, max(0, y-1)) for x, y in t]
	ans += 1

	for i in range(len(t)):
		if t[i][1] == 0:
			for x in m:
				if t[i][0] in m[x]:
					m[x].remove(t[i][0])
					if len(m[x]) == 0:
						l.append(x)

print(ans)
