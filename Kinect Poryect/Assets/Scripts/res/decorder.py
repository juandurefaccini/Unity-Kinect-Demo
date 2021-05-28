file = "sample1.bvh"
f=open(file,"r")
lines=f.readlines()
resultx=[]
resulty=[]
resultz=[]
for x in lines:
    resultx.append(x.split(' ')[17])
    resulty.append(x.split(' ')[18])
    resultz.append(x.split(' ')[16])
f.close()