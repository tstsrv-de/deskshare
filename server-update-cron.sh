#!/bin/bash
# start after update on deskshare part of repo
cd /home/rjhadmin/deskshare/
now=$(date "+%F %H:%M:%S")
# echo "$now --> deskshare update > Starting..."
git -C /home/rjhadmin/deskshare/ fetch origin >> /home/rjhadmin/deskshare_cronlog.txt 
if  [ `git -C /home/rjhadmin/deskshare/ rev-list HEAD...origin/master --count` != 0 ] 
then
    echo "$now --> deskshare update > Remote git repo newer > Updating..."
    docker-compose --project-directory /home/rjhadmin/deskshare/ stop  >> /home/rjhadmin/deskshare_cronlog.txt
    git -C /home/rjhadmin/deskshare/ reset --hard origin/master  >> /home/rjhadmin/deskshare_cronlog.txt
    git -C /home/rjhadmin/deskshare/ fetch >> /home/rjhadmin/deskshare_cronlog.txt
    git -C /home/rjhadmin/deskshare/ pull >> /home/rjhadmin/deskshare_cronlog.txt
    chmod +x /home/rjhadmin/deskshare/*.sh >> /home/rjhadmin/deskshare_cronlog.txt
    docker-compose --project-directory /home/rjhadmin/deskshare/ start >> /home/rjhadmin/deskshare_cronlog.txt
    docker-compose --project-directory /home/rjhadmin/deskshare/ restart api >> /home/rjhadmin/deskshare_cronlog.txt
    # echo "$now --> deskshare update > Update done!"
else
    echo "$now --> deskshare update > Remote git repo same as local > Nothing to do!"
fi
# echo "$now --> deskshare update > Finished!"
