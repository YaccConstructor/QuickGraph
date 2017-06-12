#!/bin/bash
if [ ! -d "gunrock" ]; then
	git clone --depth 1 --recursive https://github.com/gunrock/gunrock
	cd gunrock;
	mkdir build && cd build;
	cmake ..;
	make -j3;
	cd ../..;
fi;
cd gunrock/build/lib;
cp libgunrock.so /usr/local/lib/