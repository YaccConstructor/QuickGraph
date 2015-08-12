rem usage: dumpheap PID
cdb -p %1 -c "$$>< dumpheap.txt"