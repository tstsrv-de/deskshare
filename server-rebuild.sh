#!/bin/bash
# stops docker-compose, reloads git repo, builds docker-compose fresh and restarts
cd /home/rjhadmin/deskshare/
docker-compose --project-directory /home/rjhadmin/deskshare/ stop
git fetch origin
git reset --hard origin/master
git -C /home/rjhadmin/deskshare/ fetch
git -C /home/rjhadmin/deskshare/ pull

# optional: delete also all images and prune local-data, then build with no-cache 
#docker stop $(docker ps -a -q)
#docker system prune -a --volumes
#docker-compose --project-directory /home/rjhadmin/deskshare/ build --no-cache
cd /home/rjhadmin/deskshare/
docker-compose build 
docker-compose start
docker-compose restart deskshare_api

    
chmod +x /home/rjhadmin/deskshare/*.sh
