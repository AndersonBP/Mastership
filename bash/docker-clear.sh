#!/bin/bash

#sudo docker system prune --all
#sudo docker system prune --volumes

sudo docker container stop $(sudo docker container ls -aq)

sudo docker container rm $(sudo docker container ls -aq)

sudo docker network rm mastership
