f=open("sample1.bvh","r")
lines=f.readlines()
result=[]
for x in lines:
    result.append(x.split(' ')[17] + ' ' + x.split(' ')[18] + ' ' + x.split(' ')[16])
f.close()
print(result)