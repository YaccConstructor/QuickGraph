#!/bin/bash
if [ ! -d "../../../src/QuickGraph.Gunrock/bin/Debug/" ]; then
	mkdir "../../../src/QuickGraph.Gunrock/bin/Debug/";
fi;
if [ ! -d "../../../src/QuickGraph.Gunrock/bin/Release/" ]; then
	mkdir "../../../src/QuickGraph.Gunrock/bin/Release/";
fi;

g++ -I../gunrock/ -c -fPIC run_gunrock.c -o libgunrockwrapper.o; 
g++ -shared libgunrockwrapper.o -lgunrock -o ../../../src/QuickGraph.Gunrock/bin/Debug/libgunrockwrapper.so
cp ../../../src/QuickGraph.Gunrock/bin/Debug/libgunrockwrapper.so ../../../src/QuickGraph.Gunrock/bin/Release/libgunrockwrapper.so