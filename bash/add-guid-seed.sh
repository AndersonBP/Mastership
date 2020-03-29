#!/bin/bash

file=$path"./src/infra/Rv.Database/Seed/ModelSeed.cs"

uuid()
{
    local N B T

    for (( N=0; N < 16; ++N ))
    do
        B=$(( $RANDOM%255 ))

        if (( N == 6 ))
        then
            printf '4%x' $(( B%15 ))
        elif (( N == 8 ))
        then
            local C='89ab'
            printf '%c%x' ${C:$(( $RANDOM%${#C} )):1} $(( B%15 ))
        else
            printf '%02x' $B
        fi

        for T in 3 5 7 9
        do
            if (( T == N ))
            then
                printf '-'
                break
            fi
        done
    done

    echo
}

conteudo=$(sed ':a;N;$!ba;s/\n/yyy/g' $file)

while [[ $conteudo =~ '""' ]]
do
    id=$(uuid)
    regex="s/\"\"/\"$id\"/"

    conteudo=$(echo ${conteudo} | sed -e $regex)
done

conteudo=$(echo ${conteudo} | sed s/yyy/\\r\\n/g)

echo $conteudo | tee $file
