#!/bin/sh
# wait-for-db.sh

>&2 echo "checking mysql service"

CONTAINER="dsapi_mysql"
USERNAME="root"
PASSWORD="23131001"
while ! docker exec $CONTAINER mysql --user=$USERNAME --password=$PASSWORD -e "SELECT 1" >/dev/null 2>&1; do
   >&2 echo "mysql is down - waiting"
   sleep 1
done
  
>&2 echo "mysql is up - executing command"
exec "$@"

