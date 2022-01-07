#!/bin/bash
# start after update on deskshare part of repo
cd /home/rjhadmin/deskshare/
now=$(date "+%F %H:%M:%S")
# echo "$now --> deskshare update > Starting..."
git -C /home/rjhadmin/deskshare/ fetch origin >> /home/rjhadmin/deskshare/cronlog.txt 
if  [ `git -C /home/rjhadmin/deskshare/ rev-list HEAD...origin/main --count` != 0 ] 
then
    echo "$now --> deskshare update > Remote git repo newer > Updating..."
    docker-compose --project-directory /home/rjhadmin/deskshare/ stop deskshare  >> /home/deskshare/cronlog.txt
    git -C /home/rjhadmin/deskshare/ reset --hard origin/main  >> /home/rjhadmin/cronlog.txt
    git -C /home/rjhadmin/deskshare/ fetch >> /home/rjhadmin/cronlog.txt
    git -C /home/rjhadmin/deskshare/ pull >> /home/rjhadmin/cronlog.txt
    chmod +x /home/rjhadmin/deskshare/*.sh >> /home/rjhadmin/cronlog.txt
    docker-compose --project-directory /home/rjhadmin/deskshare/ run deskshare python rpg/manage.py makemigrations >> /home/rjhadmin/deskshare/cronlog.txt
    docker-compose --project-directory /home/rjhadmin/deskshare/ run deskshare python rpg/manage.py migrate >> /home/rjhadmin/deskshare/cronlog.txt
    docker-compose --project-directory /home/rjhadmin/deskshare/ run deskshare python rpg/manage.py loaddata db_sample_data.json >> /home/rjhadmin/deskshare/cronlog.txt
    docker-compose --project-directory /home/rjhadmin/deskshare/ start deskshare >> /home/rjhadmin/deskshare/cronlog.txt
    # echo "$now --> deskshare update > Update done!"
else
    echo "$now --> deskshare update > Remote git repo same as local > Nothing to do!"
fi
# echo "$now --> deskshare update > Finished!"
