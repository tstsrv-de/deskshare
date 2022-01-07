# tst-srv.de Config README

Here is the config and some scripts for 'Desksahre' > tstsrv.de for Max and Co.

- Hosted @ hosteurope.de
- Domain @ inwx.de

Henning 'haenno' Beier, haenno@web.de, 24.11.2021

## Automation of git repo updates to this servers containers

With the 'server-update-cron.sh' script in the **crontab** as...
``*/5 * * * * /home/rjhadmin/desksahre/server-update-cron.sh >> /home/rjhadmin/deskshare/cronlog.txt 2>&1``

...this server automaticly pulls each new commit to this repo, and restarts the docker containers.
