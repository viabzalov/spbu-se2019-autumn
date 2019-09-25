#!/usr/bin/bash

declare -a IMP=("sequential" "gsl" "parallel")

if [ -f res.txt ]; then
    rm res.txt
fi

make

step=10

for ((size = 10; size <= 100; size += $step)) do
    if [[ $size -gt 100 ]] && [[ $step -ne 100 ]]; then
        size=100
        step=100
    fi
    echo "size = $size:" >> res.txt
    N=$(($size * ($size + 1)))
    cat /dev/urandom | tr -dc '0-9' | fold -w 7 | head -n $N > test_$size.txt
    for imp in "${IMP[@]}"; do
        valgrind --tool=callgrind --time-stamp=yes --log-file=result.txt ./main $size "test_$size.txt" "$imp"
        echo "  imp = $imp:" >> res.txt 
        echo "      $(cat result.txt | grep -E -o '[0-9][0-9]:[0-9][0-9]:[0-9][0-9]:[0-9][0-9].[0-9][0-9][0-9]' | tail -1)" >> res.txt
        rm result*
    done
    diff sequential gsl
    diff sequential parallel
done

make clean

rm -f callgrind.out* test*txt

for imp in "${IMP[@]}"; do
    rm "$imp"
done