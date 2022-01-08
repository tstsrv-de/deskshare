#!/bin/bash
# stops docker-compose, reloads git repo, builds docker-compose fresh and restarts

docker-compose --project-directory /home/rjhadmin/deskshare/ stop
git fetch origin
git reset --hard origin/master
git -C /home/rjhadmin/deskshare/ fetch
git -C /home/rjhadmin/deskshare/ pull

# optional: delete also all images and prune local-data, then build with no-cache 
#docker stop $(docker ps -a -q)
#docker system prune -a --volumes
#docker-compose --project-directory /home/rjhadmin/deskshare/ build --no-cache

docker-compose --project-directory /home/rjhadmin/deskshare/ build 
docker-compose --project-directory /home/rjhadmin/deskshare/ start
docker-compose --project-directory /home/rjhadmin/deskshare/ restart api

    
chmod +x /home/rjhadmin/deskshare/*.sh
